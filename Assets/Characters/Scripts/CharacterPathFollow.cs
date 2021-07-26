using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathFollow : MonoBehaviour
{
    public PathCreator PathCreator;
    public EndOfPathInstruction EndOfPathInstruction;
    public float Speed = 5;
    
    private float _distanceTravelled;
    private CharacterController _controller;

    void Start()
    {
        _distanceTravelled = PathFollowUtilities.GetClosestTravelledDistance(PathCreator, EndOfPathInstruction, transform.position);
        _controller = GetComponent<CharacterController>();
        if (PathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            PathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (PathCreator != null)
        {
            var pathPoint = PathCreator.path.GetPointAtDistance(_distanceTravelled, EndOfPathInstruction);
            if (Vector3.Distance(transform.position, pathPoint) < (Speed + 1f))
            {
                _distanceTravelled += Speed * Time.deltaTime;
            }
            var targetPosition = pathPoint - transform.position;
            targetPosition.y = 0;
            var targetRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Speed * 1.5f * Time.deltaTime);
            _controller.Move(transform.forward * Speed * Time.deltaTime);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        _distanceTravelled = PathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    public float GetSpeed()
    {
        return Speed;
    }
}
