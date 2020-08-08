using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float TimeToMove = 5f;
    public TriggerArea Trigger;
    public bool ReverseOnTriggerExit = false;
    public bool ScreenShakeOnFinish = false;

    private float _counter = 0f;
    private bool _moving = false;
    private bool _reverse = false;
    private Vector2 _startPosition;
    private Animator _animator;

    [Tooltip("Move area")]
    [SerializeField]
    public Vector3 TargetPosition;

    private AnimatorState animatorState;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _startPosition = transform.position;

        if(Trigger != null)
        {
            Trigger.OnTriggerEnter += (collider) =>
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    _moving = true; // Set us to moving
                    _reverse = false; // Make sure that we're not reversing anymore
                }
            };

            if (ReverseOnTriggerExit) // If we want to reverse when leaving the trigger
            {
                Trigger.OnTriggerExit += collider => // Add a delegate event
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        _moving = true;
                        _reverse = true;
                    }
                };
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_moving && (_counter <= TimeToMove && _counter >= 0))
        {
            if(!_reverse)
                _counter = Mathf.Clamp(_counter + Time.deltaTime, 0, TimeToMove);

            else
                _counter = Mathf.Clamp(_counter - Time.deltaTime, 0, TimeToMove);


            transform.position = Vector3.Lerp(_startPosition, TargetPosition, _counter / TimeToMove);

            // If we're finished and we're not going to reverse and we want to shake, shake!
            if (_counter >= TimeToMove && !ReverseOnTriggerExit && ScreenShakeOnFinish && _moving)
            {
                ScreenShakeController.Instance.Shake();
                _moving = false;
            }
        }
    }

    public void StartMove()
    {
        _startPosition = transform.position;
        _moving = true;
    }

    private void OnDisable()
    {
        //var info = _animator.GetCurrentAnimatorClipInfo(0);
        //animatorState = new AnimatorState(0, info[0].clip.name, _animator.playbackTime);
    }

    private void OnEnable()
    {
        //_animator.Play(animatorState.stateName);
        //_animator.StartPlayback();
        //_animator.playbackTime = animatorState.playbackTime;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }

    private struct AnimatorState
    {
        public int layer;
        public string stateName;
        public float playbackTime;

        public AnimatorState(int layer, string stateName, float playbackTime)
        {
            this.layer = layer;
            this.stateName = stateName;
            this.playbackTime = playbackTime;
        }
    }
}
