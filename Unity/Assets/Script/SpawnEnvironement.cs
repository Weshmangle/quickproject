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
<<<<<<< HEAD
    [SerializeField] private float _XPosForDestroy = -100f;

=======
    public void SpawnFirstGround()
    {
        for (int i = 0; i < 100; i++)
        {
            float randx = Random.Range(-20.0f,45.0f);
            float randz = Random.Range(-3.0f,3.0f);
            GameObject ground = Instantiate(prefabGround, new Vector3(randx,0.1f,randz),Quaternion.identity);
            ground.transform.localScale = new Vector3(Random.Range(.125f/2, .125f), 1, Random.Range(.125f/2, .125f));
            grounds.Add(ground);
        }
        
    }
>>>>>>> c9b7d63897e881b17a37985433f4dabfb4344527
    private void Awake()
    {
        SpawnFirstGround();
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
                position.x -= .05f;
                cloud.transform.position = position;
            }

            foreach (var ground in grounds)
            {
                var position = ground.transform.position;
                position.x -= 20 * Time.deltaTime;
                ground.transform.position = position;
            }
        }
        else
        {
            foreach (var cloud in clouds)
            {
                Destroy(cloud);
            }

            foreach (var ground in grounds)
            {
                Destroy(ground);
            }

            clouds.Clear();
            grounds.Clear();
        }

        destroyEnvironementOutCamera();
    }

    private void destroyEnvironementOutCamera()
    {
        int index = 0;
        while(index < grounds.Count)
        {
            if(grounds[index].transform.position.x < _XPosForDestroy)
            {
                Destroy(grounds[index]);
            }
            else
            {
                index++;
            }
        }
        index = 0;
        while(index < clouds.Count)
        {
            if(clouds[index].transform.position.x < _XPosForDestroy)
            {
                Destroy(clouds[index]);
                clouds.RemoveAt(index);
            }
            else
            {
                index++;
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
        instance.transform.localPosition = new Vector3(50 + Random.Range(-5, .5f), Random.Range(.025f, .05f), Random.Range(-4f, 4f));
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