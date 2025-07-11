using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private AudioSource audioSource;
    [SerializeField] AudioClip jumpUpAC;
    [SerializeField] float jumpSpeed;

    private float jumpDirectionX;

    public PlayerJumpState()
    {
        this.stateEnum = PlayerStateEnum.Jump;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
    }
    public override void OnEnterState()
    {
        playerController.AnimationController.SetBoolForAnimations("isRunning", false);
        StartJump();

        //InputManager.Instance.InputActions.Guardian.Movement.performed -= InputManager.Instance.StartMove;
    }

    public override void OnExitState()
    {
        playerController.IsPlayerJumping = false;
        //playerController.CanPlayerJump = true;
        
       
    }

    public override void HandleState()
    {

        //if(playerController.PlayerCollisionController.Rb.velocityY < 0 && !playerController.IsHanging)
        //{
        //    playerController.ChangeState(PlayerStateEnum.Fall); 
        //}
    }
    private void StartJump()
    {
        playerController.IsPlayerJumping = true;
        playerController.CanPlayerJump = false;

        jumpDirectionX = playerController.PlayerMovementManager.currentInputDir.x;
        playerController.rb.gravityScale = 3;
        playerController.rb.linearVelocity = new Vector2(jumpDirectionX * 3, jumpSpeed);

        AudioManager.Instance.PlaySFX(audioSource, jumpUpAC, 1);

        playerController.AnimationController.SetTriggerForAnimations("JumpUp");
      

    }

    public void SetPlayerFallStatus()
    {
        playerController.AnimationController.SetBoolForAnimations("isFalling", true);
    }
}
