using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonPartsRotation : MonoBehaviour
{
    public GameObject Horse;

    private CharacterController _controller;
    [SerializeField] private Transform _directionFrame;
    [SerializeField] private Transform _saddleAxel;
    private float _offsetY = 3.6f;
    private CharacterFollowPath _horseFollowPathScript;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _horseFollowPathScript = Horse.GetComponent<CharacterFollowPath>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetPositioin = Horse.transform.position + (Vector3.up * _offsetY);

        var distanceToHorse = Vector3.Distance(transform.position, Horse.transform.position);
        var targetDistance = 10.5f;
        var distanceOffset = 0.2f;
        var speed = 5f + _horseFollowPathScript.GetSpeed();
        var speedFactor = Mathf.Abs(distanceToHorse - targetDistance);
        speed *= speedFactor;
        if (distanceToHorse > targetDistance + distanceOffset)
        {
            _controller.Move(transform.forward * speed * Time.deltaTime);
        }
        else if (distanceToHorse < targetDistance - distanceOffset)
        {
            _controller.Move((transform.forward * -1) * speed * Time.deltaTime);
        }

        var horseRotationY = RotationUtilities.ScaleRotation(Horse.transform.eulerAngles.y);
        var axelRotationY = RotationUtilities.ScaleRotation(_directionFrame.eulerAngles.y);
        if (Mathf.Abs(horseRotationY - axelRotationY) < 40f)
        {
            var lookWagonPos = targetPositioin - transform.position;
            lookWagonPos.y = 0;
            var targetWagonRotation = Quaternion.LookRotation(lookWagonPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetWagonRotation, 2f * Time.deltaTime);
        }

        var lookPos = targetPositioin - _directionFrame.position;
        lookPos.y = 90;
        var targetRotation = Quaternion.LookRotation(lookPos);
        _directionFrame.rotation = Quaternion.Slerp(_directionFrame.rotation, targetRotation, 5f * Time.deltaTime);

        /*float ry = _directionFrame.eulerAngles.y;
        if (ry >= 180) ry -= 360;
        _directionFrame.eulerAngles = new Vector3(
            _directionFrame.eulerAngles.x,
            Mathf.Clamp(ry, transform.localRotation.y - 12f, transform.localRotation.y + 12f),
            _directionFrame.eulerAngles.z
        );*/

        _saddleAxel.LookAt(targetPositioin);
    }
}
