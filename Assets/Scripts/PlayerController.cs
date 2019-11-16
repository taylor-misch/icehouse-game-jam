using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 4f;
    [SerializeField] private float run_speed = 6f;
    [SerializeField] private Animator animator;
    
    private new Rigidbody2D _rigidBody;

    private static readonly int SPEED = Animator.StringToHash("Speed");
    private static readonly int IS_JUMPING = Animator.StringToHash("IsJumping");
    private static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
    
    // From YouTube
    private float horizontalMove = 0f;

    private void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        #region Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        horizontalMove = Input.GetAxisRaw("Horizontal");

        Vector2 position = _rigidBody.position;
        
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        _rigidBody.MovePosition(position);
        
        animator.SetFloat(SPEED, Mathf.Abs(horizontalMove));
        #endregion

        // Jumping
        if (Input.GetButtonDown("Jump")) {
            //animator.SetBool(IS_JUMPING, true);
        }
        
        
        #region Attack
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Fire all the attacks");
            animator.SetBool(IS_ATTACKING, true);
        }
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Shield all the attacks");
        }

        if (Input.GetMouseButtonDown(2)) {
            Debug.Log("Cry all the attacks");
        }
        #endregion
        
    }
}
