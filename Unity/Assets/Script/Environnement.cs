using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environnement : MonoBehaviour
{
    [SerializeField] private float _XPosForDestroy = -30f;
    public string type;
    
    void Start()
    {
    }

    public void ResetPositionGround()
    {
        transform.localPosition = new Vector3(50 + Random.Range(-5, .5f), Random.Range(.025f, .05f), Random.Range(-4f, 4f));
    }

    protected void moveGround()
    {
        var position = transform.position;
        position.x -= 20 * Time.deltaTime;
        transform.position = position;
    } 

    protected void UpdateGround()
    {
        if(transform.position.x < _XPosForDestroy)
        {
            ResetPositionGround();
        }
        else
        {
            if(!GameManager.Instance.IsGameOver)
            {
                moveGround();
            }
        }
    }

    void Update()
    {
        switch (type)
        {
            case "ground":
                UpdateGround();
                break;
            case "cloud":
                break;
        }
    }
}