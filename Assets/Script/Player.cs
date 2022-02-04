using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody myRb;
    public float jumpHeight;
    public Vector3 jump;
    public float jumpForce = 2.0f;
     
    public bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
     
    void OnCollisionStay()
    {
        isGrounded = true;
    }
     
     void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {    
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    
    public void Die()
    {
        GameManager.Instance.GameOver();
        
    }
    /*public void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            
            myRb.AddForce(Vector2.up * jumpHeight, ForceMode.Impulse);
        }
    }
   */
}
