using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelFootIKSystem : MonoBehaviour
{
    public bool EnableFeetIk = true;
    public string RightFootAnimVariableName = "RightFootCurve";
    public string LeftFootAnimVariableName = "LeftFootCurve";
    public bool UseProIkFeature = false;
    public bool ShowSolverDebug = true;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _enviromentLayer;
    private Vector3 _rightFootPosition;
    private Vector3 _rightFootIkPosition;
    private Vector3 _leftFootPosition;
    private Vector3 _leftFootIkPosition;
    private Quaternion _rightFootIkRotation;
    private Quaternion _leftFootIkRotation;
    private float _lastPelvisPositionY;
    private float _lastRightFootPositionY;
    private float _lastLeftFootPositionY;
    private float _heightFromGroundRaycast = 1.14f;
    private float _raycastDownDistance = 1.15f;
    private float _pelvisOffset = 0f;
    private float _pelvisUpAndDownSpeed = 0.28f;
    private float _feetToIkPositionSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!EnableFeetIk) { return; }
        if (_animator == null) { return; }

        AdjustFeetTarget(ref _rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref _leftFootPosition, HumanBodyBones.LeftFoot);

        //Find and raycast to the ground to find position
        FeetPositionSolver(_rightFootPosition, ref _rightFootIkPosition, ref _rightFootIkRotation);
        FeetPositionSolver(_leftFootPosition, ref _leftFootIkPosition, ref _leftFootIkRotation);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!EnableFeetIk) { return; }
        if (_animator == null) { return; }

        MovePelvisHeight();

        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        if (UseProIkFeature)
        {
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _animator.GetFloat(RightFootAnimVariableName));
        }
        MoveFeetToIkPoint(AvatarIKGoal.RightFoot, _rightFootIkPosition, _rightFootIkRotation, ref _lastRightFootPositionY);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        if (UseProIkFeature)
        {
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat(LeftFootAnimVariableName));
        }
        MoveFeetToIkPoint(AvatarIKGoal.LeftFoot, _leftFootIkPosition, _leftFootIkRotation, ref _lastLeftFootPositionY);
    }

    private void MoveFeetToIkPoint(AvatarIKGoal foot, Vector3 positionIkHolder, Quaternion rotationIkHolder, ref float lastFootPositionY)
    {
        Vector3 targetIkPosition = _animator.GetIKPosition(foot);

        if (positionIkHolder != Vector3.zero)
        {
            targetIkPosition = transform.InverseTransformPoint(targetIkPosition);
            positionIkHolder = transform.InverseTransformPoint(positionIkHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIkHolder.y, _feetToIkPositionSpeed);
            targetIkPosition.y += yVariable;
            lastFootPositionY = yVariable;

            targetIkPosition = transform.TransformPoint(targetIkPosition);
            _animator.SetIKRotation(foot, rotationIkHolder);
        }
        _animator.SetIKPosition(foot, targetIkPosition);
    }

    private void MovePelvisHeight()
    {
        if (_rightFootIkPosition == Vector3.zero || _leftFootIkPosition == Vector3.zero || _lastPelvisPositionY == 0)
        {
            _lastPelvisPositionY = _animator.bodyPosition.y;
        }
        else
        {
            float lOffsetPosition = _leftFootIkPosition.y - transform.position.y;
            float rOffsetPosition = _rightFootIkPosition.y - transform.position.y;
            float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

            Vector3 newPelvisPosition = _animator.bodyPosition + (Vector3.up * totalOffset);
            newPelvisPosition.y = Mathf.Lerp(_lastPelvisPositionY, newPelvisPosition.y, _pelvisUpAndDownSpeed);
            _animator.bodyPosition = newPelvisPosition;
            _lastPelvisPositionY = _animator.bodyPosition.y;
        }
    }

    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIkPositions, ref Quaternion feetIkRotations)
    {
        RaycastHit feetOutHit;

        if (ShowSolverDebug)
        {
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (_raycastDownDistance + _heightFromGroundRaycast), Color.yellow);
        }

        if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, _raycastDownDistance + _heightFromGroundRaycast, _enviromentLayer))
        {
            feetIkPositions = fromSkyPosition;
            feetIkPositions.y = feetOutHit.point.y + _pelvisOffset;
            feetIkRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;
        }
        else
        {
            feetIkPositions = Vector3.zero;
        }
    }

    private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        feetPositions = _animator.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + _heightFromGroundRaycast;
    }
}
