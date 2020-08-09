using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsRunning;
    public float MaxRunSpeed;
    public float MaxWalkSpeed;
    public float JumpVelocity;

    private CharacterController _controller;
    private PlayerGravity _gravityScript;
    private PlayerCamera _cameraScript;
    private ChangePOV _povScript;
    private Transform _playerRef;

    private Camera _thirdPersonCamera;
    private float _maxSpeed;
    private float _smoothSpeed;
    private float _speedInterpolation = 3f;

    private float _modelTurnInterpolation = 5f;
    private float _turnSmoothTime = 0.2f;
    private float _turnSmoothVelocity;

    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _gravityScript = GetComponent<PlayerGravity>();
        _cameraScript = GetComponent<PlayerCamera>();
        _povScript = GetComponent<ChangePOV>();

        _playerRef = transform.Find("Player");
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

                float modelAngle;
                float modelTargetAngle = 90f - (z * 45f);
                float playerYRotation = _playerRef.localRotation.eulerAngles.y;
                playerYRotation = (playerYRotation > 180) ? playerYRotation - 360 : playerYRotation;
                modelAngle = Mathf.Lerp(playerYRotation, modelTargetAngle * x, _modelTurnInterpolation * Time.deltaTime);
                _playerRef.localRotation = Quaternion.Euler(0f, modelAngle, 0f);
            }
            else
            {
                _playerRef.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, _maxSpeed, _speedInterpolation * Time.deltaTime);
            _controller.Move(move.normalized * _smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            StopRunning();
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, 0, (_speedInterpolation * 2) * Time.deltaTime);
            _controller.Move(transform.forward.normalized * _smoothSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && _gravityScript.IsGrounded())
        {
            _gravityScript.SetVelocity(Mathf.Sqrt(-JumpVelocity * _gravityScript.GetGravity()));
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
        _gravityScript.SetVelocity(0f);
        _smoothSpeed = 0;
    }

    public void StopRunning()
    {
        IsRunning = false;
        _maxSpeed = MaxWalkSpeed;
    }
}
