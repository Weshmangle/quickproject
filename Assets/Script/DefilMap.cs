using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefilMap : MonoBehaviour
{
    public float defilSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float myX = this.transform.position.x;
        float myY = this.transform.position.y;
        float myZ = this.transform.position.z;
        this.transform.position = new Vector3(myX - defilSpeed*Time.deltaTime, myY, myZ);
        /*
        Vector3 pos = transform.position;
        pos.x -= defilSpeed * Time.deltaTime;
        transform.position = pos;*/
        if (this.transform.position.x <= -100)
        {
            DeleteObject();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {        
        Destroy(this.gameObject);
    }
    public void DeleteObject()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && this.transform.tag == "Enemy")
        {
            GameManager.GameOver();
        }
    }
}
