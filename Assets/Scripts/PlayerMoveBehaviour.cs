using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] float _moveSpeed;
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
    }

    void GetInputs()
    {

    }

    void MovePlayer()
    {
        Vector3 _moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        _moveVec *= _moveSpeed * Time.deltaTime;

        _charController.Move(_moveVec);
    }
}
