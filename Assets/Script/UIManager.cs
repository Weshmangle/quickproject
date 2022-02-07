using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Text _scoreText, _lastScoreText, _bestScoreText;
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of UIManager is already exist");
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        var position = transform.position;
    }

    public void SetScore(int score)
    {
        _scoreText.text = $"Distance: {score}";
    }

    public void SetBestScore(int score)
    {
        _bestScoreText.text = $"Best: {score}";
    }

    public void SetLastScore(int score)
    {
        _lastScoreText.text = $"Last: {score}";
    }

    public void StartGame()
    {
        GameManager.Instance.GameStart();
    }

    public void ShowHideStartButton(bool value)
    {
        _startButton.gameObject.SetActive(value);
    }
}
