using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePOV : MonoBehaviour
{
    public Transform Player;
    public Camera FirstPersonCam, ThirdPersonCam;
    public KeyCode TKey;
    public bool IsFirstPerson = false;

    void Update() {
        if (Input.GetKeyDown(TKey))
        {
            IsFirstPerson = !IsFirstPerson;
            if (IsFirstPerson)
            {
                Player.transform.localRotation = Quaternion.Euler(Player.transform.eulerAngles.x, ThirdPersonCam.transform.eulerAngles.y, Player.transform.eulerAngles.z);
                FirstPersonCam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            } else
            {
                ThirdPersonCam.transform.localRotation = Quaternion.Euler(ThirdPersonCam.transform.eulerAngles.x, FirstPersonCam.transform.eulerAngles.y, ThirdPersonCam.transform.eulerAngles.z);
            }
            FirstPersonCam.gameObject.SetActive(IsFirstPerson);
            ThirdPersonCam.gameObject.SetActive(!IsFirstPerson);
        }
    }
}
