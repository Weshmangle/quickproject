using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get { return _instance; } }
    private static UIManager _instance;

    [SerializeField] private Text _scoreText, _lastScoreText, _bestScoreText;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        if (_instance != null)
        {
            Debug.LogError("UIManager is already instanciate");
            return;
        }
        _instance = this;
    }

    private void Update()
    {
        var position = transform.position;
        //Screen.width 
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
