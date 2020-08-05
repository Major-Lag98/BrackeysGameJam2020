using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerArea : MonoBehaviour
{
    public delegate void OnTriggerEnterDelegate(Collider2D collision);
    public delegate void OnTriggerExitDelegate(Collider2D collision);

    public OnTriggerEnterDelegate OnTriggerEnter;
    public OnTriggerExitDelegate OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision);
    }
}
