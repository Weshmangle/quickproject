using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnvironement : MonoBehaviour
{
    public static SpawnEnvironement Instance;
    public GameObject prefabCloud;
    public GameObject prefabGround;
    public bool _spawn = true;
    private List<GameObject> clouds = new List<GameObject>();
    private List<GameObject> grounds = new List<GameObject>();

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
        StartCoroutine(SpawnClouds());
        StartCoroutine(SpawnGrounds());
    }

    public void Update()
    {
        if(!GameManager.Instance.IsGameOver)
        {
            foreach (var cloud in clouds)
            {
                var position = cloud.transform.position;
                position.x -= .1f;
                cloud.transform.position = position;
            }

            foreach (var ground in grounds)
            {
                var position = ground.transform.position;
                position.x -= 20 * Time.deltaTime;
                ground.transform.position = position;
            }
        }
    }

    private IEnumerator SpawnClouds()
    {
        while (_spawn)
        {   
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));

            InstantiateCloud();
        }
    }

    private IEnumerator SpawnGrounds()
    {
        while (_spawn)
        {   
            yield return new WaitForSeconds(Random.Range(.1f, .2f));

            for (var i = 0; i < Random.Range(5, 10); i++)
            {
                InstantiateGround();
            }
        }
    }

    private void InstantiateCloud()
    {
        GameObject instance = Instantiate(prefabCloud, transform);
        instance.name = prefabCloud.name;
        instance.transform.localPosition = new Vector3(50, Random.Range(5f, 15f), 15);
        clouds.Add(instance);
    }
    private void InstantiateGround()
    {
        GameObject instance = Instantiate(prefabGround, transform);
        instance.name = prefabGround.name;
        instance.transform.localPosition = new Vector3(50 + Random.Range(-5, .5f), Random.Range(.07f, .125f), Random.Range(-4f, 4f));
        instance.transform.localScale = new Vector3(Random.Range(.125f/2, .125f), 1, Random.Range(.125f/2, .125f));
        grounds.Add(instance);
    }
}