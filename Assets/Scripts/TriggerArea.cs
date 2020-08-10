using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    public bool OneShot = false;
    public LayerMask Mask;

    private bool _enterTriggered = false;
    private bool _exitTriggered = false;

    public delegate void OnTriggerEnterDelegate(Collider2D collision);
    public delegate void OnTriggerExitDelegate(Collider2D collision);

    public OnTriggerEnterDelegate OnTriggerEnter;
    public OnTriggerExitDelegate OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;
        // Only fire if not already triggered or it's not a one shot trigger. And the collider's layer matches our layer mask
        if ((!_enterTriggered || !OneShot) && Mask == (Mask | (1 << layer)))
            OnTriggerEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;
        // Only fire if not already triggered or it's not a one shot trigger. And the collider's layer matches our layer mask
        if ((!_exitTriggered || !OneShot) && Mask == (Mask | (1 << layer)))
            OnTriggerExit?.Invoke(collision);
    }
}
