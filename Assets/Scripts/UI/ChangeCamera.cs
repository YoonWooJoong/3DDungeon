using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    bool isOn = false;

    public void OnClickButton()
    {
        firstPersonCamera.SetActive(isOn);
        thirdPersonCamera.SetActive(!isOn);
        isOn = !isOn;

    }
}
