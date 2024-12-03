using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] Destroyed1 Test;
    [SerializeField] Destroyed2 Test2;
    [SerializeField] Destroyed3 Test3;
    [SerializeField] Destroyed4 Test4;
    [SerializeField] Destroyed5 Test5;
    private bool oneCollect = false;
    private bool twoCollect = false;
    private bool threeCollect = false;
    private bool fourCollect = false;
    private bool fiveCollect = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() //For order of the keys - check what is grabbed
    {
        if (Test == null && Test2 != null && Test3 != null && Test4 != null && Test5 != null)
        {
            oneCollect = true;
        }
        if (oneCollect == true && Test2 == null && Test3 != null && Test4 != null && Test5 != null)
        {
            twoCollect = true;
        }
        if (twoCollect == true && Test3 == null && Test4 != null && Test5 != null)
        {
            threeCollect = true;
        }
        if (threeCollect == true && Test4 == null && Test5 != null)
        {
            fourCollect = true;
        }
        if (fourCollect == true && Test5 == null)
        {
            fiveCollect = true;
        }
        if (fiveCollect == true)
        {
            print("ORDER CORRECT");
        }

    }
}