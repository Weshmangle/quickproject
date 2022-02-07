using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private GameObject [] prefabs;
    void Start()
    {
        var indexPrefeb = Random.Range(0, prefabs.Length);

        foreach (var item in prefabs)
        {
            item.SetActive(false);
        }

        prefabs[indexPrefeb].SetActive(true);
    }
}
