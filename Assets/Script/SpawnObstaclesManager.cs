using UnityEngine;
using System.Collections;

public class SpawnObstaclesManager : MonoBehaviour
{
    public static SpawnObstaclesManager Instance;
    public float Delay
    {
        get { return _delay; }
        private set { _delay = value; }
    }

    [SerializeField] private float _delay = 1;

    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _minYPosForNotGroundedObject = 1f;
    [SerializeField] private float _maxYPosForNotGroundedObject = 3f;

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

    private IEnumerator SpawnAfterTime()
    {
        while (_spawnObstacle)
        {
            yield return new WaitForSeconds(_delay);
            GameObject prefab = GetRandomPrefab();
            GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            instance.name = prefab.name;
            float scale = Random.Range(.5f, 1f);
            instance.transform.localScale = new Vector3(scale, scale, scale);

            if (instance.GetComponent<Obstacle>().IsGroundedObject)
            {
                instance.transform.localPosition = Vector3.zero;
            }
            else
            {
                float y = Random.Range(_minYPosForNotGroundedObject, _maxYPosForNotGroundedObject);
                instance.transform.localPosition = new Vector3(0f, y, 0f);
            }            
        }
    }
    
    private GameObject GetRandomPrefab()
    {
        return _prefabs[Random.Range(0, _prefabs.Length)];
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
