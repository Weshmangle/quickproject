using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] crouchOrNotCrouch;
    bool isCrouch = false;
    public Vector3 jump;
    public float jumpForce = 2.0f;     
    public bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 10.0f, 0.0f);
    }
     
    void OnCollisionStay()
    {
        isGrounded = true;
    }
     
     void Update()
    {
        Jump();
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Crouch();
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            crouchOrNotCrouch[0].SetActive(true);
            crouchOrNotCrouch[1].SetActive(false);
        }
    }
    
    public void Die()
    {
        GameManager.Instance.GameOver();
        
    }
    public void Crouch()
    {
            
        Debug.Log("right shift");
        crouchOrNotCrouch[0].SetActive(false);
        crouchOrNotCrouch[1].SetActive(true);
        isCrouch = true;       
        
    }
    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {    
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            crouchOrNotCrouch[0].SetActive(true);
        crouchOrNotCrouch[1].SetActive(false);
        }
    }
    
}
