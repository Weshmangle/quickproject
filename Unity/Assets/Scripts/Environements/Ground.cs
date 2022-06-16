using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Environnement
{

    protected override void ResetPosition()
    {
        transform.localPosition = new Vector3(30 + Random.Range(0f, 100f), Random.Range(.025f, .05f), Random.Range(-5f, 5f));
    }

    protected override void Move()
    {
        var position = transform.position;
        position.x -= SpawnObstaclesManager.Instance._moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}