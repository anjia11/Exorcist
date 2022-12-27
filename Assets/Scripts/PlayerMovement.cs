using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private int doubleJump;
    public int doubleJumpValue;
    public bool isJump = false;
    public float moveHorizontal;
    public Rigidbody2D rb2D;

    public bool isFacingLeft = true;
    [Header("Cek Ground")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("Animasi")]
    [SerializeField] Animator animator;

    bool isAttack = false;
    bool isRunning = false;

    Enemy enemy;

    [Header("KNOCK Back")]
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KBFromRight;

    void Start() {
        doubleJump = doubleJumpValue;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        moveHorizontal = Input.GetAxis("Horizontal");


// Animasi Saat Lari
        if (moveHorizontal == 0){
            animator.SetBool("IsRun", false);
        }
        else{
            animator.SetBool("IsRun", true);
        }
// memberikan player jump
        Jump();     
        Flip();
        
    }
    
    void FixedUpdate() {
        if (KBCounter <= 0){
            rb2D.velocity = new Vector2(moveHorizontal * speed, rb2D.velocity.y);
        }else{
            KnockBack();
        }
    }

    public void Flip(){
        if (isFacingLeft && moveHorizontal > 0f || isFacingLeft && isAttack == true && transform.position.x < enemy.transform.position.x){
            transform.eulerAngles = new Vector3(0, 180, 0); //facing right
            isFacingLeft = false;
            isAttack = false;
        }
        else if (!isFacingLeft && moveHorizontal < 0f || !isFacingLeft && isAttack == true && transform.position.x > enemy.transform.position.x){
            transform.eulerAngles = new Vector3(0, 0, 0); //facing left
            isFacingLeft = true;
            isAttack = false;
        }
    }

    bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }

    void Jump(){
        if (IsGrounded()){
            doubleJump = doubleJumpValue;
            animator.SetBool("IsJumping", false);
        }
        else{
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && doubleJump > 0){
            animator.SetTrigger("JumpTakeOf");
            rb2D.velocity = Vector2.up * jumpForce;
            doubleJump--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && doubleJump > 0)
        {
            //animator.SetBool("IsJumping", true);
            rb2D.velocity = Vector2.up * jumpForce;
        }
    }

    void KnockBack(){
        if (KBFromRight == true){
            rb2D.velocity = new Vector2(-KBForce, KBForce);
        }

        if (KBFromRight == false){
            rb2D.velocity = new Vector2(KBForce, KBForce);
        }

        KBCounter -= Time.deltaTime;
    }
}
