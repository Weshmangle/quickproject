using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environnement : MonoBehaviour
{
    [SerializeField] private float _XPosForDestroy = -35f;

    #region Unity API
    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        if(!GameManager.Instance.IsGameOver)
        {    
           ProcessMove();
        }
    }
    #endregion

    protected abstract void ResetPosition();

    protected abstract void Move();

    protected void ProcessMove()
    {
        if(transform.position.x < _XPosForDestroy)
        {
            ResetPosition();
        }
        else
        {
            Move();
        }
    }
}