using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallingTrap : MonoBehaviour
{

    public int DamageToPlayer = 10;

    private Vector2 _savedVelocity = new Vector2();

    private Rewind _rewindComp;
    private Rigidbody2D rigibody;

    private TriggerArea trigger;

    // Start is called before the first frame update
    void Start()
    {
        _rewindComp = GetComponent<Rewind>();
        trigger = transform.parent.Find("Trigger").GetComponent<TriggerArea>();
        rigibody = transform.parent.Find("FallingObject").GetComponent<Rigidbody2D>();

        // When we start rewinding, stop simulating our rigidbody
        _rewindComp.OnRewindStart = () => { GetComponent<Rigidbody2D>().simulated = false; };

        // Callback for when we are finished rewinding
        _rewindComp.OnRewindFinished = () => {
            rigibody.velocity = new Vector2(0, 0); //Reset the velocity or it'll keep increasing after rewind
            rigibody.simulated = true;  // Simulate our body again
        };

        // Called when something enters the trigger area
        trigger.AddOntriggerEnterEvent(OnTriggerAreaEntered);
    }

    private void OnTriggerAreaEntered(Collider2D collider, TriggerArea area)
    {
        // If it's the player
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _rewindComp.Recording = true; // Make sure we start recording
                                          // Start simualting the falling object
            rigibody.simulated = true;
            Destroy(trigger.gameObject); // Destroy the trigger because it causes problems
        }
    }

    private void FixedUpdate()
    {
        // Save our velocity each frame
        _savedVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var layer = collision.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Player")) // If it hits the player, hurt them and destroy myself
        {
            collision.gameObject.GetComponent<Health>().Damage(DamageToPlayer);
            DestroyMe();
        }
        else if (layer == LayerMask.NameToLayer("Ground")) // If we hit the ground, just destroy us
        {
            DestroyMe();
        }
    }

    private void OnDisable()
    {
        //_savedVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = _savedVelocity;
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
        ScreenShakeController.Instance.Shake();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && !_rewindComp.Rewinding)
        {
            _rewindComp.Rewinding = true;
        }
    }
}
