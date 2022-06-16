using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnvironement : MonoBehaviour
{
    public static SpawnEnvironement Instance;
    public Cloud prefabCloud;
    public Ground prefabGround;
    public bool _spawn = true;

    void Start()
    {
        SpawnGround();
        SpawnClouds();
    }

    public void SpawnGround() 
    {
        for (int i = 0; i < 100; i++)
        {
            InstantiateGround();
        }
    }

    public void SpawnClouds()
    {
        for (int index = 0; index < 5; index++)
        {
            InstantiateCloud(index);
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
        foreach (var env in GetComponentsInChildren<Environnement>())
        {
            //env.ResetPosition();
        }
    }

    private void InstantiateCloud(int index)
    {
        Cloud instance = Instantiate<Cloud>(prefabCloud, transform);
        instance.index = index;
        //instance.name = prefabCloud.name;
        //instance.transform.localPosition = new Vector3(originPosition + 25 + (50 * Random.Range(0.5f,1)), Random.Range(5f, 15f), 15);
    }

    private void InstantiateGround()
    {
        Environnement instance = Instantiate<Environnement>(prefabGround, transform);
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