using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator),typeof(CharacterController))]
public class PlayerMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float moveBackPauseSeconds;
    [SerializeField] Transform _cameraTransform;

    Animator animator;
    CharacterController characterController;

    float horizontalInput;
    float verticalInput;

    bool getVerticalInput = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        GetInput();
        MovePlayer();
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (getVerticalInput)
        {
            verticalInput = Input.GetAxis("Vertical");
        }
    }

    void MovePlayer()
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movementDirection *= 2;
        }

        // animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime); TODO: Enable when we have animations

        movementDirection *= _moveSpeed;
        movementDirection = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            movementDirection += Jump();
        }

        characterController.SimpleMove(movementDirection);

        if (movementDirection == Vector3.zero) return;

        if (verticalInput == -1 && horizontalInput == 0)
        {
            StartCoroutine(MoveBackPause());
        }

        RotatePlayer(movementDirection);
    }

    void RotatePlayer(Vector3 pMoveVec)
    {
        Quaternion toRotation = Quaternion.LookRotation(pMoveVec, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }

    Vector3 Jump()
    {
        return new Vector3(0, _jumpForce, 0);
    }

    IEnumerator MoveBackPause()
    {
        verticalInput = 0;

        getVerticalInput = false;
        yield return new WaitForSeconds(moveBackPauseSeconds);
        getVerticalInput = true;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
