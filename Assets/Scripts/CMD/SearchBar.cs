using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class SearchBar : MonoBehaviour {
    // Searches whatever the user puts into the text field on the internet
    public void SearchTheActualInternet(string name) {
        Commands.Search(name);
    }
}
