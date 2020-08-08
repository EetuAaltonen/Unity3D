using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public CharacterController Controller;

    public float Gravity = -9.81f;
    private float GravityScale = 2f;
    public bool IsGrounded;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    
    private Vector3 _velocity;

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (IsGrounded && _velocity.y < 0) {
            _velocity.y = -2f;
        }

        _velocity.y += Gravity * GravityScale * Time.deltaTime;
        Controller.Move(_velocity * Time.deltaTime);
    }

    public void SetVelocity(float velocity)
    {
        _velocity.y = velocity;
    }
}
