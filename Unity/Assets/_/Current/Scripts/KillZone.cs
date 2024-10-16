using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
             other.GetComponentInParent<Player>().Die();
        }
    }
}