using UnityEngine;

public class DefilMap : MonoBehaviour
{
    [HideInInspector] public float DefilSpeed = 10;

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {

        }
        else
        {
            Movement();

            if (DeleteNeeded())
            {
                DeleteObject();
            }
        }        
    }

    private void Movement()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(pos.x - DefilSpeed * Time.deltaTime, pos.y, pos.y);
    }

    private bool DeleteNeeded()
    {
        return this.transform.position.x <= -100;
    }

    private void DeleteObject()
    {
        Destroy(this.gameObject);
    }
}
