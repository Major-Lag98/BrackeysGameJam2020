using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    public bool Recording = true;

    public OnRewindFinishedDelegate OnRewindFinished;

    public delegate void OnRewindFinishedDelegate();

    private bool _rewinding = false;

    // If we want premature optimization, we could make this a Vector2[] with a predefined size and slice it when needed.
    // Then we could reuse the array without clearing or garbage collection. Need to use a counter with it though.
    private List<RewindFrame> History = new List<RewindFrame>();

    private int currHistoryIndex = -1;
    private float currTimeCounter = 0;
    private float currTimeDifference = 0;
    private Vector2 beginPosition = new Vector2();
    private Vector3 beginRotation = new Vector3();

    //TODO Maybe include rotation and other needed stuff into history?

    public bool Rewinding { 
        get => _rewinding; 
        set 
        {
            _rewinding = value;
            if (value)
            {
                History.Reverse(); // Reverse the history here to use
                currHistoryIndex = 0; // Reset the counter
                beginPosition = transform.position; // Initially set our begin position
                beginRotation = transform.rotation.eulerAngles; // Initially set our begin position
                currTimeDifference = 0.016f; // Initially set the time difference as 1s/60s
            }
        } 
    }

    public delegate void OnRewindStartDelegate(List<Vector2> history);

    public OnRewindStartDelegate OnRewindStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If we're not rewinding yet and set to record, record our position and rotation.
        if (!Rewinding && Recording)
        {
            // Record our current position and timeStamp
            var currentPosition = new Vector2(transform.position.x, transform.position.y);
            var timeStamp = Time.time;

            // Create the frame and add it
            var frame = new RewindFrame(currentPosition, timeStamp, transform.rotation.eulerAngles);
            History.Add(frame);
        }
        else if(Rewinding)
        {
            RewindMovement();
        }
    }

    void RewindMovement()
    {
        if(currHistoryIndex < History.Count - 1)
        {
            var frame = History[currHistoryIndex]; // Get the frame
            var ratio = currTimeCounter / currTimeDifference; // The ratio of time. This is so we can get a value/1 for the lerp
            transform.position = Vector2.Lerp(beginPosition, frame.Position, ratio); // Lerp our position using that ratio
            transform.eulerAngles = Vector3.Lerp(beginRotation, frame.Rotation, ratio);
            currTimeCounter += Time.deltaTime;

            if(ratio >= 1) // If we're past one then on to the next frame
            {
                currHistoryIndex++;
                currTimeCounter -= currTimeDifference; // Subtract our current time difference, don't set to 0 or else bad
                currTimeDifference = History[currHistoryIndex - 1].Timestamp - History[currHistoryIndex].Timestamp; // Get the difference between our current and last frame
                beginPosition = new Vector2(transform.position.x, transform.position.y); // Our new being position to lerp from
                beginRotation = transform.rotation.eulerAngles; // Our new being position to lerp from
            }
        }
        else
        {
            // Destroy us when we're done
            //Destroy(gameObject);
            Rewinding = false;
            History.Clear();
            OnRewindFinished();
        }
    }
}
