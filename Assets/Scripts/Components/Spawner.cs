using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float DistanceFromCenter = 7;
    public float SpawnFrequency = 1.0f;
    public GameObject Projectile;

    private float counter = 0;

    private Vector2 targetPosition = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter > SpawnFrequency)
        {
            // gets the position using a random rotation around a circle * the distance from center
            var randRotation = Random.Range(0, Mathf.PI * 2);
            var x = Mathf.Cos(randRotation) * DistanceFromCenter;
            var y = Mathf.Sin(randRotation) * DistanceFromCenter;
            var position = new Vector2(x, y);

            // Set direction of projectile
            Vector3 direction = new Vector3(targetPosition.x, targetPosition.y, 0) - new Vector3(position.x, position.y, 0);
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Create the projectile
            ProjectileFactory.CreateProjectile(Projectile, angle, new Vector2(x, y));

            counter -= SpawnFrequency;
        }
    }
}
