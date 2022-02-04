using UnityEngine;

public class Player : MonoBehaviour
{
    public float JumpForce = 10.0f;

    [SerializeField] private GameObject[] _meshes;

    private bool _isCrouch = false;
    private bool _isGrounded;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
        Crouch();
        ShowHideCrouchMesh();
    }

    public void Die()
    {
        GameManager.Instance.GameOver();
    }

    private void ShowHideCrouchMesh()
    {
        if (_isCrouch)
        {
            _meshes[0].SetActive(false);
            _meshes[1].SetActive(true);
        }
        else
        {
            _meshes[0].SetActive(true);
            _meshes[1].SetActive(false);
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isCrouch = true;
        }
        else
        {
            _isCrouch = false;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _meshes[0].SetActive(true);
            _meshes[1].SetActive(false);
        }
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

}
