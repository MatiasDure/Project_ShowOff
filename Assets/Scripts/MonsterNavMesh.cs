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

    public static event Action OnReachedNewPosition;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        AngerQuest.OnIllegalMove += AssignNewPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.Instance.IsFrozen) return;

        _navMesh.destination = _target;  
        CheckIfReachedNewDestination();
    }

    private void AssignNewPosition()
    {
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
    }
}
