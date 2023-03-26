using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public bool isInvulnerable;

    private Color _originalColor = new Color(1, 0.3378351f, 0);
    private Color _invulnerableColor = Color.black;
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private TextMesh _healthText;
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _maxHealth = Random.Range(20, 60);
        _currentHealth = _maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        _healthText.text = _currentHealth.ToString();

        Destroyed();
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (isInvulnerable)
            _meshRenderer.material.color = _invulnerableColor;
        else
            _meshRenderer.material.color = _originalColor;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
            _currentHealth -= damage;
    }

    private void Destroyed()
    {
        if (_currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.enemyList.Remove(this.gameObject);
        }
    }
}
