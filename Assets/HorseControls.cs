using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseControls : MonoBehaviour
{
    private CharacterController _controller;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("ArrowHorizontal");
        float z = Input.GetAxisRaw("ArrowVertical");
        float movementSpeed = 5f;
        Vector3 move = transform.right * x + transform.forward * z;
        if (z != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime);
            _controller.Move(transform.forward * movementSpeed * Time.deltaTime);
        }
    }
}
