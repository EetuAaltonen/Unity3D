using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator Animator;
    private PlayerMovement _movementScript;

    // Start is called before the first frame update
    void Start()
    {
        _movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var movementSpeed = _movementScript.GetMovementSpeed();
        if (Animator != null)
        {
            Animator.SetFloat("MovementSpeed", movementSpeed);
        }
    }
}
