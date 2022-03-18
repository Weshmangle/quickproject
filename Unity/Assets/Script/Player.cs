using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _meshesAnimationsDown;
    [SerializeField] private GameObject[] _meshesAnimationNormal;
    [SerializeField] private float _animationDuration;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private float stopCrouchTime, gameTime;

    public bool _isGrounded, _isCrouched;
    public static Player Instance;
    private float jumpHeight = 1.1f;
    private float gravity = -100f;
    private float stepAnimation = 0;
    private CharacterController _cc;
    private bool _playAnimation = true;

    public bool IsCrouched
    {
        get { return _isCrouched; }
        set
        {
            _isCrouched = value;
            StopAllCoroutines();
            _playAnimation = true;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance of GameManager already exist");
            return;
        }

        Instance = this;
    }

    public Vector3 positionGravity;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    private bool CalculateIsGrounded()
    {
        return Physics.CheckSphere(transform.position, .1f, groundLayer, QueryTriggerInteraction.Ignore); ;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        _isGrounded = CalculateIsGrounded();

        if (_isGrounded && positionGravity.y < 0)
        {
            positionGravity.y = 0;
        }
        else
        {
            positionGravity.y += (gravity * (_isCrouched ? 2 : 1))  * Time.deltaTime;
        }

        _cc.Move(new Vector3(0, 0, 0));
        _cc.Move(positionGravity * Time.deltaTime);

        stepAnimation = (stepAnimation + .075f) % _meshesAnimationNormal.Length;

        if (_isCrouched)
        {   
            foreach (var mesh in _meshesAnimationNormal)
            {
                mesh.SetActive(false);
            }

            foreach (var item in _meshesAnimationsDown)
            {
                item.SetActive(false);
            }

            _meshesAnimationsDown[Mathf.FloorToInt(stepAnimation)].SetActive(true);
            
            _cc.radius = .66f;
            _cc.height = 1.5f;
            _cc.center = new Vector3(.25f, _cc.height / 2, 0);
        }
        else
        {
            foreach (var mesh in _meshesAnimationNormal)
            {
                mesh.SetActive(false);
            }

            foreach (var mesh in _meshesAnimationsDown)
            {
                mesh.SetActive(false);
            }
            
            _meshesAnimationNormal[Mathf.FloorToInt(stepAnimation)].SetActive(true);
            _cc.radius = .5f;
            _cc.height = 2.65f;
            _cc.center = new Vector3(.25f, _cc.height / 2, 0);
        }
    }

    public void Jump()
    {
        if (_isGrounded )
        {
            positionGravity.y += Mathf.Sqrt(jumpHeight * -2 * gravity) * 2;
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
        StopAllCoroutines();
        GameManager.Instance.GameOver();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, .1f);
    }
}
