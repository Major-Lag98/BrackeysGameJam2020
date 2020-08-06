using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    enum State
    {
        Play,
        Pause
    }

    State state = State.Play;

    public delegate void OnSceneSwitchedDelegate();

    public OnSceneSwitchedDelegate OnSceneSwitched;
    public OnSceneSwitchedDelegate PreSceneSwitched;

    [SerializeField]
    GameObject pauseMenu = null;


    [SerializeField]
    GameObject pastTiles = null;
    [SerializeField]
    GameObject presentTiles = null;

    bool inPresent = true;

    // Start is called before the first frame update
    void Start()
    {
        OnSceneSwitched += () =>
        {
            if (inPresent)
            {
                pastTiles.SetActive(false);
                presentTiles.SetActive(true);
            }
            else
            {
                pastTiles.SetActive(true);
                presentTiles.SetActive(false);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PreSceneSwitched?.Invoke();
            inPresent = !inPresent;
            OnSceneSwitched();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == State.Play)
            {
                state = State.Pause;
            }
            else if (state == State.Pause)
            {
                state = State.Play;
            }
            SwitchState();
        }
    }


    public void Continue()
    {
        state = State.Play;
        SwitchState();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void SwitchState()
    {
        switch (state)
        {
            case State.Play:
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                break;
            case State.Pause:
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                break;
        }
    }
}
