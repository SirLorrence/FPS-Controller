using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 5f;
    public float jumpForce = 5f;
    private float movementHorizontal;
    private float movementVertial;
    private Vector3 movement;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementHorizontal = movementVector.x;
        movementVertial = movementVector.y;
        movement = new Vector3(movementHorizontal, 0f, movementVertial).normalized;
        Debug.Log("Movement");
    }

    private void OnJump(InputValue jumpValue)
    {
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jump");
    }

    private void FixedUpdate()
    {
        // rigidbody.AddForce(movement * (speed * Time.deltaTime));
        rigidbody.MovePosition(transform.position + (movement * (speed * Time.deltaTime)));
    }

    // Update is called once per frame
    private void Update()
    {
    }
}