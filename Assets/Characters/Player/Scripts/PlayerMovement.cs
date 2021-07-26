using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterGravity))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(ChangePOV))]
[RequireComponent(typeof(CharacterStats))]
public class PlayerMovement : MonoBehaviour
{
    public bool IsRunning;
    public float JumpVelocity;

    private CharacterController _controller;
    private CharacterGravity _gravityScript;
    private PlayerCamera _cameraScript;
    private ChangePOV _povScript;

    private PlayerStats _playerStats;
    private Camera _thirdPersonCamera;
    private Transform _playerRef;

    private float _maxSpeed;
    private float _smoothSpeed;
    private float _speedInterpolation = 3f;

    private float _modelTurnInterpolation = 5f;
    private float _turnSmoothTime = 0.2f;
    private float _turnSmoothVelocity;

    private ActionState _actionState = ActionState.Idle;

    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _gravityScript = GetComponent<CharacterGravity>();
        _cameraScript = GetComponent<PlayerCamera>();
        _povScript = GetComponent<ChangePOV>();
        _playerStats = GetComponent<PlayerStats>();

        _playerRef = transform.Find("PlayerModel");
        _thirdPersonCamera = _cameraScript.ThirdPersonCamera;

        StopMovement();
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (z < 0)
        {
            StopRunning();
        }
        RotatePlayerModel(x, z, move);

        if (move.magnitude > 0)
        {
            if (!_povScript.IsFirstPerson())
            {
                float targetAngle = _thirdPersonCamera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            }
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, _maxSpeed, _speedInterpolation * Time.deltaTime);
            _controller.Move(move.normalized * _smoothSpeed * Time.deltaTime);
        }
        else
        {
            StopRunning();
            _smoothSpeed = Mathf.Lerp(_smoothSpeed, 0, _speedInterpolation * Time.deltaTime);
            _controller.Move(move.normalized * _smoothSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && _gravityScript.IsGrounded())
        {
            _gravityScript.SetVelocity(Mathf.Sqrt(-JumpVelocity * _gravityScript.GetGravity()));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = !IsRunning;
            _maxSpeed = IsRunning ? _playerStats.MaxRunningSpeed.GetValue() : _playerStats.MaxWalkingSpeed.GetValue();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _actionState = ActionState.Attack;
        } else if (Input.GetMouseButtonDown(1))
        {
            _actionState = ActionState.Idle;
        }
    }

    public ActionState GetActionState()
    {
        return _actionState;
    }

    public void ResetActionState()
    {
        _actionState = ActionState.Idle;
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
        _maxSpeed = _playerStats.MaxWalkingSpeed.GetValue();
    }

    private void RotatePlayerModel(float x, float z, Vector3 move)
    {
        float modelAngle;
        float playerYRotation = _playerRef.localRotation.eulerAngles.y;
        playerYRotation = RotationUtilities.ScaleRotation(playerYRotation);
        if (move.magnitude > 0)
        {
            float modelTargetAngle = 90f - (z * 45f);
            playerYRotation = RotationUtilities.ScaleRotation(playerYRotation);
            modelAngle = Mathf.Lerp(playerYRotation, modelTargetAngle * x, _modelTurnInterpolation * Time.deltaTime);
            _playerRef.localRotation = Quaternion.Euler(0f, modelAngle, 0f);
        }
        else
        {
            if (_povScript.IsFirstPerson())
            {
                modelAngle = Mathf.Lerp(playerYRotation, 0, _modelTurnInterpolation * Time.deltaTime);
                _playerRef.localRotation = Quaternion.Euler(0f, modelAngle, 0f);
            }
        }
    }
}
