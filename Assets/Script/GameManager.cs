using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private string filePath;
    
    public static bool gameOver = true;

    public GameObject boutonStart;

    public static GameManager Instance;

    public float distanceTraveled;

    float actualDist, bestDist;
    
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/score.txt";
        
        if(Instance != null)
        {
            Debug.LogError("Instance of GameManager already exist");
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!gameOver)
        {
            distanceTraveled += MapManager.mapDefilementSpeed * Time.deltaTime;
            UIManager.Instance.SetScore(distanceTraveled);
        }
    }
    public void GameOver()
    {
        actualDist = distanceTraveled;
        UIManager.Instance.SetLastScore(distanceTraveled);
        if (actualDist > bestDist)
        {
            bestDist = actualDist;
            UIManager.Instance.SetBestScore(bestDist);
        }
        distanceTraveled = 0.0f;
        
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
        
        DataScore score;
        score.highScore = bestDist;
        score.lastScore = actualDist;
        File.WriteAllText(filePath, JsonUtility.ToJson(score));

        MapManager.Instance.DeleteOldMap();
        gameOver = true;
        boutonStart.SetActive(true);
    }
    
    public void GameStart()
    {
        gameOver = false;        
        MapManager.Instance.CreatMap();
        
    }
}
