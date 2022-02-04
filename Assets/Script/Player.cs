using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshes;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [SerializeField] private bool isGrounded;
    private float jumpHeight = 2f;
    private float gravity = -50f;
    public Vector3 positionGravity;

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore);
        
        if(isGrounded && positionGravity.y < 0)
        {
            positionGravity.y = 0;
        }
        else
        {
            positionGravity.y += gravity * Time.deltaTime;
        }

        GetComponent<CharacterController>().Move(new Vector3(0, 0, 0 ));

        if(isGrounded && Input.GetButton("Jump"))
        {
            positionGravity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        GetComponent<CharacterController>().Move(positionGravity * Time.deltaTime);
    }

    public void Die()
    {
        GameManager.Instance.GameOver();
    }

    private void ShowHideCrouchMesh()
    {
        /*if (_isCrouch)
        {
            _meshes[0].SetActive(false);
            _meshes[1].SetActive(true);
        }
        else
        {
            _meshes[0].SetActive(true);
            _meshes[1].SetActive(false);
        }*/
    }
}
