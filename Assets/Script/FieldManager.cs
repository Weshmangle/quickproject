using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public TerrainLayer fieldSynth;
    public float offsetSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.IsGameOver)
        {
            float offset = Time.deltaTime * offsetSpeed;        
            fieldSynth.tileOffset = new Vector2(fieldSynth.tileOffset.x+offset,0);      
        }
          
        
    }
}
