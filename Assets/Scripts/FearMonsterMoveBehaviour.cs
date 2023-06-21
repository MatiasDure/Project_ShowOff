using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearMonsterMoveBehaviour : MonoBehaviour, IMoveBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] AnimatorMonster _monsterAnim;

    List<Vector3> _waypoints = new List<Vector3>();

    int _currTarg = 0;

    void OnEnable()
    {
        FearQuest.OnNextMonsterWaypoint += AddWaypoint;
    }

    void OnDisable()
    {
        FearQuest.OnNextMonsterWaypoint += AddWaypoint;
    }

    void Update()
    {
        if(WaypointAvailable()) Move();
    }

    bool WaypointAvailable()
    {
        if (_waypoints.Count > _currTarg) return true;

        return false;
    }

    public void Move()
    {
        _monsterAnim.UpdateParameter(AnimatorMonster.Params.IsMoving, true);
        Vector3 _targPos = _waypoints[_currTarg];
        Vector3 _desiredPos = _targPos - transform.position;
        transform.position += _desiredPos.normalized * _moveSpeed * Time.deltaTime;

        Rotate(_desiredPos);

        if (_desiredPos.magnitude > 1) return;

        _monsterAnim.UpdateParameter(AnimatorMonster.Params.IsMoving, false);
        _currTarg++;
    }

    void Rotate(Vector3 _targRot)
    {
        Quaternion _desiredRot = Quaternion.LookRotation(_targRot);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _desiredRot, _rotationSpeed * Time.deltaTime);
    }

    void AddWaypoint(Vector2 pWaypoint)
    {
        float _currY = transform.position.y;
        _waypoints.Add(new Vector3(pWaypoint.x, _currY, pWaypoint.y));
    }
}
