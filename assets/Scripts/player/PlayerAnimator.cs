using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;

    private Animator playerAnimator;
    private InputActionMap _inputActionMap;
    private InputAction move;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        _inputActionMap = controls.FindActionMap("Player");
        move = _inputActionMap.FindAction("Move");

        move.performed += OnBeginMoving;
        move.canceled += OnStopMoving;
    }

    void OnBeginMoving(InputAction.CallbackContext context)
    {
        Vector2 moveValue = context.ReadValue<Vector2>();
        playerAnimator.SetBool("Moving", true);

        if (moveValue.y < 0)
        {
            playerAnimator.SetBool("Backward", false);
        }
        else if (moveValue.y > 0)
        {
            playerAnimator.SetBool("Backward", true);
        }
    }

    void OnStopMoving(InputAction.CallbackContext context)
    {
        playerAnimator.SetBool("Moving", false);

    }
}
