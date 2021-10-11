using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RbInput : MonoBehaviour {
    
    public float sensibilityFactor = 0.01f;
    public RbMove playerRigid;

    [HideInInspector] public RbInput current;
    private TouchControls touchControls;
    private Vector2 startPoint;


    void Awake() {
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
        if (touchControls.Touch.TouchPress.phase == InputActionPhase.Performed) {
            Vector2 currentHoldPoint = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
            Vector2 touchDelta = startPoint - currentHoldPoint;
            Vector3 fixedMove = new Vector3(touchDelta.x, 0f, 0f);
            playerRigid.rigidBody.AddForce(fixedMove * sensibilityFactor, ForceMode.Impulse);
            startPoint = currentHoldPoint;

            if (touchControls.Touch.TouchPosition.ReadValue<Vector2>().y > Settings.current.getScreenMeasures(false) * Settings.current.getMoveZoneHeight()) {
                if (playerRigid.Grounded() && !playerRigid.isJumping) {
                    playerRigid.rigidBody.AddForce(playerRigid.jumpForce * Vector3.up, ForceMode.Impulse);
                    playerRigid.isJumping = true;
                }
            }

        }
    }

    private void StartTouch(InputAction.CallbackContext ctx) {
        //Debug.Log("Touch Started" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        startPoint = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    }
    private void EndTouch(InputAction.CallbackContext ctx) {
        //Debug.Log("Touch Ended" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }
}
