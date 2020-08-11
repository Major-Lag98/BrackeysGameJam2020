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
            trigger.AddOntriggerEnterEvent(OnEnterTriggerArea);
        }
    }

    private void OnEnterTriggerArea(Collider2D collision, TriggerArea area)
     => _currCheckpoint = area.gameObject; // Set our current checkpoint

    public GameObject GetCurrentCheckpoint() => _currCheckpoint.gameObject;

    public void MovePlayerToLastCheckpoint()
        => Manager.Instance.PlayerObject.GetComponent<Health>().MovePlayerToCheckpoint(_currCheckpoint.transform);
        

}
