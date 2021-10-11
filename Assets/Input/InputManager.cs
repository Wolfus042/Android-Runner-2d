using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [HideInInspector] public static InputManager current;

    public SimpleMove player;
    public float sensibilityFactor = 0.01f;
    public float jumpStrength;

    [HideInInspector] public bool ifOnFinishZone = false; 

    private Vector2 touchDelta = Vector2.zero;
    private Vector2 startPoint;
    public TouchControls touchControls;
    

    //ширина — вторая компонента вектора

    private void Awake() {
        current = this;
        touchControls = new TouchControls();
    }

    private void OnEnable() {
        touchControls.Enable();
    }

    private void OnDisable() {
        touchControls.Disable();
    }

    private void Start() {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);


    }

    private void Update() {
        if (!ifOnFinishZone) {
            if (touchControls.Touch.TouchPress.phase == InputActionPhase.Performed) {
                //Debug.Log("touching...");
                Vector2 currentHoldPoint = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
                touchDelta = startPoint - currentHoldPoint;
                player.MovePlayer(touchDelta * -1, sensibilityFactor);
                startPoint = currentHoldPoint;

                if (touchControls.Touch.TouchPosition.ReadValue<Vector2>().y > Settings.current.getScreenMeasures(false) * Settings.current.getMoveZoneHeight()) {
                    if (player.CanJump() && !player.isJumping) {
                        player.isJumping = true;
                        player.animator.SetBool("isJumpin", true);
                        player.currentJumpingTime = 0;
                        //player.JumpPlayer();
                        
                    }

                }
            }
        }

        
    }

    private void StartTouch(InputAction.CallbackContext ctx) {
        //Debug.Log("Touch Started" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        startPoint = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        if (ifOnFinishZone) {
            Settings.current.finArea.barValue += Settings.current.finArea.clickForce;
        }
    }
    private void EndTouch(InputAction.CallbackContext ctx) {
        //Debug.Log("Touch Ended" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }
}
