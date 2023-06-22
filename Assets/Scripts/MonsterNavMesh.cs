using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform[] _monsterPositions;
    [SerializeField] private float _speed;
    [SerializeField] private AnimatorMonster _animatorMonster;

    private NavMeshAgent _navMesh;
    private Vector3 _target;
    private Vector3 _startingPosition;
    private bool _questWon;
    private int _currentIndex;

    public static event Action OnReachedNewPosition;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _startingPosition = transform.position;
        //AngerQuest.OnIllegalMove += AssignNewPosition;
        AngerQuest.OnQuestFinished += ResetPos;
        AngerQuest.OnTouchedMonster += MoveToPos;
    }

    private void Start()
    {
        ResetAttributes();
    }

    private void ResetAttributes()
    {
        _currentIndex = 0;
        _navMesh.speed = _speed;
    }

    private void MoveToPos()
    {
        if (!AngerQuest.Instance.IsPlaying) return;

        _animatorMonster.UpdateParameter(AnimatorMonster.Params.IsMoving, true);
        _target = _monsterPositions[_currentIndex++].position;
        _navMesh.destination = _target;
        GameState.Instance.IsFrozen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!GameState.Instance.IsFrozen && !_questWon) || !AngerQuest.Instance.IsPlaying) return;

        //_navMesh.destination = _target;
        CheckIfReachedNewDestination();
    }

    private void ResetPos()
    {
        _target = _startingPosition;
        _questWon = true;
        ResetAttributes();
    }

    private void CheckIfReachedNewDestination()
    {
        Vector3 dest = _navMesh.destination;
        Vector3 pos = this.transform.position;
        Vector3 dir = dest - pos;
        float distance = dir.magnitude;

        if (distance > 2f)
        {
            _navMesh.destination = _target;
            return;
        }
        //if (!(Mathf.Abs(dest.x - pos.x) < 1 &&
        //        Mathf.Abs(dest.y - pos.y) < 1 &&
        //        Mathf.Abs(dest.z - pos.z) < 1)) return;

        GameState.Instance.IsFrozen = false;
        _animatorMonster.UpdateParameter(AnimatorMonster.Params.IsMoving, false);
        OnReachedNewPosition?.Invoke();
    }

    private void OnDestroy()
    {
        AngerQuest.OnQuestFinished -= ResetPos;
        AngerQuest.OnTouchedMonster -= MoveToPos;
    }
}
