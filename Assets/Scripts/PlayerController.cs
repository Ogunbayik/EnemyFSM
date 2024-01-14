using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private EnemyFSM[] enemies;

    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _bulletSpeed;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _direction;

    private Transform _nearestEnemy;
    private float _nearestDistance;
    private float _enemiesDistance;


    private void Start()
    {
        enemies = FindObjectsOfType<EnemyFSM>();
    }

    void Update()
    {
        Movement();
        GetNearestTransform();
    }

    private void Movement()
    {
        _horizontalInput = Input.GetAxis(_horizontal);
        _verticalInput = Input.GetAxis(_vertical);
        _direction = new Vector3(_horizontalInput, 0f, _verticalInput);

        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
    }

    private Transform GetNearestTransform()
    {
        _nearestDistance = Mathf.Infinity;
        enemies = FindObjectsOfType<EnemyFSM>();
        _nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            _enemiesDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (_enemiesDistance < _nearestDistance)
            {
                _nearestDistance = _enemiesDistance;
                _nearestEnemy = enemy.gameObject.transform;
            }
        }
        return _nearestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectibleBullet = other.GetComponent<Bullet>();

        if(collectibleBullet.gameObject)
        {
            Destroy(collectibleBullet.gameObject);
            var bullet = Instantiate(_bulletPrefab);
            var positionOffset = new Vector3(0, 2, 0);
            bullet.transform.position = transform.position + positionOffset;
            bullet.GetComponent<Bullet>().isActive = true;
            bullet.GetComponent<Bullet>().Movement(_nearestEnemy.transform.position, _bulletSpeed);
        }
    }


}
