using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// �̵� input 
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CharactorManager.Instance.Player._playerData.curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CharactorManager.Instance.Player._playerData.curMovementInput = Vector2.zero;
        }
    }
    

    /// <summary>
    /// �̵�
    /// </summary>
    /// <param name="curMovementInput">���� �Էµ� �̵�input</param>
    /// <param name="speed">�̵��ӵ�</param>
    /// <param name="rigidbody">player�� rigidbody</param>
    public void Move(Vector2 curMovementInput, float speed, Rigidbody rigidbody, Vector3 ForceMove)
    { 
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= speed;
        dir += ForceMove;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }



    public void OnClimbInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CharactorManager.Instance.Player._playerData.curMovementInput = context.ReadValue<Vector3>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CharactorManager.Instance.Player._playerData.curMovementInput = Vector3.zero;
        }
    }

    public void Climb(Vector3 curMovementInput, float speed, Rigidbody rigidbody)
    {
        Vector3 dir = transform.up * curMovementInput.y + transform.right*curMovementInput.x;
        dir *= speed;
        dir.z = rigidbody.velocity.z;

        rigidbody.velocity = dir;
        
    }


    /// <summary>
    /// �þ�Input
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        CharactorManager.Instance.Player._playerData.mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// �ٶ󺸱�
    /// </summary>
    /// <param name="camCurxRot">Xȸ����</param>
    /// <param name="mouseDelta">���콺��ġVector</param>
    /// <param name="minXLook"> x�þ߰� �ּ�ġ </param>
    /// <param name="maxXLook"> x�þ߰� �ִ�ġ </param>
    /// <param name="lookSensitivity"> �þ� �ΰ��� </param>
    /// <param name="cameraContainer"> ī�޶� �����̳���ġ </param>
    public void Look(ref float camCurxRot, Vector2 mouseDelta, float minXLook, float maxXLook, float lookSensitivity,Transform cameraContainer)
    {
        camCurxRot += mouseDelta.y * lookSensitivity;
        camCurxRot = Mathf.Clamp(camCurxRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurxRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// ���� input
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if(CharactorManager.Instance.Player._playerCondition.UseStamina(CharactorManager.Instance.Player._playerData.useJumpStamina))
            {
            CharactorManager.Instance.Player._playerData.rigidbody.AddForce(Vector2.up * CharactorManager.Instance.Player._playerData.jumpPower, ForceMode.Impulse);
            }
        }
    }

    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward*0.2f) + (transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward*0.2f) + (transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (transform.right*0.2f) + (transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right*0.2f) + (transform.up*0.01f),Vector3.down)
        };

        for(int i =0; i< rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, CharactorManager.Instance.Player._playerData.groundLayerMask | CharactorManager.Instance.Player._playerData.wallLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsWall(Vector3 curMovementInput)
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        Ray[] rays = new Ray[2]
            {
                new Ray(transform.position + (transform.right*0.2f) + (transform.up*0.1f) ,dir * 0.5f),
                new Ray(transform.position + (-transform.right*0.2f) + (transform.up*0.1f) ,dir * 0.5f)
            };
        Debug.DrawRay(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), dir * 0.5f, Color.red);
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, CharactorManager.Instance.Player._playerData.wallLayerMask))
            {
                return true;
            }
            
        }
        return false;
    }
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CharactorManager.Instance.Player._playerData.inventory?.Invoke();
            ToggleCursor();
        }
    }


    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        CharactorManager.Instance.Player._playerData.canLook = !toggle;
    }
}
