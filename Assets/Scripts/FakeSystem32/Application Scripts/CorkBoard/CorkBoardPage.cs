using TMPro;
using UnityEngine;

public class CorkBoardPage : MonoBehaviour
{
    public CorkBoard board;
    private CorkBoardPageData data;
    [SerializeField] private TextMeshProUGUI text;
    public Transform lineEnd;

    private void Start()
    {
        if (data != null)
        {
            text.text = data.Text;
        }
    }
    public void SetData(CorkBoardPageData _data) => data = _data;
}