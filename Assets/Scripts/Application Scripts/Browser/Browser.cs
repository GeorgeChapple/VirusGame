using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Browser : MonoBehaviour
{
    [SerializeField] private TMP_InputField URLField;
    public string URL;

    [Header("Website will be a prefab that gets spawned into here.")]
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private TMP_Dropdown urlDropdown;

    [SerializeField] private List<Website> websites = new List<Website>();
    [SerializeField] private List<string> websiteNames = new List<string>();
    string closestURL = "";
    private void Start()
    {
        foreach (var website in websites)
        {
            websiteNames.Add(website.siteName);
        }
    }
    public void URLSelect()
    {
        urlDropdown.ClearOptions();
        urlDropdown.AddOptions(websiteNames);
        urlDropdown.Show();
    }
    public void URLUpdate()
    {
        closestURL = FindClosestMatch(URL, websiteNames);
    }
    public void URLInput(string name)
    {
        //do new website stuff here        
        string closestName = FindClosestMatch(name, websiteNames);

        foreach (var website in websites)
        {
            if (website.siteName == closestName)
            {
                URLField.text = website.siteUrl;
                LoadWebsite(website);
                break;
            }
        }
        urlDropdown.Hide();
    }
    private void LoadWebsite(Website website)
    {
        Destroy(contentPanel.transform.GetChild(0).gameObject);

        Instantiate(website.websitePrefab, contentPanel.transform);

    }

    string FindClosestMatch(string target, List<string> list)
    {
        string closest = null;
        int minDistance = int.MaxValue;

        foreach (string s in list)
        {
            int dist = LevenshteinDistance(target, s);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = s;
            }
        }

        return closest;
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
