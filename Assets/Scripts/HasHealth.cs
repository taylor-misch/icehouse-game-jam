using UnityEngine;

public class HasHealth : MonoBehaviour {
    [SerializeField] private float maxHealth = 60f;
    private float currentHealth;
    [SerializeField] private bool isAlive;
    [SerializeField] private AudioClip[] deathClip; // The sound effect of the enemy dying.
    [SerializeField] private AudioClip[] injuredClips;
    Animator _anim;
    private float timeToDie = 7f;
    private static readonly int IS_DEAD = Animator.StringToHash("IsDead");

    void Awake() {
        _anim = GetComponentInChildren<Animator>();
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        
        Debug.Log($"{currentHealth}/{maxHealth}");
        if (currentHealth <= 0 && isAlive) {
            Die();
        } else {
            AudioSource.PlayClipAtPoint(injuredClips[Random.Range(0, injuredClips.Length)], gameObject.transform.position, .8f);    
        }
    }

    void Die() {
        // TODO Make a dead trigger
        _anim.SetTrigger(IS_DEAD);
        AudioSource.PlayClipAtPoint(deathClip[Random.Range(0, deathClip.Length)], gameObject.transform.position, .8f);
        Destroy(gameObject, timeToDie);

        //Blow up object 
        // set isAlive boolean to false
        isAlive = false;
        GameManagement.Instance.IncreaseKills();
    }
}