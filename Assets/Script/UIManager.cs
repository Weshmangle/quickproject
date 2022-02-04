using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get { return _instance; } }
    private static UIManager _instance;

    [SerializeField] private Text _scoreText, _lastScoreText, _bestScoreText;

    private void Start()
    {
        if (_instance != null)
        {
            Debug.LogError("UIManager is already instanciate");
            return;
        }
        _instance = this;
    }

    public void SetScore(float score)
    {
        _scoreText.text = $"Distance parcourue: {score}";
    }

    public void SetBestScore(float score)
    {
        _bestScoreText.text = $" Plus loin attein: {score}";
    }

    public void SetLastScore(float score)
    {
        _lastScoreText.text = $"Derniere distance atteinte: {score}";
    }

    public void StartGame()
    {
        GameManager.Instance.GameStart();
    }
}
