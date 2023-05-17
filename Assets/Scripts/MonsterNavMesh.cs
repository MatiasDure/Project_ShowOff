using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Vector3 _monsterPositions;
    
    private NavMeshAgent _navMesh;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _navMesh.destination = new(0,0,0);
        
    }
}
