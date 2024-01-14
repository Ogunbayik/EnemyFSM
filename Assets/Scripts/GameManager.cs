using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> enemyList = new List<GameObject>();

    [SerializeField]
    private GameObject _startPanel;
    [SerializeField]
    private GameObject _inGamePanel;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _nextLevelPanel;

    public enum GameStates
    {
        StartState,
        InGameState,
        GameOverState,
        NextLevelState
    }

    public GameStates currentState;


    private void Awake()
    {
        if (Instance == null || Instance != this)
            Instance = this;
    }
    void Start()
    {
        currentState = GameStates.StartState;

        GetAllEnemies();
    }

    private void GetAllEnemies()
    {
        var enemies = FindObjectsOfType<EnemyFSM>();
        foreach (var enemy in enemies)
        {
            enemyList.Add(enemy.gameObject);
        }
    }

    void Update()
    {
        switch(currentState)
        {
            case GameStates.StartState: StartGame();
                break;
            case GameStates.InGameState: InGame();
                break;
            case GameStates.GameOverState: GameOver();
                break;
            case GameStates.NextLevelState: NextLevel();
                break;
        }
    }
    private enum GamePanels
    {
        StartPanel,
        InGamePanel,
        GameOverPanel,
        NextLevelPanel
    }

    private void PanelController(GamePanels panel)
    {
        _startPanel.SetActive(false);
        _inGamePanel.SetActive(false);
        _gameOverPanel.SetActive(false);
        _nextLevelPanel.SetActive(false);

        switch(panel)
        {
            case GamePanels.StartPanel: _startPanel.SetActive(true);
                break;
            case GamePanels.InGamePanel: _inGamePanel.SetActive(true);
                break;
            case GamePanels.GameOverPanel: _gameOverPanel.SetActive(true);
                break;
            case GamePanels.NextLevelPanel: _nextLevelPanel.SetActive(true);
                break;
        }
    }

    private void StartGame()
    {
        PanelController(GamePanels.StartPanel);
    }
    
    private void InGame()
    {
        PanelController(GamePanels.InGamePanel);

        ActivateMovement(true);

        DefeatAllEnemies();
    }

    private void DefeatAllEnemies()
    {
        if(enemyList.Count == 0)
        {
            currentState = GameStates.NextLevelState;
        }
    }

    private void GameOver()
    {
        PanelController(GamePanels.GameOverPanel);

        ActivateMovement(false);
    }

    private void ActivateMovement(bool activate)
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = activate;

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyFSM>().enabled = activate;
        }
    }
    private void NextLevel()
    {
        PanelController(GamePanels.NextLevelPanel);
    }

    public void ClickStartButton()
    {
        currentState = GameStates.InGameState;
    }

    public void RestartGame()
    {
        var firstScene = 0;
        SceneManager.LoadScene(firstScene);
    }
}
