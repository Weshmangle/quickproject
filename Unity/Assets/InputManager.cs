using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;

    private void Awake() {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }
    private void Start()
    {
         touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
         touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.GameStart();
        }
        else
        {
            var position = touchControls.Touch.TouchPosition.ReadValue<Vector2>();

            //GameManager.Instance.buttonAbout.transform.
            
            if(position.x < Screen.width / 2)
            {
                Player.Instance.Jump();
            }
            else
            {
                Player.Instance.Crouch();
            }
        }
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        if(Player.Instance.IsCrouched)
        {
            Player.Instance.IsCrouched = false; 
        }
    }
}
