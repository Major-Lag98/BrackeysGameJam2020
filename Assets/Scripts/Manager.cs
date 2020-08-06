using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get => instance; private set => instance = value; }

    public static GameObject Player;
    public GameObject PlayerObject;

    private static Manager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Player = PlayerObject; // assign the player to the static field
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
