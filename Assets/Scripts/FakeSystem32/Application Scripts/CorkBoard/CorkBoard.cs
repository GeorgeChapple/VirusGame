using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To essentially be a 
                        hint board for the player
                        To give them an idea of
                        where to go next
*/
public class CorkBoard : MonoBehaviour
{
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private Material lineMat;
    [SerializeField] private GameObject pagePrefab;

    [SerializeField] private List<CorkBoardPageData> pages = new List<CorkBoardPageData>();
    private Dictionary<CorkBoardPage, LineRenderer> pageAndLine = new Dictionary<CorkBoardPage, LineRenderer>();

    private void Awake()
    {
        SetUp();
    }
    private void Update()
    {
        UpdateLines();
    }
    // Update lines to follow pages
    private void UpdateLines()
    {
        foreach (KeyValuePair<CorkBoardPage, LineRenderer> keyValuePair in pageAndLine)
        {
            RectTransform rect = keyValuePair.Key.GetComponent<RectTransform>();
            keyValuePair.Value.SetPosition(1, rect.anchoredPosition3D + keyValuePair.Key.lineEnd.localPosition);
        }        
    }
    // Set up each page and make a line for each one
    private void SetUp()
    {
        foreach (var page in pages)
        {
            if (!page.active) { continue; } // If hasnt been unlocked, for progression purposes
            GameObject newPage = Instantiate(pagePrefab, contentPanel.transform);
            RectTransform rect = newPage.GetComponent<RectTransform>();
            rect.anchoredPosition = RandomRadialWithMinVal(contentPanel.transform.position, 200, 300);
            newPage.GetComponent<CorkBoardPage>().SetData(page);
            LineRenderer newLine = NewLine(contentPanel.transform.position + new Vector3(0, 0, -9.5f), rect.anchoredPosition3D + newPage.GetComponent<CorkBoardPage>().lineEnd.localPosition);

            pageAndLine.Add(newPage.GetComponent<CorkBoardPage>(), newLine);
        }
    }
    // Spawn a new line for a page
    public LineRenderer NewLine(Vector3 origin, Vector3 destination)
    {
        GameObject newLine = new GameObject("Line", typeof(RectTransform), typeof(LineRenderer));
        LineRenderer lr = newLine.GetComponent<LineRenderer>();
        lr.startWidth = 0.03f; lr.endWidth = 0.03f;
        lr.useWorldSpace = false;
        lr.material = lineMat;
        newLine.transform.SetParent(contentPanel.transform, false);
        Vector3[] positions = new Vector3[2];
        positions[0] = origin;
        positions[1] = destination;
        lr.SetPositions(positions);
        return lr;
    }
    // Calculates a random point with in a radius with a minimum radius, to make sure a page doesnt spawn in middle
    public Vector2 RandomRadialWithMinVal(Vector2 origin, float minRadius, float maxRadius)
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;

        return point;
    }
}
