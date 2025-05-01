using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Purpose           : To handle all the stuff in the windows menu like the file explorer and quit button
*/
public class WindowsMenu : MonoBehaviour
{
    private RaycastScript raycastScript;

    private void Awake()
    {
        raycastScript = FindAnyObjectByType<RaycastScript>();
    }
    public void CheckIfClickOnMenu()
    {
        // If clicked outside the menu, close menu
        if (raycastScript.lastHitObject.name != gameObject.name)
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        if (gameObject.activeInHierarchy){ return; }
        gameObject.SetActive(true);
    }
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
