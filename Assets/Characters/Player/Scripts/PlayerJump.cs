using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpHeight = 3f;

    GameObject playerRef;
    PlayerGravity gravityScript;

    void Start()
    {
        playerRef = GameObject.Find("Player");
        gravityScript = playerRef.GetComponent<PlayerGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && gravityScript.IsGrounded)
        {
            gravityScript.Velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravityScript.Gravity);
        }
    }
}
