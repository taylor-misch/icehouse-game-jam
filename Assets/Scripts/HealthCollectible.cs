using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healAmount;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Object that entered the trigger: " + other);
        HasHealth health = other.GetComponent<HasHealth>();
        if (health != null) {
            health.ChangeHealth(healAmount);
        }
    }
}
