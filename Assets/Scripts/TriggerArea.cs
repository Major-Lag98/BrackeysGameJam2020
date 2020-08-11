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

    public delegate void OnTriggerEnterDelegate(Collider2D collision, TriggerArea area);
    public delegate void OnTriggerExitDelegate(Collider2D collision, TriggerArea area);

    private OnTriggerEnterDelegate OnTriggerEnter;
    private OnTriggerExitDelegate OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;
        // Only fire if not already triggered or it's not a one shot trigger. And the collider's layer matches our layer mask
        if ((!_enterTriggered || !OneShot) && Mask == (Mask | (1 << layer)))
            OnTriggerEnter?.Invoke(collision, this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;
        // Only fire if not already triggered or it's not a one shot trigger. And the collider's layer matches our layer mask
        if ((!_exitTriggered || !OneShot) && Mask == (Mask | (1 << layer)))
            OnTriggerExit?.Invoke(collision, this);
    }

    /// <summary>
    /// Adds a OnTriggerEnterDelegate delegate to be multicasted. If no duplicates are intented, then a
    /// function should be passed and not a lambda or anonymous delegate
    /// </summary>
    /// <param name="del">The delegate to add to the multicast</param>
    public void AddOntriggerEnterEvent(OnTriggerEnterDelegate del)
     => OnTriggerEnter += del;

    /// <summary>
    /// Adds a OnTriggerExitDelegate delegate to be multicasted. If no duplicates are intented, then a
    /// function should be passed and not a lambda or anonymous delegate
    /// </summary>
    /// <param name="del">The delegate to add to the multicast</param>
    public void AddOntriggerExitEvent(OnTriggerExitDelegate del)
     => OnTriggerExit += del;


    /// <summary>
    /// Attemps to remove a OnTriggerEnterDelegate from the multicase
    /// </summary>
    /// <param name="del">The delegate to remove</param>
    public void RemoveOntriggerEnterEvent(OnTriggerEnterDelegate del)
     => OnTriggerEnter -= del;

    /// <summary>
    /// Attemps to remove a OnTriggerExitDelegate from the multicase
    /// </summary>
    /// <param name="del">The delegate to remove</param>
    public void RemoveOntriggerExitEvent(OnTriggerExitDelegate del)
     => OnTriggerExit -= del;

}
