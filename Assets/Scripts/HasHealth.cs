using UnityEngine;
using System.Collections;

public class HasHealth : MonoBehaviour {
    [SerializeField] private float hitPoints = 60f;
    [SerializeField] private bool isAlive;
    [SerializeField] private float points = 100f;
    [SerializeField] private AudioClip[] deathClip;		// The sound effect of the enemy dying.
    [SerializeField] private AudioClip[] injuredClips;
    Animator _anim;
    private float timeToDie = 7f;

    void Awake()    {
        _anim = GetComponent<Animator>();
        isAlive = true;
    }

    void ApplyDamage(float damage, bool wasHeadShot)    {
        hitPoints -= damage;
        if (hitPoints <= 0 && isAlive) {
        Die(wasHeadShot);
        }
    }

    public void RecieveDamage(float amt, bool wasHeadShot) {
        hitPoints -= amt;
        if (hitPoints <= 0 && isAlive) {
            Die(wasHeadShot);
        }
    }

    void Die(bool wasHeadShot) {
        _anim.SetTrigger("isDead");
        AudioSource.PlayClipAtPoint(deathClip[Random.Range(0, deathClip.Length)], gameObject.transform.position, .8f);
        Destroy(gameObject, timeToDie);

        //Blow up object 
        // set isAlive boolean to false
        isAlive = false;
        float pointsToIncrease = wasHeadShot?points * 2: points;
        GameManagement.manage.increaseScore(pointsToIncrease);
        GameManagement.manage.increaseKills();
    }
}