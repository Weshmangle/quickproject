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
    public float distanceTraveled; // score
    public Text scoreText, lastScoreText, bestScoreText;

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
            scoreText.text = $"Distance parcourue: {distanceTraveled}";
        }
    }
    public void GameOver()
    {
        Debug.Log(("game over", Application.persistentDataPath));

        actualDist = distanceTraveled;
        lastScoreText.text = $"Derniere distance atteinte: {distanceTraveled}"; 
        if (actualDist > bestDist)
        {
            bestDist = actualDist;
            bestScoreText.text = $" Plus loin attein: {bestDist}";
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
