using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _jumpForce;

    CharacterController _charController;

    private Vector3 _previousPosition;

    public bool IsMoving { get; private set; }

    void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        GetInputs();
        MovePlayer();
        RotatePlayer();
    }

    void GetInputs()
    {

    }

    void MovePlayer()
    {
        _previousPosition = this.transform.position;

        Vector3 _moveVec = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        _moveVec *= _moveSpeed;
        _moveVec = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * _moveVec;

        _charController.SimpleMove(_moveVec); // SimpleMove adds Gravity and Time.deltaTime

        IsMoving = _previousPosition != this.transform.position;
    }


    void RotatePlayer()
    {
        Vector3 _rotationVec = new Vector3(0, Input.GetAxis("Horizontal"), 0);

        _rotationVec *= _rotationSpeed * Time.deltaTime * 100;

        transform.Rotate(_rotationVec);
    }
}
