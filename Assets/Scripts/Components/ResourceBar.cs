using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    Transform bar;

    // Start is called before the first frame update
    void Awake() //Awake is called before start. Will be problems if you try to find "Bar" later in execution
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float percentSize)
    {
        bar.localScale = new Vector3(percentSize, 1f);
    }
}
