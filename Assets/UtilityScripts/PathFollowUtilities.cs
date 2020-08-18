using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFollowUtilities
{
    public static Vector3 GetPathClosestPoint(PathCreator pathCreator, EndOfPathInstruction endOfPath, Vector3 targetPosition)
    {
        return CalculateClosestPoint(pathCreator, endOfPath, targetPosition).Position;
    }

    public static float GetClosestTravelledDistance(PathCreator pathCreator, EndOfPathInstruction endOfPath, Vector3 targetPosition)
    {
        return CalculateClosestPoint(pathCreator, endOfPath, targetPosition).TravelledDistance;
    }

    private static ClosestPoint CalculateClosestPoint(PathCreator pathCreator, EndOfPathInstruction endOfPath, Vector3 targetPosition)
    {
        var stepScale = pathCreator.path.length % 10;
        stepScale = Mathf.Clamp(stepScale, 1, 500f);
        var pathStep = pathCreator.path.length / stepScale;
        ClosestPoint closestPoint = new ClosestPoint(Vector3.zero, -1, 0);
        for (int i = 0; i < stepScale; i++)
        {
            var travelledDistance = pathStep * i;

            var pathPoint = pathCreator.path.GetPointAtDistance(travelledDistance, endOfPath);
            var vectorToTarget = targetPosition - pathPoint;
            vectorToTarget.y = 0;

            var distanceToTarget = vectorToTarget.magnitude;
            if (distanceToTarget < closestPoint.DistanceToTarget || closestPoint.DistanceToTarget == -1)
            {
                closestPoint = new ClosestPoint(vectorToTarget, distanceToTarget, travelledDistance);
            }
        }
        return closestPoint;
    }
}

struct ClosestPoint
{
    public Vector3 Position { get; }
    public float DistanceToTarget { get; }
    public float TravelledDistance { get; }

    public ClosestPoint(Vector3 position, float distanceToTarget, float travelledDistance)
    {
        Position = position;
        DistanceToTarget = distanceToTarget;
        TravelledDistance = travelledDistance;
    }
}
