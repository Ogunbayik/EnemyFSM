using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private Image[] _hearts;

    [SerializeField]
    private Sprite _heartSprite;
    [SerializeField]
    private Sprite _devilHeartSprite;

    private int _currentHealth = 3;
    [SerializeField]
    private float _heartSpeed;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        HeartController();

        CheckHeartCount();
    }

    private void HeartController()
    {
        foreach (var image in _hearts)
        {
            image.sprite = _devilHeartSprite;

            for (int i = 0; i < _currentHealth; i++)
            {
                _hearts[i].sprite = _heartSprite;
            }

            HeartMovement();
            HeartDeactivate();
        }
    }

    private void HeartMovement()
    {
        foreach (var image in _hearts)
        {
            if (image.sprite == _devilHeartSprite)
            {
                image.rectTransform.position += Vector3.up * _heartSpeed * Time.deltaTime;
            }
        }
    }

    private void HeartDeactivate()
    {
        foreach (var image in _hearts)
        {
            var maximumY = 590;
            if (image.rectTransform.localPosition.y > maximumY)
            {
                image.rectTransform.position = new Vector3(image.rectTransform.position.x, maximumY, image.rectTransform.position.z);
                image.gameObject.SetActive(false);
            }
        }
    }

    public void LoseHeart(int value)
    {
        _currentHealth -= value;
    }

    private void CheckHeartCount()
    {
        if (_currentHealth == 0)
            gameManager.currentState = GameManager.GameStates.GameOverState;
    }


}
