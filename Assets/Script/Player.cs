using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float JumpHeight;
    public Vector3 Jump;
    public float JumpForce = 2.0f;
    public bool IsGrounded;
     
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
     
    void OnCollisionStay()
    {
        IsGrounded = true;
    }
     
     void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {    
            _rb.AddForce(Jump * JumpForce, ForceMode.Impulse);
            IsGrounded = false;
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
