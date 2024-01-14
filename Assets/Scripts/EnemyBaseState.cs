using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
    protected EnemyHealthManager healthManager;

    protected GameObject _player;
    protected Color _originalColor = new Color(1, 0.3378351f, 0);
    protected float _movementSpeed;

    protected const string playerTag = "Player";
    public abstract void EnterState(EnemyFSM enemy);

    public abstract void UpdateState(EnemyFSM enemy);

    public abstract void CollisionState(EnemyFSM enemy, Collision collision);
}
