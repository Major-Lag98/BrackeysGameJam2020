using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
    }

    // Lateupdate bacuase playermovement should be calculated before we try to follow it.
    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
    }
}
