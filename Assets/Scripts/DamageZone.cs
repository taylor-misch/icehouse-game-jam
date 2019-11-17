using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {
    [SerializeField] private float damageAmount;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Object that entered the trigger: " + other);
        HasHealth health = other.GetComponent<HasHealth>();
        if (health != null) {
            health.ChangeHealth(damageAmount);
        }

    }
}
