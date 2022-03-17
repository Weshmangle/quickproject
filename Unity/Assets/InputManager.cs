using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;
    public int stateSpeaker = 0;

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
            
            if(isClickedButtonSpeak(position))
            {
                Debug.Log("speak");
                stateSpeaker = (stateSpeaker + 1) % 3;
            }
            else if(isClickedButtonAbout(position))
            {
                Debug.Log("about");
            }
            else if(position.x < Screen.width / 2)
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
