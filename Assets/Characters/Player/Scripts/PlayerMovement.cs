using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;

    public float Speed = 12f;

    GameObject playerRef;
    ChangePOV povScript;

    void Start() {
        playerRef = GameObject.Find("Player");
        povScript = playerRef.GetComponent<ChangePOV>();
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Controller.Move(move * Speed * Time.deltaTime);
    }
}
