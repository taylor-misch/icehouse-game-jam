using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HasHealth : MonoBehaviour {
    [SerializeField] private float maxHealth = 60f;
    private float currentHealth;
    public bool isAlive { get; set; }
    [SerializeField] private AudioClip[] deathClip; // The sound effect of the enemy dying.
    [SerializeField] private AudioClip[] injuredClips;
    Animator _anim;
    private float timeToDie = 7f;
    [SerializeField] private bool isPlayer = false;

    private void OnEnable() {
        _anim = GetComponentInChildren<Animator>();
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount) {
        if (!isAlive) return;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        
        // Debug.Log($"{currentHealth}/{maxHealth}");
        if (currentHealth <= 0 && isAlive) {
            Die();
        } else {
            AudioSource.PlayClipAtPoint(injuredClips[Random.Range(0, injuredClips.Length)], gameObject.transform.position, .8f);    
        }
    }

    private void Die() {
        isAlive = false;
        AudioSource.PlayClipAtPoint(deathClip[Random.Range(0, deathClip.Length)], gameObject.transform.position, .8f);

        if (isPlayer) {
            _anim.SetBool(Constants.IS_DEAD, true);
            GameObjective.Instance.GameOver();
        }
        else {
            // TODO Make a dead trigger for front and back separate
            _anim.SetTrigger(Constants.IS_DEAD_BACK);
            
            IEnumerator coroutine = KillEnemy();
            StartCoroutine(coroutine);

            GameManagement.Instance.IncreaseKills();
        }
    }
    
    private IEnumerator KillEnemy() {
        yield return new WaitForSeconds(timeToDie);
        gameObject.SetActive(false);
    }
}