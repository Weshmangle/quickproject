using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshes;
    [SerializeField] private GameObject[] _meshesAnimation;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private AudioClip _jumpSound;    
    [SerializeField] private float stopCrouchTime, gameTime;

    private bool _isGrounded, _isCrouched;
    private float jumpHeight = 4f;
    private float gravity = -100f;
    private float index = 0;
    private CharacterController _cc;

    public Vector3 positionGravity;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore);
        
        if(_isGrounded && positionGravity.y < 0)
        {
            positionGravity.y = 0;
        }
        else
        {
            positionGravity.y += gravity * Time.deltaTime;
        }

        _cc.Move(new Vector3(0, 0, 0 ));
        _cc.Move(positionGravity * Time.deltaTime);
      
        //ShowHideCrouchMesh();
        if(_isCrouched)
        {
            var scale = transform.localScale;        
            //scale.y = .5f;
            transform.localScale = scale;

            //StartCoroutine(StopCrouch());
            /*

            Time.time c'est le temps total depuis le demarage du jeu
            si le temps de jeu est plus grand que le temps de jeu sauvegardé quand on a crouche + crouchetime (le cd) le dino se releve
            on peut spam la touche pour rester accroupi et il restera toujours accroupi "stopcrouchtime" seconde
            */
            _meshes[0].SetActive(false);
            _meshes[1].SetActive(true);
            _meshesAnimation[0].SetActive(false);
            _meshesAnimation[1].SetActive(false);
            if (Time.time > gameTime+stopCrouchTime)
            {
                _isCrouched = false;
            }
        }
        else
        {
            var scale = transform.localScale;        
            scale.y = 1f;
            transform.localScale = scale;
            //StopCoroutine(StopCrouch());
            _meshes[0].SetActive(true);
            _meshes[1].SetActive(false);
            index = (index + .05f)%_meshesAnimation.Length;

            foreach (var mesh in _meshesAnimation)
            {
                mesh.SetActive(false);
            }

            _meshesAnimation[Mathf.FloorToInt(index)].SetActive(true);
        }        
    }

    public void Jump()
    {
        if(_isGrounded)
        {
            positionGravity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            AudioManager.Instance.PlayClipAt(_jumpSound, transform.position);
        }
    }
    public void Crouch()
    {
        _isCrouched = true;   
        gameTime = Time.time;     
    }
    public void Die()
    {
        GameManager.Instance.GameOver();
    }

    private void ShowHideCrouchMesh()
    {
        if (_isCrouched)
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
    private IEnumerator StopCrouch()
    {        
        //ca marche mais ca bug
        yield return new WaitForSeconds(1);
        _isCrouched = false;        
    } 
}
