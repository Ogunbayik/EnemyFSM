using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    private EnemyBaseState _currentState;

    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyStopState stopState = new EnemyStopState();
    void Start()
    {
        _currentState = patrolState;
        _currentState.EnterState(this);
    }

    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherEnemy = collision.gameObject.GetComponent<EnemyFSM>();

        if (!otherEnemy)
            _currentState.CollisionState(this, collision);
    }
}
