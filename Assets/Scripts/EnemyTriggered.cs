using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggered : MonoBehaviour
{
    private EnemyHealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<EnemyHealthManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<Bullet>();

        if (bullet.gameObject && bullet.isActive == true)
        {
            var damage = 5;
            healthManager.TakeDamage(damage);
        }
    }
}
