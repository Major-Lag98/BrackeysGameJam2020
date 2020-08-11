using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTrapsInRoom : MonoBehaviour
{

    public TriggerArea Area;

    // Start is called before the first frame update
    void Start()
    {
        BroadcastMessage("SetTrapActive", false, SendMessageOptions.DontRequireReceiver);

        Area.AddOntriggerEnterEvent(ActivateTraps);
        Area.AddOntriggerExitEvent(DeactivateTraps);
    }

    void DeactivateTraps(Collider2D collider, TriggerArea area)
        => BroadcastMessage("SetTrapActive", false);

    void ActivateTraps(Collider2D collider, TriggerArea area)
     => BroadcastMessage("SetTrapActive", true);
}
