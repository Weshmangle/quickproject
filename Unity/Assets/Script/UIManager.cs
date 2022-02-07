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
        string a = "0";
        string uiScore =  "";
        int maxDeZero = 4 - score.ToString().Length; 
        for(int i = 0; i < maxDeZero; i++)       
        {
            uiScore += a;
        }
        _scoreText.text = uiScore + score.ToString();
       
    }

    public void SetBestScore(int score)
    {
        string a = "0";
        string uiScore =  "";
        int maxDeZero = 4 - score.ToString().Length; 
        for(int i = 0; i < maxDeZero; i++)       
        {
            uiScore += a;
        }
        _bestScoreText.text = "HI:" + uiScore + score.ToString();
        //_bestScoreText.text = $"HI:{score}";
    }

    public void SetLastScore(int score)
    {
        _lastScoreText.text = $"LAST:{score}";
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
