using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArrowDirection : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        var target = new Vector3(2, 0, 0);
        var bottom = target + new Vector3(-0.5f, -0.25f, 0);
        var top = target + new Vector3(-0.5f, 0.25f, 0);

        //Quaternion.LookRotation().eulerAngles;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0,0,0) , target);
        Gizmos.DrawLine(target, bottom);
        Gizmos.DrawLine(target, top);

    }
}
