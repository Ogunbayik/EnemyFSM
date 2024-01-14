using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isActive;

    private MeshRenderer meshRenderer;
    private float _deactiveCooldown = 5f;
    private float _movementSpeed;
    private Vector3 _direction;

    private void Start()
    {
        StartCoroutine(nameof(DeactiveCooldown));
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _direction, _movementSpeed * Time.deltaTime);
    }
    public void Movement(Vector3 dir, float speed)
    {
        _direction = dir;
        _movementSpeed = speed;
    }
    IEnumerator DeactiveCooldown()
    {
        yield return new WaitForSeconds(_deactiveCooldown);
        DeactiveBullet();
    }
    private void DeactiveBullet()
    {
        isActive = false;
    }

    
}
