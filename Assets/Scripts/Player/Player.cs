using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController _playerController;
    public PlayerData _playerData;

    private void Awake()
    {
        CharactorManager.Instance.Player = this;
        _playerController = GetComponent<PlayerController>();
        _playerData = GetComponent<PlayerData>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        _playerController.Move(_playerData.curMovementInput, _playerData.moveSpeed, _playerData.rigidbody);
    }
    private void LateUpdate()
    {
        if(_playerData.canLook)
            _playerController.Look(ref _playerData.camCurxRot, _playerData.mouseDelta, _playerData.minXlook, _playerData.maxXlook, _playerData.lookSensitivity, _playerData.cameraContainer);   
    }
}
