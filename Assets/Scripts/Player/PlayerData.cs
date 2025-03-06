using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXlook;
    public float maxXlook;
    public float camCurxRot;
    public float lookSensitivity;

    [HideInInspector]
    public Vector2 mouseDelta;
    public bool canLook = true;

    private Rigidbody _rigidbody;
    public Rigidbody rigidbody { get { return _rigidbody; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

}
