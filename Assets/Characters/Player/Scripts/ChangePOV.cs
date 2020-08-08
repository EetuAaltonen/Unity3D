using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePOV : MonoBehaviour
{
    public KeyCode TKey;

    [SerializeField] private GameObject _playerRef;
    [SerializeField] private GameObject _cinemachineThirdPerson;
    private bool _isFirstPerson;
    private PlayerCamera _cameraScript;
    private CinemachineFreeLook _cinemachineScript;
    private Camera _firstPersonCamera;
    private Camera _thirdPersonCamera;

    private void Start()
    {
        _isFirstPerson = true;
        _cameraScript = GetComponent<PlayerCamera>();
        _cinemachineScript = _cinemachineThirdPerson.GetComponent<CinemachineFreeLook>();
        _firstPersonCamera = _cameraScript.FirstPersonCamera;
        _thirdPersonCamera = _cameraScript.ThirdPersonCamera;
    }

    void Update() {
        if (Input.GetKeyDown(TKey))
        {
            _isFirstPerson = !_isFirstPerson;
            if (_isFirstPerson)
            {
                transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, _thirdPersonCamera.transform.eulerAngles.y, transform.eulerAngles.z);
                _cameraScript.ResetXRotation();
            } else
            {
                _cinemachineScript.m_XAxis.Value = _playerRef.transform.localRotation.eulerAngles.y;
                _cinemachineScript.m_YAxis.Value = 0.25f;
            }
            _firstPersonCamera.gameObject.SetActive(_isFirstPerson);
            _thirdPersonCamera.gameObject.SetActive(!_isFirstPerson);
        }
    }

    public bool IsFirstPerson()
    {
        return _isFirstPerson;
    }
}
