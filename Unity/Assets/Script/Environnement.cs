using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environnement : MonoBehaviour
{
    [SerializeField] private float _XPosForDestroy = -35f;
    public string type;
    
    void Start()
    {
    }

    public void ResetPositionGround()
    {
        transform.localPosition = new Vector3(50 + Random.Range(-5, .5f), Random.Range(.025f, .05f), Random.Range(-4f, 4f));
    }

    public void ResetPositionCloud()
    {
        transform.position = new Vector3(25 + (50 * Random.Range(0.5f,1)), Random.Range(5f, 15f), 15);
    }

    public void ResetPosition()
    {
        switch (type)
        {
            case "ground":
                ResetPositionGround();
                break;
            case "cloud":
                ResetPositionCloud();
                break;
        }
    }

    protected void MoveGround()
    {
        var position = transform.position;
        position.x -= SpawnObstaclesManager.Instance._moveSpeed * Time.deltaTime;
        transform.position = position;
    } 

    public void MoveCloud()
    {
        var position = transform.position;
        position.x -= SpawnObstaclesManager.Instance._moveSpeed * Time.deltaTime * .5f;
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
            MoveGround();
        }
    }

    public void UpdateCloud()
    {
        if(transform.position.x < _XPosForDestroy)
        {
            ResetPositionCloud();
        }
        else
        {
            MoveCloud();
        }
    }

    public void UpdatePosition()
    {
        switch (type)
            {
                case "ground":
                    UpdateGround();
                    break;
                case "cloud":
                    UpdateCloud();
                    break;
            }
    }

    void Update()
    {
        if(!GameManager.Instance.IsGameOver)
        {    
            UpdatePosition();
        }
    }
}