using UnityEngine;

public class Player : MonoBehaviour
{
    public void Die()
    {
        GameManager.Instance.GameOver();
    }
}
