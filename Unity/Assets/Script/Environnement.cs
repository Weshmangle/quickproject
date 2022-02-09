using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environnement : MonoBehaviour
{
    [SerializeField] private float _XPosForDestroy = -100f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < _XPosForDestroy)
        {
            Destroy(this);
        }
    }
}
