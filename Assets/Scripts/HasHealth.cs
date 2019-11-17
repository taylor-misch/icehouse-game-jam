using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {
    [SerializeField] private float maxHealth = 60f;
    private float currentHealth;
    [SerializeField] private bool isAlive;
    [SerializeField] private AudioClip[] deathClip; // The sound effect of the enemy dying.
    [SerializeField] private AudioClip[] injuredClips;
    Animator _anim;
    private float timeToDie = 7f;

    void Awake() {
        _anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        Debug.Log($"{currentHealth}/{maxHealth}");
        if (currentHealth <= 0 && isAlive) {
            Die();
        }
    }

    void Die() {
        _anim.SetTrigger("isDead");
        AudioSource.PlayClipAtPoint(deathClip[Random.Range(0, deathClip.Length)], gameObject.transform.position, .8f);
        Destroy(gameObject, timeToDie);

        //Blow up object 
        // set isAlive boolean to false
        isAlive = false;
        GameManagement.manage.increaseKills();
    }
}