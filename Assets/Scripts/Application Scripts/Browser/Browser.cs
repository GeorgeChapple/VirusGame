using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
*/

public class Browser : MonoBehaviour
{
    [SerializeField] private TMP_InputField URLField;
    public string URL;

    [SerializeField] private List<Website> websites = new List<Website>();

    //[SerializeField] private List<string> websiteNames = new List<string>();
    [SerializeField] private List<string> websiteURLs = new List<string>();

    [SerializeField] private Website currentSite;
    public List<Website> previousSites = new List<Website>();
    public List<Website> futureSites = new List<Website>();

    public int maxSuggestions = 5;
    private List<GameObject> currentSuggestions = new List<GameObject>();
    private string currentSuggestion = "";
    public RectTransform suggestionPanel;
    public GameObject suggestionItemPrefab;

    [Header("Website will be a prefab that gets spawned into here.")]
    [SerializeField] private GameObject contentPanel;


    string closestURL = "";
    private void Start()
    {
        URLField.onValueChanged.AddListener(OnInputChanged);
        suggestionPanel.gameObject.SetActive(false);
        foreach (var website in websites)
        {
            websiteURLs.Add(website.siteUrl);
        }
    }
    private void LateUpdate()
    {
        //this is horrible, ill change it soon
        URLField.onSubmit.RemoveAllListeners();
        URLField.onSubmit.AddListener(delegate { URLInput(currentSuggestion); });
    }
    private void OnInputChanged(string input)
    {
        ClearSuggestions();

        if (string.IsNullOrWhiteSpace(input))
        {
            suggestionPanel.gameObject.SetActive(false);
            return;
        }

        var matches = websiteURLs.Select(option => new { option, distance = LevenshteinDistance(input.ToLower(), option.ToLower()) }).OrderBy(x => x.distance).Take(maxSuggestions).ToList();

        if (matches.Count == 0)
        {
            suggestionPanel.gameObject.SetActive(false);
            return;
        }

        foreach (var match in matches)
        {
            GameObject item = Instantiate(suggestionItemPrefab, suggestionPanel);
            item.GetComponentInChildren<TMP_Text>().text = match.option;
            item.GetComponent<HitEventScript>().hitEvent.AddListener(() => URLInput(match.option));
            item.GetComponent<BoxCollider>().size = new Vector3(suggestionPanel.rect.width, 30, 1);
            currentSuggestions.Add(item);
        }
        suggestionPanel.gameObject.SetActive(true);

        currentSuggestion = currentSuggestions[0].GetComponentInChildren<TMP_Text>().text;
    }
    private void OnSuggestionClicked(string suggestion)
    {
        URLField.text = suggestion;
        ClearSuggestions();
        suggestionPanel.gameObject.SetActive(false);
    }

    private void ClearSuggestions()
    {
        foreach (GameObject item in currentSuggestions)
        {
            Destroy(item);
        }

        currentSuggestions.Clear();
    }
    public void URLInput(string suggestion)
    {
        Debug.Log("search is: " + suggestion);
        //do new website stuff here        

        foreach (var website in websites)
        {
            if (website.siteUrl == suggestion)
            {
                URLField.text = website.siteUrl;
                LoadWebsite(website, true);
                break;
            }
        }
        ClearSuggestions();
        suggestionPanel.gameObject.SetActive(false);
    }
    private void LoadWebsite(Website website, bool newSite)
    {

        if (previousSites.Count > 4)
        {
            previousSites.RemoveAt(4);
            previousSites.Insert(0, currentSite);
            if (newSite) { futureSites.Clear(); }
        }
        else
        {
            previousSites.Insert(0, currentSite);
            if (newSite) { futureSites.Clear(); }
        }
        currentSite = website;
        Destroy(contentPanel.transform.GetChild(0).gameObject);

        Instantiate(website.websitePrefab, contentPanel.transform);

    }
    public void GoBack()
    {
        futureSites.Insert(0, currentSite);
        LoadWebsite(previousSites[0], false);        
    }
    public void GoForward()
    {
        LoadWebsite(futureSites[0], false);
    }

    //first thing that came up when i searched up how to get a closest string, pretty cool
    int LevenshteinDistance(string a, string b)
    {
        int[,] dp = new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++) dp[i, 0] = i;
        for (int j = 0; j <= b.Length; j++) dp[0, j] = j;

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                dp[i, j] = Mathf.Min(dp[i - 1, j] + 1,
                                     dp[i, j - 1] + 1,
                                     dp[i - 1, j - 1] + cost);
            }
        }

        return dp[a.Length, b.Length];
    }
}
