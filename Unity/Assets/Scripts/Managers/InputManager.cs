using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;
    public int stateSpeaker = 0;
    public string touchState = "";

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
        var position = touchControls.Touch.TouchPosition.ReadValue<Vector2>();

        if(isClickedButtonSpeak(position))
        {
            GameManager.Instance.buttonSpeaker.GetComponent<ButtonSpeaker>().Click();
        }
        else if(isClickedButtonAbout(position))
        {
            GameManager.Instance.buttonAbout.GetComponent<ButtonAbout>().Click();
        }
        else if(GameManager.Instance.buttonAbout.GetComponent<ButtonAbout>().popup.IsActive())
        {
            GameManager.Instance.buttonAbout.GetComponent<ButtonAbout>().Click();
        }
        else if(GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.GameStart();
        }
        else
        {
            if(position.x < Screen.width / 2)
            {
                Player.Instance.Jump();
                touchState = "OnPressJump";
            }
            else
            {
                Player.Instance.Crouch();
                touchState = "OnPressCrouch";
            }
        }
    }
    
    private void EndTouch(InputAction.CallbackContext context)
    {
        var position = touchControls.Touch.TouchPosition.ReadValue<Vector2>();

        Player.Instance.IsCrouched = false;
        
        if(position.x < Screen.width / 2)
        {
            Player.Instance.Jump();
            touchState = "OnReleaseJump";
        }
    }

    private void Update()
    {
        if(touchState == "OnPressJump")
        {
            Player.Instance.Jump();
        }
    
        if(touchState == "OnReleaseJump")
        {
            Player.Instance.Jump();
            touchState = "";
        }
    }

    private bool isClickedButton(Button button, Vector2 position)
    {
        var rect = (button.transform as RectTransform).rect;
        var pos = button.transform.position;

        return position.x > pos.x + rect.x &&
               position.x < pos.x + rect.width &&
               position.y > pos.y + rect.y &&
               position.y < pos.y + rect.height;
    }

    private bool isClickedButtonSpeak(Vector2 position)
    {
        return isClickedButton(GameManager.Instance.buttonSpeaker, position);
    }

    private bool isClickedButtonAbout(Vector2 position)
    {
        return isClickedButton(GameManager.Instance.buttonAbout, position);
    }
}
