using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public delegate void MyEventHandler();

    public static event MyEventHandler PlayerDeathEvent;
    public static event MyEventHandler PlayerRespawnEvent;
    public PlayerDeathState()
    {
        this.stateEnum = PlayerStateEnum.Death;
    }

    private void OnEnable()
    {
        PlayerDeathEvent += StartPlayerDeathCoroutine;
        PlayerRespawnEvent += PlayerRespawn;
    }

    private void OnDisable()
    {
        PlayerDeathEvent -= StartPlayerDeathCoroutine;
        PlayerRespawnEvent -= PlayerRespawn;
    }
    public override void OnEnterState()
    {
        
        OnPlayerDeath();
    }

    public override void OnExitState()
    {
        
    }

    public override void HandleState()
    {


    }

    private void StartPlayerDeathCoroutine()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    private IEnumerator PlayerDeathCoroutine()
    {
        InputManager.Instance.OnDisable();
        playerController.IsDead = true;    
        playerController.AnimationController.SetBoolForAnimations("isDead", true);
        playerController.AnimationController.SetTriggerForAnimations("Death");
        GameManager.Instance.DistortCamera();
        //MobManager.Instance.ResetPlayerForAllEnemies();

        yield return new WaitForSeconds(1f);

        UIManager.Instance.OpenDeathMenu();

    }

    public void OnPlayerDeath()
    {
        if (!playerController.IsDead)
        {
            PlayerDeathEvent?.Invoke();
        }

    }

    private void PlayerRespawn()
    {
        GameManager.Instance.EndPlayerDistortion();
        playerController.AnimationController.SetBoolForAnimations("isDead", false);
        InputManager.Instance.OnEnable();
        playerController.IsDead = false;
        playerController.RestoreHealth(playerController.MaxHealth);
        playerController.ResetPlayerVariables();
        playerController.ChangeState(PlayerStateEnum.Idle); 

    }

   

    public void OnPlayerRespawn()
    {
        if (playerController.IsDead)
        {
            PlayerRespawnEvent?.Invoke();
        }
       
    }
}
