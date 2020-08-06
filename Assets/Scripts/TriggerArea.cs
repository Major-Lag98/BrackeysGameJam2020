using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    public bool OneShot = false;

    private bool _enterTriggered = false;
    private bool _exitTriggered = false;

    public delegate void OnTriggerEnterDelegate(Collider2D collision);
    public delegate void OnTriggerExitDelegate(Collider2D collision);

    public OnTriggerEnterDelegate OnTriggerEnter;
    public OnTriggerExitDelegate OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_enterTriggered || !OneShot)
            OnTriggerEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_exitTriggered || !OneShot)
            OnTriggerExit?.Invoke(collision);
    }
}
