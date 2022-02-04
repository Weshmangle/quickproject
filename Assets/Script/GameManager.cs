using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int IncrementPoint = 1;
    public float IncrementPointDelay = .35f;
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
        remainingTimeBeforeAddScore = IncrementPointDelay;
    }

    private void Update()
    {
        if (!IsGameOver)
        {            
            remainingTimeBeforeAddScore -= Time.deltaTime;

            if(remainingTimeBeforeAddScore <= 0)
            {
                remainingTimeBeforeAddScore = IncrementPointDelay;
                _distanceTraveled += IncrementPoint;
                UIManager.Instance.SetScore(_distanceTraveled);
            }
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        SpawnObstaclesManager.Instance.StopSpawn();
        UIManager.Instance.SetLastScore(_distanceTraveled);
        if (_distanceTraveled > _bestDist)
        {
            _bestDist = _distanceTraveled;
            UIManager.Instance.SetBestScore(_bestDist);
        }
        SaveScore(_bestDist, _distanceTraveled);

        _distanceTraveled = 0;
        UIManager.Instance.ShowHideStartButton(true);
    }


    private void SaveScore(int bestScore, int actualScore)
    {
        //TODO try except
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, string.Empty);
        }

        DataScore score = new DataScore() { HighScore = bestScore, LastScore = actualScore };
        File.WriteAllText(_filePath, JsonUtility.ToJson(score));
    }

    public void GameStart()
    {
        SpawnObstaclesManager.Instance.DeleteAllObstacles();
        UIManager.Instance.ShowHideStartButton(false);
        IsGameOver = false;
        Time.timeScale = 1;
        _distanceTraveled = 0;
        SpawnObstaclesManager.Instance.StartSpawn();
    }
}