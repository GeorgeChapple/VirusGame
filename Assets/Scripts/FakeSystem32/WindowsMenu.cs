using UnityEngine;

public class WindowsMenu : MonoBehaviour
{
    //this is going to handle the windows menu/start menu
    //this wont be like the real start menu that gives the user a huge library of apps they probably dont need
    //this will just have a set few (because we dont have enough time to make a full on unity os(who would anyway))
    //key ideas: messenger, browser(idk how we'll do that), mail, file explorer, recycling bin, some little game things for story puzzles and such
    //ideas: music player, freaking epic pinball, calendar

    //create list based off preset saved thing like the desktop
    //this will have the quit button, settings button, file explorer button

    private RaycastScript raycastScript;

    private void Awake()
    {
        raycastScript = FindAnyObjectByType<RaycastScript>();
    }
    public void CheckIfClickOnMenu()
    {
        //if clicked outside the menu close menu
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
}
