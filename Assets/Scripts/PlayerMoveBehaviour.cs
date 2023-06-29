using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    //[SerializeField] float _jumpForce;
    //[SerializeField] float _jumpButtonDelay;
    [SerializeField] Transform _camTransform;
    [SerializeField] float _moveBackPauseSec;
    [SerializeField] public AnimatorManipulator AnimManipulator;

    private CharacterController _charController;
    private float _ySpeed;
    //private float _origStepOffset;
    //private float? _lastGroundedTime;
    //private float? _jumpButtonPressedTime;
    private bool _getVerticalInput = true;
    private float _horizontalInput;
    private float _verticalInput;

    public bool IsMoving { get; private set; }
    private Vector3 _previousPosition;

    void Awake()
    {
        if (AnimManipulator == null) AnimManipulator = GetComponentInChildren<AnimatorManipulator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        //_origStepOffset = _charController.stepOffset;
    }

    public void Move()
    {
        GetInput();
        MovePlayer();
    }

    void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_getVerticalInput) _verticalInput = Input.GetAxis("Vertical");

        _verticalInput = GameState.Instance.IsFrozen ? 0 : _verticalInput;
    }

    void MovePlayer()
    {
        _previousPosition = this.transform.position;

        Vector3 movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude *= 2;
        }

        float speed = inputMagnitude * _moveSpeed;
        movementDirection = Quaternion.AngleAxis(_camTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        _ySpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = movementDirection * speed;
        velocity.y = _ySpeed;

        _charController.Move(velocity * Time.deltaTime);

        IsMoving = _previousPosition != this.transform.position;

        //setting animations
        AnimManipulator.SetParamValue(AnimatorManipulator.Params.IsMoving, IsMoving);
        if (IsMoving) AnimManipulator.SetParamValue(AnimatorManipulator.Params.IsInteracting, false);

        if (movementDirection == Vector3.zero) return;

        if (_verticalInput == -1 && _horizontalInput == 0)
        {
            StartCoroutine(MoveBackPause());
        }

        RotatePlayer(movementDirection);
    }

    void RotatePlayer(Vector3 _pMoveVec)
    {
        Quaternion _toRotation = Quaternion.LookRotation(_pMoveVec, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _toRotation, _rotationSpeed * Time.deltaTime);
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

    IEnumerator MoveBackPause()
    {
        _verticalInput = 0;

        _getVerticalInput = false;
        yield return new WaitForSeconds(_moveBackPauseSec);
        _getVerticalInput = true;
    }
}
