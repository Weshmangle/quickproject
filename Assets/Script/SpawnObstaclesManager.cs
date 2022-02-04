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
            var prefab = GetRandomPrefab();
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            instance.name = prefab.name;
            var scale = Random.Range(.5f, 1f);
            instance.transform.localScale = new Vector3(scale, scale, scale);
            instance.transform.localPosition = Vector3.zero;
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
