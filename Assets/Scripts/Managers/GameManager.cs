using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    private int _currentScore = 0;

    [SerializeField]
    private TextMeshProUGUI _comboText;
    private int _currentCombo = 0;
    public int CurrentCombo { get { return _currentCombo; } set { _currentCombo = value; } }
    private int _maxCombo = 0;

    [SerializeField]
    private TextMeshProUGUI _finalScoreText;
    [SerializeField]
    private TextMeshProUGUI _maxComboText;

    [Header("Game Objects with Canvas")]
    [SerializeField]
    private GameObject _menuCanvasObject;
    [SerializeField]
    private GameObject _scoreCanvasObject;
    [SerializeField]
    private GameObject _gameOverCanvasObject;

    [Header("Not pooled platforms")]
    [SerializeField]
    private GameObject[] _notPooled = new GameObject[4];

    [Header("Variables")]
    public bool isGameStarted = false;

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

    }

    public void AddScore()
    {
        _currentScore += _currentCombo;
        UpdateScore(_currentScore);
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
        _finalScoreText.text = $"Final Score: {score}";
        UpdateCombo();

    }

    private void UpdateCombo()
    {
        _comboText.text = $"Combo: {_currentCombo}";

        if(_maxCombo < _currentCombo)
        {
            _maxCombo = _currentCombo;
        }

        _maxComboText.text = $"Max Combo: {_maxCombo}";
    }

    private void ShowMenu()
    {
        if (!_menuCanvasObject.activeInHierarchy)
        {
            _menuCanvasObject.SetActive(true); 
        }
        _scoreCanvasObject.SetActive(false);
        _gameOverCanvasObject.SetActive(false);
    }

    public void ShowScore(bool isScoreActive)
    {
        _scoreCanvasObject.SetActive(isScoreActive);
        StartGame();
    }

    private void StartGame()
    {
        _menuCanvasObject.SetActive(false);
        isGameStarted = true;
        SphereController.Instance.sphereRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }


    public void GameOver()
    {
        SphereController.Instance.MarkAsDead(true);
        isGameStarted = false;
        _scoreCanvasObject.SetActive(false);
        _gameOverCanvasObject.SetActive(true);

    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        _gameOverCanvasObject.SetActive(false);
        _scoreCanvasObject.SetActive(true);
        
        _currentCombo = 0;
        _currentScore = 0;
        UpdateScore(_currentScore);

        for(int i = 0; i < _notPooled.Length; i++)
        {
            _notPooled[i].GetComponent<Platform>().ResetPlatformPosition();

            if(!_notPooled[i].activeInHierarchy)
                _notPooled[i].SetActive(true);
        }

        SphereController.Instance.ResetPlayerPosition();
        SphereController.Instance.MarkAsDead(false);

        isGameStarted = true;
       
    }

}
