using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallingTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.Find("Trigger").GetComponent<TriggerArea>().OnTriggerEnter += (Collider2D collision) =>
        {
            print("Hello");
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                transform.parent.Find("FallingObject").GetComponent<Rigidbody2D>().simulated = true;
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
}
