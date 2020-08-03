using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A Factory for creating projectiles
/// </summary>
public class ProjectileFactory : ScriptableObject
{

    /// <summary>
    /// Creates a projectile GameObject, sets its target direction (needs to be in degrees), and it's starting position.
    /// Returns a GameObject with the values set.
    /// </summary>
    /// <param name="prefab">The prefab GameObject to instantiate</param>
    /// <param name="targetDirection">The direction (in degrees) to face. 0 is directly right, 180 is directly left. 90 is up, 0 is down</param>
    /// <param name="position">The position for the Projectile to start at</param>
    /// <returns></returns>
    public static GameObject CreateProjectile(GameObject prefab, float targetDirection, Vector2 position)
    {
        var obj = Instantiate(prefab);

        targetDirection = Mathf.Deg2Rad * targetDirection;
        obj.transform.position = new Vector2(position.x, position.y);

        // Set direction of projectile
        Vector2 direction = new Vector3(Mathf.Cos(targetDirection), Mathf.Sin(targetDirection));
        //direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); // Why do I need -90 here? Unity is weird

        return obj;
    }
}
