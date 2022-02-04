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
    public bool SpawnObstacle
    {
        get
        {
            return _spawnObstacle;
        }
        private set
        {
            _spawnObstacle = value;
            if (!_spawnObstacle)
            {
                StopAllCoroutines();
            }
        }
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

    private void Start()
    {
        StartSpawn();
    }

    private IEnumerator SpawnAfterTime()
    {
        while (_spawnObstacle)
        {
            yield return new WaitForSeconds(_delay);

            var instance = Instantiate(GetRandomPrefab(), Vector3.zero, Quaternion.identity, transform);
            var scale = Random.Range(.5f, 1f);
            instance.transform.localScale = new Vector3(scale, scale, scale);
            instance.transform.localPosition = Vector3.zero;
        }
    }
    private GameObject GetRandomPrefab()
    {
        return _prefabs[Random.Range(0, _prefabs.Length)];
    }

    private void StartSpawn()
    {
        StartCoroutine(SpawnAfterTime());
    }
}
