using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopState : EnemyBaseState
{
    private float _cooldown;

    public override void EnterState(EnemyFSM enemy)
    {
        healthManager = enemy.GetComponent<EnemyHealthManager>();
        healthManager.isInvulnerable = true;
        _movementSpeed = 0f;
        _cooldown = 5f;
    }

    public override void UpdateState(EnemyFSM enemy)
    {
        _cooldown -= Time.deltaTime;

        if(_cooldown <= 0f)
        {
            healthManager.isInvulnerable = false;
            enemy.GetComponent<MeshRenderer>().material.color = _originalColor;
            enemy.SwitchState(enemy.patrolState);
            _cooldown = 0f;
        }
    }
  
    public override void CollisionState(EnemyFSM enemy, Collision collision)
    {

    }
}
