using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallingTrap : MonoBehaviour
{

    private Rewind _rewindComp;
    // Start is called before the first frame update
    void Start()
    {
        _rewindComp = GetComponent<Rewind>();
        var trigger = transform.parent.Find("Trigger");
        var rigidBody = transform.parent.Find("FallingObject").GetComponent<Rigidbody2D>();

        // When we start rewinding, stop simulating our rigidbody
        _rewindComp.OnRewindStart = () => { GetComponent<Rigidbody2D>().simulated = false; };

        // Callback for when we are finished rewinding
        _rewindComp.OnRewindFinished = () => {
            rigidBody.velocity = new Vector2(0, 0); //Reset the velocity or it'll keep increasing after rewind
            rigidBody.simulated = true;  // Simulate our body again
        };

        // Called when something enters the trigger area
        trigger.GetComponent<TriggerArea>().OnTriggerEnter += (Collider2D collision) =>
        {
            // If it's the player
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _rewindComp.Recording = true; // Make sure we start recording
                // Start simualting the falling object
                rigidBody.simulated = true;
                Destroy(trigger.gameObject); // Destroy the trigger because it causes problems
            }
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var layer = collision.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Player")) // If it hits the player, hurt them and destroy myself
        {
            collision.gameObject.GetComponent<Health>().Damage(1);
            Destroy(gameObject);
        }
        else if (layer == LayerMask.NameToLayer("Ground")) // If we hit the ground, just destroy us
            Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && !_rewindComp.Rewinding)
        {
            _rewindComp.Rewinding = true;
        }
    }
}
