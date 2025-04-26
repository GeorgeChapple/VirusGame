using System;
using System.Diagnostics;
using UnityEngine;

public class CMD_SearchBar : MonoBehaviour { 
    public void CreateDesktopFile(string name) {
        ProcessStartInfo process = new ProcessStartInfo();
        process.FileName = "cmd.exe";
        process.WindowStyle = ProcessWindowStyle.Hidden;
        process.Arguments = @"/c md " + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + name;
        Process.Start(process);
    }
}
