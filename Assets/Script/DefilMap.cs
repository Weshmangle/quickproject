using UnityEngine;

public class DefilMap : MonoBehaviour
{
    [HideInInspector] public float defilSpeed = 10;

    void Update()
    {
        Movement();

        if (this.transform.position.x <= -100)
        {
            DeleteObject();
        }
    }

    private void Movement()
    {
        float myX = this.transform.position.x;
        float myY = this.transform.position.y;
        float myZ = this.transform.position.z;
        this.transform.position = new Vector3(myX - defilSpeed * Time.deltaTime, myY, myZ);
    }

    private void DeleteObject()
    {
        Destroy(this.gameObject);
    }
}
