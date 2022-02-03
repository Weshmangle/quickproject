using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float distanceTraveled; // score
    public Text scoreText, lastScoreText, bestScoreText;

    float actualDist, bestDist;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Instance of GameManager already exist");
            return;
        }

        Instance = this;
    }
    private void Update()
    {
        distanceTraveled += MapManager.mapDefilementSpeed*Time.deltaTime;
        scoreText.text = $"Distance parcourue: {distanceTraveled}";  
    }
    public void GameOver()
    {
        Debug.Log("game over");
        actualDist = distanceTraveled;
        lastScoreText.text = $"Derniere distance atteinte: {distanceTraveled}"; 
        if (actualDist > bestDist)
        {
            bestDist = actualDist;
            bestScoreText.text = $" Plus loin attein: {bestDist}";
        }
        distanceTraveled = 0.0f;
    }
}
