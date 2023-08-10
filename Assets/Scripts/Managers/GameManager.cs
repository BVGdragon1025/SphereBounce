using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    private int _currentScore = 0;

    [SerializeField]
    private TextMeshProUGUI _comboText;
    private int _currentCombo = 0;
    public int CurrentCombo { get { return _currentCombo; } set { _currentCombo = value; } }

    [SerializeField]
    private GameObject _menuCanvas;
    [SerializeField]
    private GameObject _scoreCanvas;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ShowMenu();
        Time.timeScale = 0.0f;
    }

    public void AddScore()
    {
        _currentScore += _currentCombo;
        UpdateScore(_currentScore);
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
        UpdateCombo();

    }

    private void UpdateCombo()
    {
        _comboText.text = $"Combo: {_currentCombo}";
    }

    private void ShowMenu()
    {
        if (!_menuCanvas.activeInHierarchy)
        {
            _menuCanvas.SetActive(true); 
        }
        _scoreCanvas.SetActive(false);
    }

    public void ShowScore(bool isScoreActive)
    {
        _scoreCanvas.SetActive(isScoreActive);
        StartGame();
    }

    private void StartGame()
    {
        _menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

}
