using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 4f;
    [SerializeField] private float run_speed = 7f;
    [SerializeField] private Animator animator;
    
    private new Rigidbody2D _rigidBody;
    private bool _facingRight = true;  // For determining which way the player is currently facing.
    
    private static readonly int SPEED = Animator.StringToHash("Speed");
    private static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
    private static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
    private static readonly int IS_WALKING = Animator.StringToHash("IsWalking");

    private bool _isRunning;

    private void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        #region Movement
        _isRunning = Input.GetButton("Run");
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var runSpeed = _isRunning ? run_speed: speed;
        var horizontalChange = runSpeed * horizontalInput * Time.deltaTime;
        var verticalChange =   runSpeed * verticalInput * Time.deltaTime;
        
        Vector2 position = _rigidBody.position;
        position.x += horizontalChange;
        position.y += verticalChange;

        // If the input is moving the player right and the player is facing left...
        if (horizontalChange > 0 && !_facingRight) {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalChange < 0 && _facingRight){
            Flip();
        }

        var movingSpeed = Mathf.Abs(horizontalChange + verticalChange);
        Debug.LogError(movingSpeed);
        animator.SetFloat(SPEED, movingSpeed);

        if (_isRunning && movingSpeed > 0) {
            animator.SetBool(IS_RUNNING, true);
        }
        else {
            animator.SetBool(IS_RUNNING, false);
        }

        _rigidBody.MovePosition(position);
        #endregion

        #region Attacks
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Fire all the attacks");
            //animator.SetBool(IS_ATTACKING, true);
        }
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Shield all the attacks");
        }

        if (Input.GetMouseButtonDown(2)) {
            Debug.Log("Cry all the attacks");
        }
        #endregion
        
    }
    
    private void Flip() {
        // Switch the way the player is labelled as facing.
        _facingRight  = !_facingRight;

        // Multiply the player's x local scale by -1.
        var transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
}
