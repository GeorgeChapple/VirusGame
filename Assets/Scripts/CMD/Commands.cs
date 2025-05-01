using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Collections.ObjectModel;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class Commands {
    public static string desktopFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    // Opens the given amount of CMD windows
    public static void FlashCMD(int amount) {
        ProcessStartInfo process = new ProcessStartInfo();
        process.FileName = "cmd.exe";
        process.Arguments = "/c exit";
        process.WindowStyle = ProcessWindowStyle.Normal;
        for (int i = 0; i < amount; i++) { 
            Process.Start(process);
        }
    }

    // Shows the user's IP address and such
    public static void ShowIP() {
        ProcessStartInfo process = new ProcessStartInfo();
        process.FileName = "cmd.exe";
        process.Arguments = "/k ipconfig";
        process.WindowStyle = ProcessWindowStyle.Normal;
        Process.Start(process);
    }

    // Looks like hacking
    public static void ShowDir() {
        ProcessStartInfo process = new ProcessStartInfo();
        process.FileName = "cmd.exe";
        process.Arguments = "/k dir/s";
        process.WindowStyle = ProcessWindowStyle.Normal;
        Process.Start(process);
    }

    // If there's a string to search, search it on the internet using Google and the user's default browser app
    public static void Search(string name) {
        if (!(name == null || name == "" || name == " ")) {
            Process.Start("https://www.google.com/search?q=" + name);
        }
    }

    // Creates a folder on the users desktop under the given name
    public static void CreateFolderOnDesktop(string name) {
        ProcessStartInfo process = new ProcessStartInfo();
        process.FileName = "cmd.exe";
        process.Arguments = @"/c md " + desktopFilePath + @"\" + name;
        process.WindowStyle = ProcessWindowStyle.Hidden;
        Process.Start(process);
    }
}
