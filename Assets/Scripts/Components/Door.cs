using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float TimeToMove = 5f;
    public TriggerArea Trigger;
    public bool ReverseOnTriggerExit = false;

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
                    _moving = true;
                    _counter = 0;
                }
            };

            if (ReverseOnTriggerExit)
            {
                Trigger.OnTriggerExit += collider =>
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        _moving = true;
                        _reverse = true;
                        _counter = 0;
                    }
                };
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_moving && _counter <= TimeToMove)
        {
            if(!_reverse)
                transform.position = Vector3.Lerp(_startPosition, TargetPosition, _counter / TimeToMove);
            else
                transform.position = Vector3.Lerp(TargetPosition, _startPosition, _counter / TimeToMove);

            _counter += Time.deltaTime;
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
