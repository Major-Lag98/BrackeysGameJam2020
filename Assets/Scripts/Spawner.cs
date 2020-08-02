using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float DistanceFromCenter = 7;
    public float SpawnFrequency = 1.0f;
    public GameObject Projectile;

    private float counter = 0;

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
            var obj = Instantiate(Projectile);
            var randRotation = Random.Range(0, Mathf.PI * 2);
            var x = Mathf.Cos(randRotation) * DistanceFromCenter;
            var y = Mathf.Sin(randRotation) * DistanceFromCenter;

            obj.transform.position = new Vector2(x, y);

            counter -= SpawnFrequency;
        }
    }
}
