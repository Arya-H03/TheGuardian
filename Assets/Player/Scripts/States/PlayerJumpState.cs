using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    [SerializeField] AudioClip jumpUpAC;
    [SerializeField] float jumpSpeed;

    private float jumpDirectionX;

    public PlayerJumpState()
    {
        this.stateEnum = PlayerStateEnum.Jump;
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

        jumpDirectionX = playerController.PlayerMovementHandler.currentInputDir.x;
        playerController.rb.gravityScale = 3;
        playerController.rb.linearVelocity = new Vector2(jumpDirectionX * 3, jumpSpeed);

        AudioManager.Instance.PlaySFX( jumpUpAC,playerController.transform.position, 1);

        playerController.AnimationController.SetTriggerForAnimations("JumpUp");
      

    }

    public void SetPlayerFallStatus()
    {
        playerController.AnimationController.SetBoolForAnimations("isFalling", true);
    }
}
