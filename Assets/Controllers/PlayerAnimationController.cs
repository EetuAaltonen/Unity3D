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
        if (Animator != null)
        {
            var movementSpeed = _movementScript.GetMovementSpeed();
            Animator.SetFloat("MovementSpeed", movementSpeed);

            var actionState = _movementScript.GetActionState();
            switch (actionState)
            {
                case ActionState.Attack:
                    {
                        Animator.SetTrigger("Attack");
                        _movementScript.ResetActionState();
                    }
                    break;
            }
        }
    }
}
