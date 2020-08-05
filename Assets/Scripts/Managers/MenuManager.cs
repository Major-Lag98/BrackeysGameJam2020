using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menu = null;
    [SerializeField]
    GameObject credits = null;



    enum MenuState
    {
        Menu,
        Credits
    }

    MenuState state = MenuState.Menu; //default is the menu


    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Credits()
    {
        state = MenuState.Credits;
        switchState();
    }
    public void BackToMenuFromCredits()
    {
        state = MenuState.Menu;
        switchState();
    }

    public void Exit()
    {
        Application.Quit();
    }

    void switchState()
    {
        switch (state)
        {
            case MenuState.Menu:
                menu.SetActive(true);
                credits.SetActive(false);
                break;
            case MenuState.Credits:
                menu.SetActive(false);
                credits.SetActive(true);
                break;
        }
    }

}
