using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class HealthBar : MonoBehaviour
{

    Transform bar;


    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        
    }

    public void SetSize(float percentSize)
    {
        bar.localScale = new Vector3(percentSize, 1f);
    }
}
