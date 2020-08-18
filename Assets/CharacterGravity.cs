using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity : MonoBehaviour
{
    public CharacterController Controller;
    public float CheckDistanceToGround;
    public LayerMask GroundMask;
    public Transform ModelRef;

    private float _gravity = -9.81f;
    private float _gravityScale = 2.5f;
    private bool _isGrounded;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(ModelRef.position, Vector3.down, CheckDistanceToGround);

        if (_isGrounded && _velocity.y < 0) {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * _gravityScale * Time.deltaTime;
        Controller.Move(_velocity * Time.deltaTime);
    }

    public float GetGravity()
    {
        return _gravity * _gravityScale;
    }

    public void SetVelocity(float velocity)
    {
        _velocity.y = velocity;
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }
}
