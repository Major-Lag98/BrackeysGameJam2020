using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public List<TriggerArea> CheckpointTriggers;

    private static GameObject _currTrigger = null;

    public static CheckpointController Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;

        // Add a callback for each of our checkpoints
        CheckpointTriggers.ForEach(trigger =>
        {
            // When something enters the trigger
            trigger.OnTriggerEnter = collision =>
            {
                // If it's the player
                if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    _currTrigger = trigger.gameObject;
                    CheckpointTriggers.Remove(trigger); // Remove from our list since we've already saved here.
                }
            };
        });
    }


    public static GameObject GetCurrentCheckpoint() => _currTrigger.gameObject;

}
