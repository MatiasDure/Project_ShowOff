using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator),typeof(CharacterController))]
public class PlayerMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    public bool IsMoving { get; private set; }

    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _moveBackPauseSec;
    [SerializeField] Transform _cameraTransform;

    Animator _animator;
    CharacterController _charController;

    Vector3 _previousPosition;

    float _horInput;
    float _verInput;

    bool _getVerInput = true;


    //=====================//
    float ySpeed;
    float lastGroundedTime;
    float jumpButtonPressedTime;
    float jumpButtonGracePeriod = 0.5f;
    [SerializeField] float originalStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _charController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        GetInput();
        MovePlayer();
    }

    void GetInput()
    {
        _horInput = Input.GetAxis("Horizontal");

        if (_getVerInput)
        {
            _verInput = Input.GetAxis("Vertical");
        }
    }

    void MovePlayer()
    {
        _previousPosition = this.transform.position;

        Vector3 _moveVec = new Vector3(_horInput, 0, _verInput).normalized;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _moveVec *= 2;
        }

        // animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime); TODO: Enable when we have animations

        _moveVec *= _moveSpeed;
        _moveVec = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up) * _moveVec;

        /*
        if (Input.GetButtonDown("Jump") && _charController.isGrounded)
        {
            _moveVec = Jump(_moveVec);
        }
        */

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (_charController.isGrounded)
        {
            lastGroundedTime = Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.deltaTime;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            _charController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if(Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = _jumpForce;
                jumpButtonPressedTime = 0;
                lastGroundedTime = 0;
            }
        }
        else
        {
            _charController.stepOffset = 0;
        }

        _moveVec.y = ySpeed;


        _charController.Move(_moveVec * Time.deltaTime);

        if (_moveVec == Vector3.zero) return;

        if (_verInput == -1 && _horInput == 0)
        {
            StartCoroutine(MoveBackPause());
        }

        RotatePlayer(_moveVec);

        IsMoving = _previousPosition != this.transform.position;
    }

    void RotatePlayer(Vector3 _pMoveVec)
    {
        Quaternion _toRotation = Quaternion.LookRotation(_pMoveVec, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, _rotationSpeed * Time.deltaTime);
    }

    Vector3 Jump(Vector3 _pMoveVec)
    {
        return new Vector3(_pMoveVec.x, _jumpForce, _pMoveVec.z);
    }

    IEnumerator MoveBackPause()
    {
        _verInput = 0;

        _getVerInput = false;
        yield return new WaitForSeconds(_moveBackPauseSec);
        _getVerInput = true;
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
