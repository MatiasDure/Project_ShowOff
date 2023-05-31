using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] _monsterPositions;
    
    private NavMeshAgent _navMesh;
    private Vector3 _target;
    private Vector3 _startingPosition;
    private bool _questWon;

    public static event Action OnReachedNewPosition;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _startingPosition = transform.position;
        AngerQuest.OnIllegalMove += AssignNewPosition;
        AngerQuest.OnQuestFinished += MoveToStartingPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.Instance.IsFrozen &&
            !_questWon) return;

        _navMesh.destination = _target;
        CheckIfReachedNewDestination();
    }

    private void MoveToStartingPos()
    {
        _target = _startingPosition;
        _questWon = true;
    }

    private void AssignNewPosition()
    {
        if (_monsterPositions.Length < 1 ||
            _monsterPositions[0] == null) return;

        GameState.Instance.IsFrozen = true;
        _target = _monsterPositions[UnityEngine.Random.Range(0, _monsterPositions.Length)].position;       
    }

    private void CheckIfReachedNewDestination()
    {
        Vector3 dest = _navMesh.destination;
        Vector3 pos = this.transform.position;

        if (!(Mathf.Abs(dest.x - pos.x) < 1 &&
                Mathf.Abs(dest.y - pos.y) < 1 &&
                Mathf.Abs(dest.z - dest.z) < 1)) return;

        GameState.Instance.IsFrozen = false;
        OnReachedNewPosition?.Invoke();
    }

    private void OnDestroy()
    {
        AngerQuest.OnIllegalMove -= AssignNewPosition;
        AngerQuest.OnQuestFinished -= MoveToStartingPos;
    }
}
