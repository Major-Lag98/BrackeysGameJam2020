using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Affects{ Health, Energy}

    public Affects AffectsStat;
    public float PickupAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (AffectsStat == Affects.Health)
                print("Nothing for now"); //TODO implement heal
            //collision.gameObject.GetComponent<Health>().Add(PickupAmount);
            else if (AffectsStat == Affects.Energy)
                collision.gameObject.GetComponent<Energy>().AddEnergy(PickupAmount);

            Destroy(gameObject);
        }
    }
}
