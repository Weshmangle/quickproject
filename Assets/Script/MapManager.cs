using System.Collections;
using System.Collections.Generic;
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
    public GameObject[] _obstacle, _bonus, _floor, objectExistList;
    public static float mapDefilementSpeed = 20;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of MapManager already exist");
            return;
        }

        Instance = this;
    }
    public void GenerateNewMap()
    {
        
    }
    public void DeleteOldMap()
    {
        
    }

    //test
    public void CreatMap()
    {
        GameObject floor = Instantiate(_floor[0], new Vector3(0, 0, 0), Quaternion.identity);
        floor.transform.SetParent(this.transform);
        floor.GetComponent<DefilMap>().defilSpeed = mapDefilementSpeed;
    }
}
