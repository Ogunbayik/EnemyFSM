using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private float _chaseDistance = 5f;

    public override void EnterState(EnemyFSM enemy)
    {
        healthManager = enemy.GetComponent<EnemyHealthManager>();
        healthManager.isInvulnerable = false;
        _player = GameObject.FindGameObjectWithTag(playerTag);
        _movementSpeed = 3f;
    }

    public override void UpdateState(EnemyFSM enemy)
    {
        Chasing(enemy);
    }

    private void Chasing(EnemyFSM enemy)
    {
        enemy.GetComponent<MeshRenderer>().material.color = Color.red;
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, _player.transform.position, _movementSpeed * Time.deltaTime);

        if (Vector3.Distance(enemy.transform.position, _player.transform.position) >= _chaseDistance)
        {
            enemy.GetComponent<MeshRenderer>().material.color = _originalColor;
            enemy.SwitchState(enemy.patrolState);
        }
    }
    public override void CollisionState(EnemyFSM enemy, Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        var playerHealth = collision.gameObject.GetComponent<HealthManager>();

        if(player.gameObject)
        {
            int count = 1;
            playerHealth.LoseHeart(count);
            enemy.SwitchState(enemy.stopState);
        }
    }
}
