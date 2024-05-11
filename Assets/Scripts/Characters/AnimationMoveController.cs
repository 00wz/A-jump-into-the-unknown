using UnityEngine;

[RequireComponent(typeof(Animator),typeof(GroundedCheck),typeof(Rigidbody))]
public class AnimationMoveController : MonoBehaviour
{
    [SerializeField]
    private float MaxRotateSpeed = 360f;
    [SerializeField]
    private AudioClip LandingAudioClip;
    [SerializeField]
    private AudioClip[] FootstepAudioClips;
    [SerializeField]
    private AudioClip[] JumpAudioClips;
    [Range(0, 1)] 
    public float FootstepAudioVolume = 0.5f;
    [Range(0, 1)] 
    public float JumpAudioVolume = 1f;
    [SerializeField]
    private bool Grounded = true;
    [SerializeField]
    public bool RotateToMovementDirection = true;
    [SerializeField]
    private SmoothedSpeedCheck SmoothedSpeedCheck;

    private Animator _animator;
    private GroundedCheck _groundedCheck;
    private Vector3 _smoothedSpeed => _rigidbody.velocity;//SmoothedSpeedCheck.SmoothedSpeed;
    private Rigidbody _rigidbody;

    private const float VELOCITY_ERROR = 0.001f;

    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _groundedCheck = GetComponent<GroundedCheck>();
        _rigidbody = GetComponent<Rigidbody>();
        AssignAnimationIDs();
        //SmoothedSpeedCheck = GetComponent<SmoothedSpeedCheck>();
    }

    private void Update()
    {
        GroundedCheck();
        Move();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
    }

    private void GroundedCheck()
    {
        bool _currentGrounded = _groundedCheck.Grounded;
        if (Grounded && !_currentGrounded)
        {
            _animator.SetBool(_animIDGrounded, false);
            if (_smoothedSpeed.y > 0f)
            {
                _animator.SetBool(_animIDJump, true);
                PlayJampSound();
            }
            else
                _animator.SetBool(_animIDFreeFall, true);
        }
        else if (!Grounded && _currentGrounded)
        {
            _animator.SetBool(_animIDGrounded, true);
            _animator.SetBool(_animIDJump, false);
            _animator.SetBool(_animIDFreeFall, false);
        }
        Grounded = _currentGrounded;
    }

    private void Move()
    {
        Vector3 smoothedHorizontalSpeed = new Vector3(_smoothedSpeed.x, 0.0f, _smoothedSpeed.z);
        float smoothedHorizontalSpeedMagnitude = smoothedHorizontalSpeed.magnitude;

        if (RotateToMovementDirection && smoothedHorizontalSpeedMagnitude > VELOCITY_ERROR)// smoothedHorizontalSpeed != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(_smoothedSpeed.x, _smoothedSpeed.z) * Mathf.Rad2Deg;
            targetRotation = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation, MaxRotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0.0f, targetRotation, 0.0f);
        }

        _animator.SetFloat(_animIDSpeed,
            smoothedHorizontalSpeedMagnitude < 0.01f ? 0f : smoothedHorizontalSpeedMagnitude);
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (true || animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (true || animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.position, FootstepAudioVolume);
        }
    }

    private void PlayJampSound()
    {
        if (JumpAudioClips.Length > 0)
        {
            var index = Random.Range(0, JumpAudioClips.Length);
            AudioSource.PlayClipAtPoint(JumpAudioClips[index], transform.position, JumpAudioVolume);
        }
    }

}