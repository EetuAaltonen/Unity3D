using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsRunning;
    public float MaxRunSpeed;
    public float MaxWalkSpeed;
    public float JumpVelocity;

    [SerializeField] private GameObject _playerRef;

    private CharacterController _controller;
    private PlayerGravity _gravityScript;
    private PlayerCamera _cameraScript;
    private ChangePOV _povScript;

    private Camera _thirdPersonCamera;
    private float _maxSpeed;
    private float _smoothSpeed;

    private float _turnSmoothTime = 0.2f;
    private float _turnSmoothVelocity;


    void Start() {
        _controller = GetComponent<CharacterController>();
        _gravityScript = GetComponent<PlayerGravity>();
        _cameraScript = GetComponent<PlayerCamera>();
        _povScript = GetComponent<ChangePOV>();

        _thirdPersonCamera = _cameraScript.ThirdPersonCamera;

        StopMovement();
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        var isFirstPerson = _povScript.IsFirstPerson();
        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 0)
        {
            if (!isFirstPerson)
            {
                float targetAngle = _thirdPersonCamera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, _maxSpeed, Time.deltaTime);
        }
        else
        {
            StopRunning();
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, 0, Time.deltaTime);
        }
        _smoothSpeed = Mathf.Clamp(_smoothSpeed, 0, _maxSpeed);
        _controller.Move(move * _smoothSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _gravityScript.IsGrounded)
        {
            _gravityScript.SetVelocity(Mathf.Sqrt(JumpVelocity * -1f * _gravityScript.Gravity));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = !IsRunning;
            _maxSpeed = IsRunning ? MaxRunSpeed : MaxWalkSpeed;
        }
    }

    public float GetMovementSpeed()
    {
        return _smoothSpeed;
    }

    public void StopMovement()
    {
        StopRunning();
        transform.up = Vector3.zero;
        _smoothSpeed = 0;
    }

    public void StopRunning()
    {
        IsRunning = false;
        _maxSpeed = MaxWalkSpeed;
    }
}
