using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float DistanceTraveled; // score
    public bool IsGameOver = false;
    private string _filePath;
    private float _actualDist, _bestDist;

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
    
    private void Update()
    {
        if (!IsGameOver)
        {
            DistanceTraveled += SpawnObstaclesManager.Instance.Delay * Time.deltaTime;
            UIManager.Instance.SetScore(DistanceTraveled);
        }
    }

    public void GameOver()
    {
        _actualDist = DistanceTraveled;
        UIManager.Instance.SetLastScore(DistanceTraveled);
        if (_actualDist > _bestDist)
        {
            _bestDist = _actualDist;
            UIManager.Instance.SetBestScore(_bestDist);
        }
        DistanceTraveled = 0.0f;

        SaveScore(_bestDist, _actualDist);

        //SpawnManager.Instance.DeleteMap();
        IsGameOver = true;
        Time.timeScale = 0;
    }


    private void SaveScore(float bestScore, float actualScore)
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
        IsGameOver = false;
        Time.timeScale = 1;
        //SpawnManager.Instance.CreateMap();
    }
}