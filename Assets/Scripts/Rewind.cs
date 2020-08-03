using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    private bool _rewinding = false;

    // If we want premature optimization, we could make this a Vector2[] with a predefined size and slice it when needed.
    // Then we could reuse the array without clearing or garbage collection. Need to use a counter with it though.
    private List<Vector2> History = new List<Vector2>();

    public bool Rewinding { 
        get => _rewinding; 
        set 
        {
            _rewinding = value;
            if (value)
            {
                History.Reverse();
                RewindStart(History); // Invoke delegate with history
                History = new List<Vector2>(); //Instead of clearing we'll just make a new list
            }
        } 
    }

    public delegate void OnRewindStart(List<Vector2> history);

    public OnRewindStart RewindStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If we're not rewinding yet, record our position.
        if (!Rewinding)
        {
            History.Add(new Vector2(transform.position.x, transform.position.y));
        }
    }
}
