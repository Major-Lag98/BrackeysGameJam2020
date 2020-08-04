using UnityEngine;

public class RollingBoulder : MonoBehaviour
{
    public AnimationCurve curve;

    private Rewind _rewind;

    private bool startRewind = false;
    private float curveCounter = 0;
    private Animator animator;

    public bool StartRewind { get => startRewind; 
        set
        {
            startRewind = value;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _rewind = GetComponent<Rewind>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartRewind && StopRolling() && !_rewind.Rewinding)
            _rewind.Rewinding = true;
    }

    private bool StopRolling()
    {
        if (curveCounter < 0.3)
        {
            animator.speed = curve.Evaluate(curveCounter);
            curveCounter += Time.deltaTime;
            return false;
        }

        animator.speed = 0;
        return true;
    }

    private void OnMouseOver()
    {
        print("Mouse over");
        if (Input.GetMouseButton(0) && !StartRewind)
            StartRewind = true;
    }
}
