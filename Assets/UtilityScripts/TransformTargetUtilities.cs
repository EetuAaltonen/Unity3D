using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformTargetUtilities
{
    public static void TransformFaceTarget(Transform self, Transform target, float slerp)
    {
        Vector3 direction = (target.position - self.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        self.rotation = Quaternion.Slerp(self.rotation, lookRotation, Time.deltaTime * slerp);
    }
}
