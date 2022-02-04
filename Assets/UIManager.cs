using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get{return _instance;}}
    private static UIManager _instance;
    
    [SerializeField] private Text scoreText, lastScoreText, bestScoreText;

    private void Start()
    {
        if(_instance != null)
        {
            Debug.LogError("UIManager is already instanciate");
            return;
        }
        _instance = this;
    }
    
    public void SetScore(float score)
    {
        scoreText.text = $"Distance parcourue: {score}";
    }

    public void SetBestScore(float score)
    {
        bestScoreText.text = $" Plus loin attein: {score}";
    }

    public void SetLastScore(float score)
    {
        lastScoreText.text = $"Derniere distance atteinte: {score}";
    }
}
