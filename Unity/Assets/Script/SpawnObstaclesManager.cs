using UnityEngine;
using System.Collections;

public class SpawnObstaclesManager : MonoBehaviour
{
    public static SpawnObstaclesManager Instance;

    [SerializeField] private float _minDelay = .5f;
    [SerializeField] private float _maxDelay = 2f;
    [SerializeField] private float _reduceDelayValue = .25f;

    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _minYPosForNotGroundedObject = 1f;
    [SerializeField] private float _maxYPosForNotGroundedObject = 3f;

    private bool _spawnObstacle = true;

    private float _maxDelayForSpawn;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of MapManager already exist");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _maxDelayForSpawn = _maxDelay;
        GameManager.Instance.OnGameSpeedChanged += ReduceSpawnDelay;
        GameManager.Instance.OnGameSpeedReset += ResetSpawnDelay;
    }

    private void ReduceSpawnDelay()
    {
        if (_maxDelayForSpawn -.1f <= _minDelay) return;
        _maxDelayForSpawn -= _reduceDelayValue;
    }

    private void ResetSpawnDelay()
    {
        _maxDelayForSpawn = _maxDelay;
    }

    private IEnumerator SpawnAfterTime()
    {
        while (_spawnObstacle)
        {
            float random = Random.Range(_minDelay, _maxDelayForSpawn);
            yield return new WaitForSeconds(random);

            switch (Random.Range(0, _prefabs.Length))
            {
                case 0:
                    InstantiateCactus();
                    break;
                case 1:
                    InstantiateBird();
                    break;
            }
        }
    }

    private void InstantiateCactus()
    {
        GameObject prefab = GetPrefabAt(0);

        for (var i = 0; i < Random.Range(1, 3); i++)
        {
            var rot = Quaternion.AngleAxis(180 * Random.Range(0, 1), Vector3.up);

            GameObject instance = Instantiate(prefab, Vector3.zero, rot, transform);
            instance.transform.localScale = instance.transform.localScale * Random.Range(1f, 2.5f);
            instance.name = prefab.name;
            //instance.transform.FindChild("cactus_1");
            instance.transform.localPosition = new Vector3(i * Random.Range(.75f, 1), 0, 0);
        }
    }

    private void InstantiateBird()
    {
        GameObject prefab = GetPrefabAt(1);
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);

        float scale = Random.Range(.75f, 1.5f);
        instance.transform.localScale = new Vector3(scale, scale, scale);

        float y = Random.Range(_minYPosForNotGroundedObject, _maxYPosForNotGroundedObject);
        
        instance.transform.localPosition = new Vector3(0f, y, 0f);
    }

    private GameObject GetPrefabAt(int index)
    {
        return _prefabs[index];
    }

    public void StartSpawn()
    {
        _spawnObstacle = true;
        StartCoroutine(SpawnAfterTime());
    }

    public void StopSpawn()
    {
        _spawnObstacle = false;
        StopAllCoroutines();
    }

    public void DeleteAllObstacles()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
