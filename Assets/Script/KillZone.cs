using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().Die();
        }
    }
}