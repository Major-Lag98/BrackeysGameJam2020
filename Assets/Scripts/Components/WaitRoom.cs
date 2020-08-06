using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitRoom : MonoBehaviour
{
    float waitTime = 10;

    bool triggered = false;

    [SerializeField]
    GameObject entranceDoor = null;
    [SerializeField]
    GameObject exitDoor = null;


    [SerializeField]
    Animator enterAnim = null;
    [SerializeField]
    Animator exitAnim = null;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered == false)
        {

            Debug.Log("Triggered");
            triggered = true;

            exitDoor.GetComponent<Door>().StartMove();

            enterAnim.SetTrigger("Close");
            exitAnim.SetTrigger("Open");

        }
        
    }
}
