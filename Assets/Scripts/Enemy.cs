using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Transform _player;
    private HasHealth _health;
    private Animator _animator;
    private const float MOVING_SPEED = 1.5f;
    private const float MIN_DIST = 1.7f;
    private bool _facingRight = true;  // For determining which way the enemy is currently facing.
    private static readonly int IS_ATTACKING_TRIGGER = Animator.StringToHash("IsAttackingTrigger");
    private float _previousHorizontal;
    private bool _isAlreadyAttacking;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        _health = gameObject.GetComponent<HasHealth>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update() {
        if (!_health.isAlive) return;
        
        var playerPos = _player.position;
        Vector2 pos = transform.position;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer < MIN_DIST) {
            _animator.SetFloat(Constants.SPEED, 0);
            if (!_isAlreadyAttacking) {
                _isAlreadyAttacking = true;
                StartCoroutine(Attack());
            }
        }
        else {
            //Vector2 position = new Vector2(playerPos.x - movingSpeed * Time.deltaTime, playerPos.y - movingSpeed * Time.deltaTime);
            //Vector2 position = new Vector2(pos.x - movingSpeed * Time.deltaTime, pos.y);
            Vector2 position = new Vector2(Mathf.Lerp(pos.x, playerPos.x, Time.deltaTime), 
                Mathf.Lerp(pos.y, playerPos.y, Time.deltaTime));
            _animator.SetFloat(Constants.SPEED, MOVING_SPEED);
            transform.position = position;

            var horizontalChange = _previousHorizontal - transform.position.x;
            _previousHorizontal = transform.position.x;

            if (horizontalChange > 0 && !_facingRight) {
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (horizontalChange < 0 && _facingRight){
                Flip();
            }
        }
        
    }
    
    private void Flip() {
        // Switch the way the player is labelled as facing.
        _facingRight  = !_facingRight;

        // Multiply the player's x local scale by -1.
        var transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
    
    private IEnumerator Attack() {
        _animator.SetTrigger(IS_ATTACKING_TRIGGER);
        yield return new WaitForSeconds(.75f);
        if (_health.isAlive) {
            Debug.Log("IS now actrually attacking");

            var hits = Physics2D.RaycastAll(transform.position, _facingRight ? Vector2.left : Vector2.right, MIN_DIST);
            foreach (var v in hits) {
                //TODO use layer mask instead https://stackoverflow.com/questions/24563085/raycast-but-ignore-yourself
                if (v.transform.CompareTag(Tags.ENEMY)) continue;
                var playerHealth = v.transform.GetComponentInParent<HasHealth>();
                playerHealth.ChangeHealth(-15);
            }

            _isAlreadyAttacking = false;
        }
    }
}
