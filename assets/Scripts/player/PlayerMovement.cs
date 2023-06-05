using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private InputActionAsset controls;

	private Animator playerAnimator;
	private Transform playerTransform;
	private Rigidbody2D playerBody;
	private Vector2 moveValue = Vector2.zero;
	private InputActionMap _inputActionMap;
	private InputAction move;

	void Start()
	{
		playerAnimator = GetComponent<Animator>();
		playerTransform = GetComponent<Transform>();
		playerBody = GetComponent<Rigidbody2D>();

		_inputActionMap = controls.FindActionMap("Player");
		move = _inputActionMap.FindAction("Move");

		move.performed += OnBeginMoving;
		move.canceled += OnStopMoving;

	}

	void FixedUpdate()
	{
		playerBody.velocity = moveValue * moveSpeed;
	}

	public void OnBeginMoving(InputAction.CallbackContext context)
	{
		moveValue = context.ReadValue<Vector2>();
	}

	public void OnStopMoving(InputAction.CallbackContext context)
	{
		moveValue = Vector2.zero;
	}
}
