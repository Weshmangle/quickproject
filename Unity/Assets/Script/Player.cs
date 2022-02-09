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

    private bool _isGrounded;
    private bool _isCrouched;
    private float jumpHeight = 1f;
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
            StartCoroutine(Animation());
        }
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

    public void OnJump()
    {
        Debug.Log("OnJump");
        if (GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.GameStart();
        }
        Jump();
    }

    public void OnCrouch()
    {
        Crouch();
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
            positionGravity.y += gravity * Time.deltaTime;
        }

        _cc.Move(new Vector3(0, 0, 0));
        _cc.Move(positionGravity * Time.deltaTime);

        if (_isCrouched)
        {
            var scale = transform.localScale;
            transform.localScale = scale;
            _meshesAnimationsDown[0].SetActive(false);
            _meshesAnimationsDown[1].SetActive(true);

            foreach (var item in _meshesAnimationNormal)
            {
                item.SetActive(false);
            }

            if (Time.time > gameTime + stopCrouchTime)
            {
                _isCrouched = false;
            }
        }
        else
        {
            var scale = transform.localScale;
            scale.y = 1f;
            transform.localScale = scale;
            //stepAnimation = (stepAnimation + .05f) % _meshesAnimationNormal.Length;

            //foreach (var mesh in _meshesAnimationNormal)
            //{
            //    mesh.SetActive(false);
            //}

            //_meshesAnimationNormal[Mathf.FloorToInt(stepAnimation)].SetActive(true);
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        while (_playAnimation)
        {
            if (_isCrouched)
            {
                foreach (var item in _meshesAnimationNormal)
                {
                    item.SetActive(false);
                }

                _meshesAnimationsDown[0].SetActive(true);
                _meshesAnimationsDown[1].SetActive(false);
                yield return new WaitForSeconds(_animationDuration);
                _meshesAnimationsDown[0].SetActive(false);
                _meshesAnimationsDown[1].SetActive(true);
                yield return new WaitForSeconds(_animationDuration);
            }
            else
            {
                foreach (var item in _meshesAnimationsDown)
                {
                    item.SetActive(false);
                }

                _meshesAnimationNormal[0].SetActive(true);
                _meshesAnimationNormal[1].SetActive(false);
                yield return new WaitForSeconds(_animationDuration);
                _meshesAnimationNormal[0].SetActive(false);
                _meshesAnimationNormal[1].SetActive(true);
                yield return new WaitForSeconds(_animationDuration);
            }
        }

    }


    public void Jump()
    {
        if (_isGrounded && _cc.isGrounded)
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
