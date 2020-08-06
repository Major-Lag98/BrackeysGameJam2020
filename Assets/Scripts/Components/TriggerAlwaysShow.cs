using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TriggerAlwaysShow : MonoBehaviour
{

    public Color TriggerColor = Color.yellow;

    private BoxCollider2D colliderComp;

    // Start is called before the first frame update
    void Start()
    {
        this.colliderComp = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        var bounds = colliderComp.bounds;
        Gizmos.color = TriggerColor;
        if(colliderComp != null)
            Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
