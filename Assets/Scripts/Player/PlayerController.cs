using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 이동 input 
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
    /// 이동
    /// </summary>
    /// <param name="curMovementInput">현재 입력된 이동input</param>
    /// <param name="speed">이동속도</param>
    /// <param name="rigidbody">player의 rigidbody</param>
    public void Move(Vector2 curMovementInput, float speed, Rigidbody rigidbody)
    { 
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= speed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    /// <summary>
    /// 시야Input
    /// </summary>
    /// <param name="context"></param>
    public void OnLookInput(InputAction.CallbackContext context)
    {
        CharactorManager.Instance.Player._playerData.mouseDelta = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 바라보기
    /// </summary>
    /// <param name="camCurxRot">X회전값</param>
    /// <param name="mouseDelta">마우스위치Vector</param>
    /// <param name="minXLook"> x시야각 최소치 </param>
    /// <param name="maxXLook"> x시야각 최대치 </param>
    /// <param name="lookSensitivity"> 시야 민감도 </param>
    /// <param name="cameraContainer"> 카메라 컨테이너위치 </param>
    public void Look(ref float camCurxRot, Vector2 mouseDelta, float minXLook, float maxXLook, float lookSensitivity,Transform cameraContainer)
    {
        camCurxRot += mouseDelta.y * lookSensitivity;
        camCurxRot = Mathf.Clamp(camCurxRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurxRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// 점프 input
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

    private bool IsGrounded()
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
            if (Physics.Raycast(rays[i], 0.1f, CharactorManager.Instance.Player._playerData.groundLayerMask))
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
