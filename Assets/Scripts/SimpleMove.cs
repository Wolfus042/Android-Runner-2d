using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    //скрипт для передвижения персонажа по оси z


    public Animator animator;
    public Vector3 speedAndGravity; // это мы редактируем в редакторе, и затем работаем с вектором что меняется постоянно


    public float speedFix = 0.007f;

    public float jumpStrength;
    [HideInInspector] public float jumpFactor = 1;
    public float jumpDuration;
    public bool isJumping = false;

    public float currentJumpingTime;
    public AnimationCurve jumpCurve;


    [SerializeField] private CharacterController charController;
    private Transform playerTransform;

    public GameObject finalCanvas;

    private void Start() {
        Settings.current.RegisterPlayerMove(this);
        playerTransform = GetComponent<Transform>();
    }

    private void Update() {
        /*if (charController.isGrounded) {
            isJumping = false;
        }*/
        Settings.current.SetPlayerSpeed(speedAndGravity.z);
    }

    private void FixedUpdate() {    

        ResultingMove();

        if (currentJumpingTime >= jumpDuration) {
            animator.SetBool("isJumpin", false);
            isJumping = false;
        } 

        if (isJumping) {
            charController.Move(jumpCurve.Evaluate(currentJumpingTime / jumpDuration) * Vector3.up * jumpStrength * jumpFactor);
            currentJumpingTime += Time.deltaTime;
        }
        //MoveForward();
        //Debug.Log("Player X = " + playerTransform.position.x.ToString());

    }

    private void ResultingMove() {
        if (charController != null) charController.Move(speedAndGravity * speedFix);
    }

    
    public bool CanJump() {
        return charController.isGrounded;
    }

    public void MovePlayer(Vector2 delta, float sensibility) {
        if (delta != Vector2.zero) {
            Vector2 fixedMovement = new Vector2(delta.x, 0);
            fixedMovement *= sensibility;

            charController.Move(fixedMovement);
            //FixBorderCrossing();
        }
    }

    public void DestroyPlayer() {
        finalCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void JumpPlayer() {
        if (jumpStrength != 0) {
            Vector3 fixedMovement = new Vector3(0, jumpStrength, 0);
            charController.Move(fixedMovement);
        }
    }

    /* public void FixBorderCrossing() {
       bool isFixed = false;

       if (playerTransform.position.x >= Settings.current.widthUpperBorder) {

           playerTransform.position = new Vector3(Settings.current.widthUpperBorder, playerTransform.position.y, playerTransform.position.z);
           isFixed = true;
       } else if (playerTransform.position.x <= Settings.current.widthUpperBorder * -1) {
           playerTransform.position = new Vector3(Settings.current.widthUpperBorder * -1, playerTransform.position.y, playerTransform.position.z);
           isFixed = true;
       }

       if (isFixed) {
           Debug.Log("Fixed border crossing.");
       }
   } */ //не нужен, эту функцию выполняют коллайдеры



}
