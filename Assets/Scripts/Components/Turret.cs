using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A turret the shoots projectiles. Can be killed by rewinding a projectile into it.
/// Reverse the object by setting the X scale to -1 (makes it face left). positive 1 is right.
/// </summary>
public class Turret : MonoBehaviour
{
    public float FireRate = 2f;
    public GameObject FirePosition; // Position to fire from
    public GameObject Projectile; // The projectile prefab

    private float counter = 0;

    // Update is called once per frame
    void Update()
    {
        // Firing our projectile
        counter += Time.deltaTime;
        if(counter > FireRate) 
        {
            ProjectileFactory.CreateProjectile(Projectile, transform.localScale.x > 0 ? 0 : 180, FirePosition.transform.position);
            counter -= FireRate;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // If a projectile hits us, destroy both of us
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
    //    {
    //        //TODO probably a better way to do this but it was easy at the time
    //        var isRewinding = collision.gameObject.GetComponent<Rewind>().Rewinding;
    //        if (isRewinding) // If the projectile is rewinding and it hits us, kill us both
    //        {
    //            Destroy(gameObject);
    //            Destroy(collision.gameObject);
    //        }
    //    }
    //}
}
