using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject pastTiles = null;
    [SerializeField]
    GameObject presentTiles = null;

    bool inPresent = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            inPresent = !inPresent;

        }

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
            


    }
}
