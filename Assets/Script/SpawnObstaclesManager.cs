using UnityEngine;
using System.Collections;

public class SpawnObstaclesManager : MonoBehaviour
{
    //liste de prefab (obstacle, bonus, )
    //position des object
    //position de la map
    //position limite de la map (en gros a partir de quand on suprime le vieux terrain et genere le nouveau)
    //vitesse de defilement
    //instantiation des terrain
    //supression des terrain
    
    public static SpawnObstaclesManager Instance;

    private float _delay = 1;

    [SerializeField] private GameObject[] _prefabs;

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
        yield return new WaitForSeconds(_delay);

        var instance = Instantiate(GetRandomPrefab(), transform.position, Quaternion.identity, transform);
        var scale = Random.Range(0.5f, 1);
        instance.transform.localScale = new Vector3(scale, scale, scale);

        StartCoroutine(SpawnAfterTime());
    }
    private GameObject GetRandomPrefab()
    {
        return _prefabs[Random.Range(0, _prefabs.Length)];
    }

    public float Delay
    {
        get { return _delay; }
        private set { _delay = value; }
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnAfterTime());
    }
}
