using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameObject _player;
    private HasHealth _health;
    private Animator _animator;
    float movingSpeed = 1.5f;
    
    private void Awake() {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        _health = gameObject.GetComponent<HasHealth>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update() {
        if (!_health.isAlive) return;
        
        // TODO Else if player is alive. Mode towards player!
        var pos = transform.position;
        var playerPos = _player.transform.position;
        //Vector2 position = new Vector2(playerPos.x - movingSpeed * Time.deltaTime, playerPos.y - movingSpeed * Time.deltaTime);
        Vector2 position = new Vector2(pos.x - movingSpeed * Time.deltaTime, pos.y);
        
        pos = position;
        transform.position = pos;
        _animator.SetFloat(Constants.SPEED, movingSpeed);
    }
}
