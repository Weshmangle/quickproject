using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _XPosForDestroy = -100f;

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        
        performMovement();

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

    private void performMovement()
    {
        Vector3 pos = this.transform.position;
        pos.x -= _moveSpeed * Time.deltaTime;
        this.transform.position = pos;
    }
}
