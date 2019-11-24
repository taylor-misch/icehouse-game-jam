using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameObject _player;
    private HasHealth _health;
    private Animator _animator;
    float movingSpeed = 3;
    
    void Awake() {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _health = gameObject.GetComponent<HasHealth>();
        _animator = GetComponentInChildren<Animator>();
    }
    void Update() {
        if (!_health.isAlive) return;
        
        // TODO Else if player is alive. Mode towards player!
        Vector2 position = new Vector2(transform.position.x - movingSpeed* Time.deltaTime, transform.position.y);
        transform.position = position;
        _animator.SetFloat(Constants.SPEED, movingSpeed);
    }
}
