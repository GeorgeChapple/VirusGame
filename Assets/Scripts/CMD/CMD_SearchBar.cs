using System;
using System.Diagnostics;
using UnityEngine;

public class CMD_SearchBar : MonoBehaviour { 
    public void Search(string name) {
        Process.Start("https://www.google.com/search?q=" + name); ;
    }
}
