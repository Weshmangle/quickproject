using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshesAnimationsDown;
    [SerializeField] private GameObject[] _meshesAnimationNormal;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private float stopCrouchTime, gameTime;

    public bool _isGrounded, _isCrouched;
    private float jumpHeight = 1f;
    private float gravity = -100f;
    private float stepAnimation = 0;
    private CharacterController _cc;

    public Vector3 positionGravity;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(GameManager.Instance.IsGameOver) return;

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
      
        if(_isCrouched)
        {
            var scale = transform.localScale;
            transform.localScale = scale;
            _meshesAnimationsDown[0].SetActive(false);
            _meshesAnimationsDown[1].SetActive(true);
            
            foreach (var item in _meshesAnimationNormal)
            {
                item.SetActive(false);
            }
            
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
            stepAnimation = (stepAnimation + .05f)%_meshesAnimationNormal.Length;

            foreach (var mesh in _meshesAnimationNormal)
            {
                mesh.SetActive(false);
            }

            _meshesAnimationNormal[Mathf.FloorToInt(stepAnimation)].SetActive(true);
        }

        //if(Input.GetButton("Jump"))
        {
            Jump();
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
            _meshesAnimationsDown[0].SetActive(false);
            _meshesAnimationsDown[1].SetActive(true);
        }
        else
        {
            _meshesAnimationsDown[0].SetActive(true);
            _meshesAnimationsDown[1].SetActive(false);
        }
    }
    private IEnumerator StopCrouch()
    {        
        //ca marche mais ca bug
        yield return new WaitForSeconds(1);
        _isCrouched = false;
    } 
}
