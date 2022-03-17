using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnvironement : MonoBehaviour
{
    public static SpawnEnvironement Instance;
    public Environnement prefabCloud;
    public Environnement prefabGround;
    public bool _spawn = true;
    private List<Environnement> clouds = new List<Environnement>();
    private List<Environnement> grounds = new List<Environnement>();
    private IEnumerator IEClouds;
    private IEnumerator IEGrounds;

    public void SpawnFirstGround() 
    {
        for (int i = 0; i < 100; i++)
        {
            InstantiateGround();
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
        foreach (var cloud in clouds)
        {
            Destroy(cloud.gameObject);
        }

        foreach (var ground in grounds)
        {
            Destroy(ground.gameObject);
        }

        clouds.Clear();
        grounds.Clear();

        SpawnFirstGround();

        if(IEClouds != null)
        {
            StopCoroutine(IEClouds);
        }

        if(IEGrounds != null)
        {
            StopCoroutine(IEGrounds);
        }

        IEClouds = SpawnClouds();
        
        StartCoroutine(IEClouds);
    }

    public void Update()
    {
    }

    private IEnumerator SpawnClouds()
    {
        while (_spawn)
        {   
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

            InstantiateCloud();
        }
    }

    private void InstantiateCloud()
    {
        Environnement instance = Instantiate<Environnement>(prefabCloud, transform);
        instance.name = prefabCloud.name;
        instance.type = "cloud";
        instance.transform.localPosition = new Vector3(50, Random.Range(5f, 15f), 15);
        clouds.Add(instance);
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
        
        grounds.Add(instance);
    }
}