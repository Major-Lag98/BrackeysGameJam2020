using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera CMcam;

    [SerializeField]
    GameObject door;

    bool triggered = false;

    [SerializeField]
    Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return; //stop using trigger if we already changed camera location
        
        CMcam.LookAt = target;
        CMcam.Follow = target;
        CMcam.m_Lens.OrthographicSize = 15;

        door.SetActive(true);
    }

}
