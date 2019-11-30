using UnityEngine;

public class Constants {
    // Animator constants for hashes
    public static readonly int SPEED = Animator.StringToHash("Speed");
    public static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
    public static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
    public static readonly int IS_WALKING = Animator.StringToHash("IsWalking");
    public static readonly int IS_ATTACKING_TRIGGER = Animator.StringToHash("IsAttackingTrigger");
    public static readonly int IS_DEAD_FRONT = Animator.StringToHash("IsDeadFront");
    public static readonly int IS_DEAD_BACK = Animator.StringToHash("IsDeadBack");
    public static readonly int IS_DEAD = Animator.StringToHash("IsDead");
    
    public static readonly int MAIN_MENU = 0;
}
