using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Affects{ Health, Energy}

    public Affects AffectsStat;
    public int PickupAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) // If it's the player colliding with us
        {
            if (AffectsStat == Affects.Health) // Heal
                collision.gameObject.GetComponent<Health>().Heal(PickupAmount);
            else if (AffectsStat == Affects.Energy) // Add energy
                collision.gameObject.GetComponent<Energy>().AddEnergy(PickupAmount);

            Destroy(gameObject);
        }
    }
}
