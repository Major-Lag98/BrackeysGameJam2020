using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component the will make a GameObject move in the forward (or up for 2D?) direction. Make sure to set it's rotation when instantiated
/// </summary>
public class Projectile : MonoBehaviour
{
    public AnimationCurve PauseCurve; // The animation curve to apply movement falloff. Should range from 0 to 1
    public float MovementSpeed = 5.0f;
    private bool StartRewind = false; // A flag for starting the rewind

    private Rewind rewind; // The Rewind script

    private Vector2[] points = new Vector2[0]; // An array of points to follow when we are rewinding.
    private int currIndex = 0; // Current index of the points above.

    private float currRewindCounter = 0; //Counter used for applying the animation curve which slows movement until stopping.
    private const float timeToStop = 0.3f;
    private readonly float lifespan = 10f;

    private float startLifeTime = 0f;

    //private delegate void OnStartRewind();

    //private OnStartRewind onStartRewind;

    // Start is called before the first frame update
    void Start()
    {
        rewind = GetComponent<Rewind>();

        // The delegate for our Rewind component to call when we assign Rewind.Rewdining = true
        rewind.RewindStart = (List<Vector2> history) => { points = history.ToArray(); currIndex = 0; };

        startLifeTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rewind.Rewinding) // Regular move
            RegularMove();
        else // Rewinding move
            RewindMove();
    }

    private void RegularMove() 
    {
        var moveSpeed = MovementSpeed; // temp cache for applying the animation curve
        if (StartRewind)
        {
            if (currRewindCounter < timeToStop)
            {
                moveSpeed = PauseCurve.Evaluate(currRewindCounter) * moveSpeed; // Apply our curve
                currRewindCounter += Time.deltaTime;
            }
            else // If >= 1, start our rewind
            {
                rewind.Rewinding = true; // This will trigger the delegate we made in our Start()
            }
        }


        // Move using our (possible affected) speed
        //transform.position = Vector2.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
        transform.position = transform.position + transform.up * moveSpeed * Time.deltaTime; // Another weird unity thing. We move in the *up* direction

        // If our start time + lifespan is greater than the current time, kill us!
        if (Time.time >= startLifeTime + lifespan)
            Destroy(gameObject);
    }

    private void RewindMove()
    {
        if (currIndex >= 0)
        {
            var currPoint = points[currIndex]; // get our curr point

            transform.position = Vector2.MoveTowards(transform.position, currPoint, MovementSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, currPoint) <= MovementSpeed * 2 * Time.deltaTime) // Move until close
            {
                if (currIndex < points.Length - 1)
                    currIndex += 1;
                else
                    Destroy(this.gameObject); // When we finish rewinding, destroy us
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
            StartRewind = true;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collider entered");

        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
