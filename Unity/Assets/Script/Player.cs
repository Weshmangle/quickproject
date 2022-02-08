using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshes;
    [SerializeField] private GameObject[] _meshesAnimation;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [SerializeField] private bool isGrounded, isCrouched;
    [SerializeField] private float stopCrouchTime, gameTime;

    private float jumpHeight = 4f;
    private float gravity = -100f;
    public Vector3 positionGravity;
    private float index = 0;

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

        //Jump();
        //Crouch();
        GetComponent<CharacterController>().Move(positionGravity * Time.deltaTime);

        index = (index + .05f)%_meshesAnimation.Length;

        foreach (var mesh in _meshesAnimation)
        {
            mesh.SetActive(false);
        }

        _meshesAnimation[Mathf.FloorToInt(index)].SetActive(true);
        
        if(isCrouched)
        {
            var scale = transform.localScale;        
            scale.y = .5f;
            transform.localScale = scale;
            //StartCoroutine(StopCrouch());
            /*

            Time.time c'est le temps total depuis le demarage du jeu
            si le temps de jeu est plus grand que le temps de jeu sauvegardé quand on a crouche + crouchetime (le cd) le dino se releve
            on peut spam la touche pour rester accroupi et il restera toujours accroupi "stopcrouchtime" seconde
            */
            if (Time.time > gameTime+stopCrouchTime)
            {
                isCrouched = false;
            }
        }
        else
        {
            var scale = transform.localScale;        
            scale.y = 1f;
            transform.localScale = scale;
            //StopCoroutine(StopCrouch());
        }
        
        //var position = transform.position;
        //position.x = -Screen.width / 40;
        //transform.position = position;
    }

    public void Jump()
    {
        if(isGrounded)
        {
            positionGravity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    public void Crouch()
    {
        isCrouched = true;   
        gameTime = Time.time;     
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
    private IEnumerator StopCrouch()
    {        
        //ca marche mais ca bug
        yield return new WaitForSeconds(1);
        isCrouched = false;        
    } 
}
