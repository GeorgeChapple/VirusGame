using System.Diagnostics;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class Commands { 
    // If there's a string to search, search it on the internet using Google and the user's default browser app
    public static void Search(string name) {
        if (!(name == null || name == "" || name == " ")) {
            Process.Start("https://www.google.com/search?q=" + name);
        }
    }
}
