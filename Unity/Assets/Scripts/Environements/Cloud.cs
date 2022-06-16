using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Environnement
{
    public float index;

    protected override void ResetPosition()
    {
        transform.position = new Vector3(index * 50 + 25 + (50 * Random.Range(0.5f,1)), Random.Range(5f, 15f), 15);
    } 

    protected override void Move()
    {
        var position = transform.position;
        position.x -= SpawnObstaclesManager.Instance._moveSpeed * Time.deltaTime * .5f;
        transform.position = position;
    }
}