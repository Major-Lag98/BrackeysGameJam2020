using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private static GameObject _currCheckpoint = null;

    public static CheckpointController Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;

        // Add a callback for each of our checkpoints
        foreach (Transform child in transform)
        {
            // Get the trigger
            var trigger = child.gameObject.GetComponent<TriggerArea>();
            // When something enters the trigger
            trigger.OnTriggerEnter = collision =>
            {
                // If it's the player
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    _currCheckpoint = trigger.gameObject; // Set our current checkpoint
                }
            };


        }
    }

    public GameObject GetCurrentCheckpoint() => _currCheckpoint.gameObject;

}
