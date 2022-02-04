using UnityEngine;

public class MapManager : MonoBehaviour
{
    //liste de prefab (obstacle, bonus, )
    //position des object
    //position de la map
    //position limite de la map (en gros a partir de quand on suprime le vieux terrain et genere le nouveau)
    //vitesse de defilement
    //instantiation des terrain
    //supression des terrain

    public static MapManager Instance;
    private float _mapDefilementSpeed = 20;
    [SerializeField] private GameObject[] _obstacle, _bonus, _floor, _objectExistList;

    public float MapDefilementSpeed
    {
        get { return _mapDefilementSpeed; }
        private set { _mapDefilementSpeed = value; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of MapManager already exist");
            return;
        }

        Instance = this;
    }

    public void CreateMap()
    {

    }

    public void DeleteMap()
    {

    }

    public void GenerateFloor()
    {
        int index = GetRandomIndex(_floor);

        GameObject floor = Instantiate(_floor[index], Vector3.zero, Quaternion.identity, this.transform);
        floor.GetComponent<DefilMap>().DefilSpeed = MapDefilementSpeed;
    }

    private int GetRandomIndex(GameObject[] objs)
    {
        return Random.Range(0, _floor.Length);
    }
}
