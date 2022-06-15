using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnvironement : MonoBehaviour
{
    public static SpawnEnvironement Instance;
    public Environnement prefabCloud;
    public Environnement prefabGround;
    public bool _spawn = true;

    public void SpawnGround() 
    {
        for (int i = 0; i < 100; i++)
        {
            InstantiateGround();
        }
    }

    public void SpawnClouds()
    {
        Vector3 position = Vector3.zero;
        
        for (int i = 0; i < 5; i++)
        {
            position = InstantiateCloud(position.x);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of SpawnEnvironement already exist");
            return;
        }

        Instance = this;
    }
    
    public void StartSpawn()
    {
        SpawnGround();
        SpawnClouds();
    }

    private Vector3 InstantiateCloud(float originPosition)
    {
        Environnement instance = Instantiate<Environnement>(prefabCloud, transform);
        instance.name = prefabCloud.name;
        instance.type = "cloud";
        instance.transform.localPosition = new Vector3(originPosition + 25 + (50 * Random.Range(0.5f,1)), Random.Range(5f, 15f), 15);
        return instance.transform.position;
    }

    private void InstantiateGround()
    {
        Environnement instance = Instantiate<Environnement>(prefabGround, transform);
        instance.type = "ground";
        instance.name = prefabGround.name;
        instance.transform.localPosition = new Vector3(30 + Random.Range(0f, 100f), Random.Range(.025f, .05f), Random.Range(-5f, 5f));
        var scale = Random.Range(.125f/2, .125f);
        
        if(Random.Range(0,1f) < 0.95)
        {
            instance.transform.localScale = new Vector3(scale*2, 1, scale*2);
        }
        else
        {
            instance.transform.localScale = new Vector3(Random.Range(0.5f, 1f), 1, scale);
        }
    }
}