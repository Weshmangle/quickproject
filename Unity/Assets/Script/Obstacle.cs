using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _moveSpeedIncreaseValue = 1;
    [SerializeField] private float _XPosForDestroy = -100f;
    [SerializeField] private float _maxMoveSpeed = 50f;

    private float _actualMoveSpeed;

    public bool IsGroundedObject = true;

    private void Start()
    {
        _actualMoveSpeed = _moveSpeed;
        GameManager.Instance.OnGameSpeedChanged += IncreaseMoveSpeed;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameSpeedChanged -= IncreaseMoveSpeed;
    }

    private void IncreaseMoveSpeed()
    {
        if (_actualMoveSpeed >= _maxMoveSpeed) return;
        _actualMoveSpeed += _moveSpeedIncreaseValue;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        PerformMovement();

        if (NeedToBeDestroy())
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private bool NeedToBeDestroy()
    {
        return this.transform.position.x < _XPosForDestroy;
    }

    private void PerformMovement()
    {
        Vector3 pos = this.transform.position;
        pos.x -= _actualMoveSpeed * Time.deltaTime;
        this.transform.position = pos;
    }
}
