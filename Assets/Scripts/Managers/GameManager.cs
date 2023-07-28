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

}
