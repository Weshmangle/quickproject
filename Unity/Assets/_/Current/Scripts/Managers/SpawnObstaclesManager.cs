using UnityEngine;
using System.Collections;

public class SpawnObstaclesManager : MonoBehaviour
{
    public static SpawnObstaclesManager Instance;

    [SerializeField] private float _reduceDelayValue = .25f;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _minYPosForNotGroundedObject = 1f;
    [SerializeField] private float _maxYPosForNotGroundedObject = 3f;
    [SerializeField] public float _moveSpeed = 20f;
    protected float offsetLimit;
    [SerializeField]
    public Transform limitNextObsatcle;
    public Transform currentNextObsatcle;
    protected GameObject lastObstacle;
    private bool _spawnObstacle = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of MapManager already exist");
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if(lastObstacle && lastObstacle.transform.position.x <= currentNextObsatcle.position.x)
        {
            SpawnAfterTime();
        }
    }

    private void SpawnAfterTime()
    {
        var rating = Random.Range(0, 1f);

        if(rating <= .75)
            InstantiateCactus();
        else
        {
            InstantiateBird();
            _moveSpeed += 1;
            IncreaseNextObstacle(_reduceDelayValue);
        }
    }

    private void IncreaseNextObstacle(float value)
    {
        offsetLimit += _reduceDelayValue;
        float factor = -Mathf.Pow(2, -offsetLimit) + 1;
        Vector3 position = currentNextObsatcle.position;
        position.x = limitNextObsatcle.position.x * factor;
        currentNextObsatcle.position = position;
    }

    private void InstantiateCactus()
    {
        GameObject prefab = GetPrefabAt(0);

        for (var i = 0; i < Random.Range(1, 5); i++)
        {
            var rot = Quaternion.AngleAxis(180 * Random.Range(0, 1), Vector3.up);

            GameObject instance = Instantiate(prefab, Vector3.zero, rot, transform);
            instance.transform.localScale = instance.transform.localScale * Random.Range(1.5f, 2.5f);
            instance.name = prefab.name;
            instance.transform.localPosition = new Vector3(i * (Random.Range(.75f, 1) + instance.transform.localScale.x * .25f), 0, 0);
            lastObstacle = instance;
        }
    }

    private void InstantiateBird()
    {
        GameObject prefab = GetPrefabAt(1);
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);

        float scale = Random.Range(1.25f, 1.5f);
        instance.transform.localScale = new Vector3(scale, scale, scale);

        float y = Random.Range(_minYPosForNotGroundedObject, _maxYPosForNotGroundedObject);
        
        instance.transform.localPosition = new Vector3(Random.Range(0,10f), y, 0f);
        
        lastObstacle = instance;
    }

    private GameObject GetPrefabAt(int index)
    {
        return _prefabs[index];
    }

    public void StartSpawn()
    {
        _spawnObstacle = true;
        _moveSpeed = 20f;
        offsetLimit = 0f;
        IncreaseNextObstacle(0);
        SpawnAfterTime();
    }

    public void StopSpawn()
    {
        _spawnObstacle = false;
    }

    public void DeleteAllObstacles()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(limitNextObsatcle.position, new Vector3(.10f, 10, 10));
    }
}
