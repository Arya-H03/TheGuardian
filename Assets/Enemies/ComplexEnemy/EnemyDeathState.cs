using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    [SerializeField] Sprite deadSprite;
    [SerializeField] GameObject atonement;

    public delegate void EventHandler();
    public EventHandler EnemyBeginDeathEvent;
    public EventHandler EnemyEndDeathEvent;
    public EnemyDeathState() : base()
    {
        stateEnum = EnemyStateEnum.Death;

    }

    private void Start()
    {
        EnemyBeginDeathEvent += OnEnemyDeathBegin;
    }


    public override void OnEnterState()
    {
        EnemyBeginDeathEvent.Invoke();
       

    }

    public override void OnExitState()
    {

    }

    public override void HandleState()
    {
        
    }

    private void OnEnemyDeathBegin()
    {
        enemyController.IsDead = true;
        enemyController.collisionManager.Rb.bodyType = RigidbodyType2D.Static;
        enemyController.collisionManager.BoxCollider.enabled = false;
        enemyController.EnemyAnimationManager.SetTriggerForAnimation("Death");
        enemyController.WorldCanvas.gameObject.SetActive(false);
        Instantiate(atonement, transform.position, Quaternion.identity);

    }

    private void OnEnemyDeathEnd()
    {
       
    }

    public void OnDeathAnimationEnd()
    {
        enemyController.EnemyAnimationManager.Animator.enabled = false;
        enemyController.SpriteRenderer.sprite = deadSprite;
        if (EnemyEndDeathEvent != null)
        {
            EnemyEndDeathEvent.Invoke();
        }
      
        Destroy(transform.parent.parent.gameObject,0.25f);
        



    }

}
