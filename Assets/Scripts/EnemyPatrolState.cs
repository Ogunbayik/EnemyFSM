using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector3 _randomPosition;

    private float _waitTime = 2f;
    private float _startWaitTime = 2f;
    private float _chaseDistance = 5f;

    public override void EnterState(EnemyFSM enemy)
    {
        healthManager = enemy.GetComponent<EnemyHealthManager>();
        _player = GameObject.FindGameObjectWithTag(playerTag);
        _movementSpeed = 1f;
        healthManager.isInvulnerable = false;

        GetRandomPosition();
    }

    public override void UpdateState(EnemyFSM enemy)
    {
        Patrolling(enemy);
    }

    private void Patrolling(EnemyFSM enemy)
    {
        var positionDistance = Vector3.Distance(enemy.transform.position, _randomPosition);
        var distanceBetweenPos = 0.1f;
        if (positionDistance >= distanceBetweenPos)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, _randomPosition, _movementSpeed * Time.deltaTime);
        }
        else
        {
            healthManager.isInvulnerable = true;
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0f)
            {
                _randomPosition = GetRandomPosition();
                _waitTime = _startWaitTime;
                healthManager.isInvulnerable = false;
            }
        }

        if (Vector3.Distance(enemy.transform.position, _player.transform.position) <= _chaseDistance)
            enemy.SwitchState(enemy.chaseState);
    }

    private Vector3 GetRandomPosition()
    {
        var borderX = 15f;
        var borderZ = 7f;
        var posY = 0f;
        var randomX = Random.Range(-borderX, borderX);
        var randomZ = Random.Range(-borderZ, borderZ);

        _randomPosition = new Vector3(randomX, posY, randomZ);
        return _randomPosition;
    }

    public override void CollisionState(EnemyFSM enemy, Collision collision)
    {
        
    }
}
