using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;

    public Animator Animator { get => animator; set => animator = value; }

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>(); 
        Animator = GetComponent<Animator>();
    }
    public void SetBoolForAnimation(string name, bool value)
    {
        Animator.SetBool(name, value);
        
    }

    public void SetTriggerForAnimation(string name)
    {
        Animator.SetTrigger(name);

    }

    //public void EndTurningAnimation()
    //{

    //    enemyController.EnemyMovement.OnEnemyEndTurning();
        
    //}

    public void EndAttacking(EnemyAttackState.AttackTypeEnum attackType)
    {
        enemyController.AttackState.EndAttack(attackType);
    }


    public void CallAttack(EnemyAttackState.AttackTypeEnum attackType)
    {
        enemyController.AttackState.CallAttack(attackType);
    }

    

    public void BeginBlockingSword()
    {
        //enemyController.BlockState.BeginBlockingSword();
    }

    public void OnDeathAnimationEnd()
    {
        enemyController.DeathState.OnDeathAnimationEnd();
    }
}
