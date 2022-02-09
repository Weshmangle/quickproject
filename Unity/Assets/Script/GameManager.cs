using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private int _scorePointForAugmentGameSpeed = 15;
    [SerializeField] private float _speedGameAddValue = .15f; 

    public static GameManager Instance;
    public int IncrementPoint = 1;
    public float IncrementPointDelay = .35f;

    public float GlobalGameSpeed = 1f;
    public bool IsGameOver
    {
        get { return _isGameOver; }
        private set
        {
            _isGameOver = value;
            if (_isGameOver)
            {
                Time.timeScale = 0;
            }
        }
    }

    private int _distanceTraveled;
    private int _bestDist;
    private float remainingTimeBeforeAddScore = 1;
    private string _filePath;
    private bool _isGameOver = true;
    private bool _speedGame = false;
    private bool _firstStartGame = true;

    public event GameSpeedChanged OnGameSpeedChanged;
    public event GameSpeedChanged OnGameSpeedReset;
    public delegate void GameSpeedChanged();

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/score.txt";

        if (Instance != null)
        {
            Debug.LogError("Instance of GameManager already exist");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        DisplayAllScore();
        remainingTimeBeforeAddScore = IncrementPointDelay;
        AudioManager.Instance.StartMusic();
    }

    private void Update()
    {
/*        if(Input.GetMouseButton(0) && IsGameOver)
        {
            IsGameOver = false;
            GameStart();
        }*/

        var scale = ground.transform.localScale;
        //scale.x = Screen.width / 15;
        ground.transform.localScale = scale;

        if (!IsGameOver)
        {
            remainingTimeBeforeAddScore -= Time.deltaTime;

            if (remainingTimeBeforeAddScore <= 0)
            {
                remainingTimeBeforeAddScore = IncrementPointDelay;
                _distanceTraveled += IncrementPoint;
                UIManager.Instance.SetScore(_distanceTraveled);

                if (_distanceTraveled % _scorePointForAugmentGameSpeed == 0)
                {
                    _speedGame = true;
                }
            }

            if (_speedGame)
            {
                GlobalGameSpeed += _speedGameAddValue;
                OnGameSpeedChanged?.Invoke();
                _speedGame = false;
            }
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        SpawnObstaclesManager.Instance.StopSpawn();
        UIManager.Instance.ShowMainText(true);
        UIManager.Instance.SetTextMain("Game Over \n Touch screen \nfor restart");
        if (_distanceTraveled > _bestDist)
        {
            _bestDist = _distanceTraveled;
            UIManager.Instance.SetBestScore(_bestDist);
        }
        
        SaveScore(_bestDist, _distanceTraveled);

        _distanceTraveled = 0;
        UIManager.Instance.ShowHideBestText(true);
    }


    private void SaveScore(int bestScore, int actualScore)
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, string.Empty);
        }

        DataScore score = new DataScore() { HighScore = bestScore, LastScore = actualScore };
        File.WriteAllText(_filePath, JsonUtility.ToJson(score, true));
    }


    private void DisplayAllScore()
    {
        DataScore score = RetrieveScore();

        UIManager.Instance.SetBestScore(score.HighScore);
        UIManager.Instance.SetLastScore(score.LastScore);
        UIManager.Instance.SetScore(_distanceTraveled);
    }

    private DataScore RetrieveScore()
    {
        DataScore score = new DataScore() { HighScore = 0, LastScore = 0 };

        if (File.Exists(_filePath))
        {
            string fileTxt = File.ReadAllText(_filePath);
            DataScore savedScore = JsonUtility.FromJson<DataScore>(fileTxt);

            score.HighScore = savedScore.HighScore;
            score.LastScore = savedScore.LastScore;
        }

        _bestDist = score.HighScore;

        return score;
    }

    public void GameStart()
    {
        if (!_firstStartGame)
        {
            OnGameSpeedReset?.Invoke();
        }
        
        GlobalGameSpeed = 1f;
        SpawnObstaclesManager.Instance.DeleteAllObstacles();
        UIManager.Instance.ShowMainText(false);
        IsGameOver = false;
        Time.timeScale = 1;
        _distanceTraveled = 0;
        SpawnObstaclesManager.Instance.StartSpawn();
        SpawnEnvironement.Instance.StartSpawn();
        _firstStartGame = false;
    }
}