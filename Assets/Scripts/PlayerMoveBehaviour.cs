using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _jumpForce;

    Rigidbody _rb;
    CharacterController _charController;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
        Vector3 _moveVec = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        _moveVec *= _moveSpeed;
        _moveVec = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * _moveVec;

        _charController.SimpleMove(_moveVec); // SimpleMove adds Gravity and Time.deltaTime
    }


    void RotatePlayer()
    {
        Vector3 _rotationVec = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        _rotationVec *= _rotationSpeed * Time.deltaTime * 100;

        transform.Rotate(_rotationVec);
    }
}
