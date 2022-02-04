using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float JumpForce = 10.0f;

    private bool _isGrounded;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    public void Die()
    {
        GameManager.Instance.GameOver();
    }

}
