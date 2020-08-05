using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float ProjectileSpeedAtSpawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TriggerArea>().OnTriggerEnter += (collision) =>
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                FireProjectile();
        };
    }

    private void FireProjectile()
    {
        var projectile = ProjectileFactory.CreateProjectile(ProjectilePrefab, transform.rotation.eulerAngles.z, transform.position);
        projectile.GetComponent<Projectile>().MovementSpeed = ProjectileSpeedAtSpawn;
    }
}
