    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    void pickNewPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;
    public float damage = 30;

    private PlayerHealth _playerHealth;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        pickNewPoint();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _playerHealth.DealDamage(damage * Time.deltaTime);
            }
        }
    }

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;
 

    void Update()
    {
        AttackUpdate();
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
        if (!_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance == _navMeshAgent.stoppingDistance)
            {
                pickNewPoint();
            }
        }

        _isPlayerNoticed = false;
        var direction = player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }

            }
        }
    }
}