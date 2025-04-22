using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FakeHTML : MonoBehaviour
{
    [Header("Fake HTML TextAsset")]
    public TextAsset fakeHtmlFile; // Assign your text file here (e.g., fakePage.txt)

    [Header("Roots")]
    public Transform uiRoot; // Assign a panel to be a container where other panels will go
    private RectTransform scrollContentRoot; 


    [Header("Image Sprite Lookup")]
    public Dictionary<string, Sprite> imageLookup = new(); // Manually fill this or load dynamically

    void Start()
    {
        // Start parsing when the game starts
        if (fakeHtmlFile != null && uiRoot != null)
        {
            // Create ScrollRect wrapper first
            CreateScrollWrapper();

            // Parse and add content
            ParseFakeHtml(fakeHtmlFile.text);
        }
    }

    void ParseFakeHtml(string content)
    {
        // Handle full-screen <background>
        var bgRegex = new Regex(@"<background\s+(.*?)>(.*?)<\/background>", RegexOptions.Singleline); //just found out about regex but it's super useful
        foreach (Match match in bgRegex.Matches(content))
        {
            var bgAttrs = ParseAttributes(match.Groups[1].Value);
            var bgContent = match.Groups[2].Value;
            CreateBackground(bgAttrs, bgContent);
        }

        // Handle regular <panel>
        var panelRegex = new Regex(@"<panel\s+(.*?)>(.*?)<\/panel>", RegexOptions.Singleline);
        foreach (Match match in panelRegex.Matches(content))
        {
            var panelAttrs = ParseAttributes(match.Groups[1].Value);
            var panelContent = match.Groups[2].Value;
            CreatePanel(panelAttrs, panelContent);
        }
    }

    Dictionary<string, string> ParseAttributes(string attrText)
    {
        var attrDict = new Dictionary<string, string>();
        var attrRegex = new Regex(@"(\w+)=([^\s]+)");

        foreach (Match match in attrRegex.Matches(attrText))
        {
            attrDict[match.Groups[1].Value] = match.Groups[2].Value;
        }

        return attrDict;
    }
    void CreateScrollWrapper()
    {
        // ScrollRect parent
        GameObject scrollGO = new GameObject("ScrollRect", typeof(RectTransform), typeof(ScrollRect), typeof(Image));
        scrollGO.transform.SetParent(uiRoot, false);

        var scrollRect = scrollGO.GetComponent<ScrollRect>();
        var scrollImg = scrollGO.GetComponent<Image>();
        scrollImg.color = Color.clear;

        var scrollRT = scrollGO.GetComponent<RectTransform>();
        scrollRT.anchorMin = Vector2.zero;
        scrollRT.anchorMax = Vector2.one;
        scrollRT.offsetMin = Vector2.zero;
        scrollRT.offsetMax = Vector2.zero;

        // Viewport
        GameObject viewportGO = new GameObject("Viewport", typeof(RectTransform), typeof(Mask), typeof(Image));
        viewportGO.transform.SetParent(scrollGO.transform, false);

        var viewportRT = viewportGO.GetComponent<RectTransform>();
        viewportRT.anchorMin = Vector2.zero;
        viewportRT.anchorMax = Vector2.one;
        viewportRT.offsetMin = Vector2.zero;
        viewportRT.offsetMax = Vector2.zero;

        var viewportImg = viewportGO.GetComponent<Image>();
        viewportImg.color = new Color(0, 0, 0, 0); // Transparent
        viewportImg.raycastTarget = true;
        viewportGO.GetComponent<Mask>().showMaskGraphic = false;

        // Content root
        GameObject contentGO = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        contentGO.transform.SetParent(viewportGO.transform, false);

        scrollContentRoot = contentGO.GetComponent<RectTransform>();
        scrollRect.content = scrollContentRoot;
        scrollRect.viewport = viewportRT;
        scrollRect.horizontal = false;

        var layoutGroup = contentGO.GetComponent<VerticalLayoutGroup>();
        layoutGroup.spacing = 10;
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);

        //var fitter = contentGO.GetComponent<ContentSizeFitter>();
        //fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    void CreatePanel(Dictionary<string, string> attrs, string innerHtml)
    {
        GameObject panelGO = new GameObject("FakePanel", typeof(RectTransform), typeof(Image), typeof(LayoutElement));
        panelGO.transform.SetParent(scrollContentRoot, false);

        var rect = panelGO.GetComponent<RectTransform>();
        var img = panelGO.GetComponent<Image>();
        var layoutElement = panelGO.GetComponent<LayoutElement>();

        // Set anchors
        if (attrs.ContainsKey("anchorMin"))
            rect.anchorMin = ParseVector2(attrs["anchorMin"]);
        else
            rect.anchorMin = new Vector2(0, 1); // default top-left

        if (attrs.ContainsKey("anchorMax"))
            rect.anchorMax = ParseVector2(attrs["anchorMax"]);
        else
            rect.anchorMax = new Vector2(0, 1);

        // Stretching vs fixed size
        bool isStretching = rect.anchorMin != rect.anchorMax;

        if (isStretching)
        {
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            layoutElement.minHeight = attrs.ContainsKey("height") ? float.Parse(attrs["height"]) : 100f;
            layoutElement.flexibleWidth = 1;
        }
        else
        {
            rect.sizeDelta = new Vector2(
                attrs.ContainsKey("width") ? float.Parse(attrs["width"]) : 200,
                attrs.ContainsKey("height") ? float.Parse(attrs["height"]) : 150
            );

            layoutElement.preferredWidth = rect.sizeDelta.x;
            layoutElement.preferredHeight = rect.sizeDelta.y;
        }

        // Background color
        img.color = attrs.ContainsKey("color") ? ColorFromName(attrs["color"]) : Color.gray;

        // Handle nested <text> and <image>
        var textRegex = new Regex(@"<text\s+(.*?)>(.*?)<\/text>");
        foreach (Match match in textRegex.Matches(innerHtml))
        {
            var textAttrs = ParseAttributes(match.Groups[1].Value);
            var textContent = match.Groups[2].Value;
            CreateText(panelGO.transform, textAttrs, textContent);
        }

        var imageRegex = new Regex(@"<image\s+(.*?)\/>");
        foreach (Match match in imageRegex.Matches(innerHtml))
        {
            var imageAttrs = ParseAttributes(match.Groups[1].Value);
            CreateImage(panelGO.transform, imageAttrs);
        }
    }
    void CreateBackground(Dictionary<string, string> attrs, string innerHtml)
    {
        GameObject bgGO = new GameObject("SiteBackground", typeof(RectTransform), typeof(Image));
        bgGO.transform.SetParent(uiRoot, false);

        var rect = bgGO.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        var img = bgGO.GetComponent<Image>();
        img.color = attrs.ContainsKey("color") ? ColorFromName(attrs["color"]) : Color.black;

        // Support inner text or image just like panels
        var textRegex = new Regex(@"<text\s+(.*?)>(.*?)<\/text>");
        foreach (Match match in textRegex.Matches(innerHtml))
        {
            var textAttrs = ParseAttributes(match.Groups[1].Value);
            var textContent = match.Groups[2].Value;
            CreateText(bgGO.transform, textAttrs, textContent);
        }

        var imageRegex = new Regex(@"<image\s+(.*?)\/>");
        foreach (Match match in imageRegex.Matches(innerHtml))
        {
            var imageAttrs = ParseAttributes(match.Groups[1].Value);
            CreateImage(bgGO.transform, imageAttrs);
        }
    }
    void CreateText(Transform parent, Dictionary<string, string> attrs, string content)
    {
        GameObject textGO = new GameObject("SiteText", typeof(TextMeshProUGUI));
        textGO.transform.SetParent(parent, false);

        var tmp = textGO.GetComponent<TextMeshProUGUI>();
        tmp.text = content;

        // Set font size and color if specified
        tmp.fontSize = attrs.ContainsKey("fontSize") ? int.Parse(attrs["fontSize"]) : 16;
        tmp.color = attrs.ContainsKey("color") ? ColorFromName(attrs["color"]) : Color.black;

        // Optional layout adjustments
        var rect = tmp.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(250, 50); // You can calculate size based on content later
    }

    void CreateImage(Transform parent, Dictionary<string, string> attrs)
    {
        if (!attrs.ContainsKey("src")) return;

        GameObject imgGO = new GameObject("SiteImage", typeof(Image));
        imgGO.transform.SetParent(parent, false);

        var img = imgGO.GetComponent<Image>();
        if (imageLookup.TryGetValue(attrs["src"], out Sprite sprite))
        {
            img.sprite = sprite;
        }

        var rect = imgGO.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(100, 100);
    }
    Vector2 AnchorFromName(string name)
    {
        return name.ToLower() switch
        {
            "topleft" => new Vector2(1, 0),
            "topright" => new Vector2(1, 1),
            "bottomleft" => new Vector2(0, 0),
            "bottomright" => new Vector2(0, 1),
            _ => new Vector2(0, 0)
        };
    }
    Vector2 ParseVector2(string input)
    {
        var parts = input.Split(',');
        if (parts.Length != 2) return Vector2.zero;
        return new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
    }

    Color ColorFromName(string name)
    {
        return name.ToLower() switch
        {
            "white" => Color.white,
            "black" => Color.black,
            "red" => Color.red,
            "green" => Color.green,
            "blue" => Color.blue,
            "gray" => Color.gray,
            _ => Color.magenta //for unknown values
        };
    }
}
