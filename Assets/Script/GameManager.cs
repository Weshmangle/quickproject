using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float DistanceTraveled; // score
    public bool IsGameOver = true;

    private string _filePath;
    private float _actualDist, _bestDist;

<<<<<<< HEAD
    public static GameManager Instance;

    public float distanceTraveled;

    float actualDist, bestDist;
    
=======
    [SerializeField] private GameObject _boutonStart;

>>>>>>> 954ee498c714df8d3bb3fe48be9e3ca9ecbee6f7
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
            DistanceTraveled += MapManager.Instance.MapDefilementSpeed * Time.deltaTime;
            UIManager.Instance.SetScore(DistanceTraveled);
        }
    }
    public void GameOver()
    {
<<<<<<< HEAD
        actualDist = distanceTraveled;
        UIManager.Instance.SetLastScore(distanceTraveled);
        if (actualDist > bestDist)
=======
        Debug.Log(("game over", Application.persistentDataPath));

        _actualDist = DistanceTraveled;
        UIManager.Instance.SetLastScore(DistanceTraveled);
        if (_actualDist > _bestDist)
>>>>>>> 954ee498c714df8d3bb3fe48be9e3ca9ecbee6f7
        {
            _bestDist = _actualDist;
            UIManager.Instance.SetBestScore(_bestDist);
        }
        DistanceTraveled = 0.0f;

        SaveScore(_bestDist, _actualDist);

        MapManager.Instance.DeleteMap();
        IsGameOver = true;
        _boutonStart.SetActive(true);
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
<<<<<<< HEAD
    
=======

>>>>>>> 954ee498c714df8d3bb3fe48be9e3ca9ecbee6f7
    public void GameStart()
    {
        IsGameOver = false;
        MapManager.Instance.CreateMap();
    }
}
