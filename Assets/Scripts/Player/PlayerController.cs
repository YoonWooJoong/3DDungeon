using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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

    public void Move(Vector2 curMovementInput, float speed, Rigidbody rigidbody)
    { 
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= speed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }
}
