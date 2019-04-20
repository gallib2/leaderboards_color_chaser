using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Start game vars")]
    [SerializeField] private GameObject _startGamePanel;
    [SerializeField] private InputField _nicknameInput;

    [Header("Game Over vars")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private float _timer;
    private bool _gameStarted = false, _gameOver = false;
    private string _playerName;


    //We reset the playerprefs so when the user restarts the game it wont save it
    //Maybe this should move to GM class?
    private void Awake()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
    }

    private void Start()
    {
        _startGamePanel.SetActive(true);
    }

    //Called from GameManager cs
    public void GameOver()
    {
        _gameStarted = false;
        _gameOver = true;
        _gameOverPanel.SetActive(true);

         float score = _timer;

        if (score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            Debug.Log("New high score!");
            PlayerPrefs.SetFloat("HighScore", _timer);
        }

        _playerNameText.text = string.Format("Player name: {0}", _playerName);
        _currentScoreText.text = string.Format("Score: {0}", _timer.ToString("F0"));
        _highScoreText.text = string.Format("High Score: {0}", PlayerPrefs.GetFloat("HighScore").ToString("F0"));
    }

    private void Update()
    {
        DisplayTime();
    }

    private void DisplayTime()
    {
        if (_gameStarted && !_gameOver)
        {
            _timer += Time.deltaTime;

            _timerText.text = "Time: " + _timer.ToString("F0");
        }
    }

    #region UICallbacks
    public void OnClick_CreateNickname()
    {
        _playerName = _nicknameInput.text;
        if (!name.Equals(string.Empty))
        {
            _startGamePanel.GetComponent<Animator>().enabled = true;
            GameManager.Instance.SetupPlatformSpawner();
            GameManager.Instance.SetupPlayer();
            _gameStarted = true;
        }
        else
            Debug.Log("Please enter a nickname");
    }

    public void OnClick_Retry()
    {
        SceneManager.LoadSceneAsync(0);
    }
    #endregion


}
