using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private GameObject _player;
    
    void Awake() {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }
    void Update() {
        Vector2 position = new Vector2(transform.position.x - 4  * Time.deltaTime, transform.position.y);
        
        //transform.position = position;
    }
}
