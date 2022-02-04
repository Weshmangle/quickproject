using UnityEngine;

public class Player : MonoBehaviour
{
    public float LValue, jumpHeight, LSpeed;
    Vector3 MStart, MEnd;
    [SerializeField] private GameObject[] _meshes;
    private bool _isCrouch = false;
    private bool _isJump = false;    
    private bool _isGrounded;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        MStart = this.transform.position;
        MEnd = MStart;        
        LValue = 1.0f;
        
    }

    void Update()
    {
        Jump();
        LerpJump();
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
            
            /*
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _meshes[0].SetActive(true);
            _meshes[1].SetActive(false);
            */
        }
        else
        {
            
        }
    }
    public void LerpJump()
    {
        if(_isJump)
        {
            LValue = -0.0f;
            MStart = this.transform.position;
            MEnd = this.transform.position + this.transform.up * jumpHeight*Time.deltaTime;
            LValue += LSpeed * Time.deltaTime;
        }
        else
        {
            if (LValue > 1.0f)
            {
                LValue = 1.0f;
            }
            this.transform.position = Vector3.Lerp(MStart, MEnd, LValue);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
            //_isJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = false;
            //_isJump = false;
        }
    }

}
