using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnvironmentCheck : MonoBehaviour
{
    [SerializeField] Transform headLevelCheckOrigin;
    [SerializeField] Transform midLevelCheckOrigin;
    [SerializeField] Transform groundCheckOrigin1;
    [SerializeField] Transform groundCheckOrigin2;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask interactionLayerMask;

    PlayerController playerController;

    private IInteractable currentInteractable;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!playerController.IsDead)
        {
            CheckForInteractions();
            EndFallingGroundCheck();
            GroundCheck();
            RaycastHit2D headLevelCast = Physics2D.Raycast(headLevelCheckOrigin.position, new Vector2(playerController.gameObject.transform.localScale.x * 1, 0), 0.5f, layerMask);
            RaycastHit2D midLevelCast = Physics2D.Raycast(midLevelCheckOrigin.position, new Vector2(playerController.gameObject.transform.localScale.x * 1, 0), 0.5f, layerMask);

            if (midLevelCast && !headLevelCast && playerController.CanHang)
            {

                playerController.ChangeState(PlayerStateEnum.Hang);
                playerController.PlayerHangingState.SetHaningPosition(midLevelCast);


            }




        }

    }

    private void CheckForInteractions()
    {
        RaycastHit2D hit = Physics2D.Raycast(headLevelCheckOrigin.position, new Vector2(playerController.gameObject.transform.localScale.x * 1, 0), 1f, interactionLayerMask);
        if(hit) 
        {
            switch(hit.collider.tag)
            {
                case "Statue":
                    if(currentInteractable == null)
                    {
                        currentInteractable = hit.transform.gameObject.GetComponent<IInteractable>();
                        currentInteractable.OnIntercationBegin();
                    }
                    break;
                default:
                    Debug.Log("Other Tag: " + hit.collider.tag);
                    break;
            }
        }

        else
        {
            if(currentInteractable != null)
            {
                currentInteractable.OnIntercationEnd();
                currentInteractable = null;
            }
        }
    }
    private void GroundCheck()
    {
        RaycastHit2D rayCast1 = Physics2D.Raycast(groundCheckOrigin1.transform.position, Vector2.down, 0.25f, groundLayer);
        RaycastHit2D rayCast2 = Physics2D.Raycast(groundCheckOrigin2.transform.position, Vector2.down, 0.25f, groundLayer);

        if (rayCast1 || rayCast2)
        {
            playerController.IsPlayerGrounded = true;

            // Check for traps
            if (rayCast1.collider != null && rayCast1.collider.CompareTag("Trap"))
            {
                playerController.ChangeState(PlayerStateEnum.Death);
                return; 
            }

            if (rayCast2.collider != null && rayCast2.collider.CompareTag("Trap"))
            {
                playerController.ChangeState(PlayerStateEnum.Death);
                return; 
            }
        }
        else
        {
            playerController.IsPlayerGrounded = false;

            if (!playerController.IsHanging)
            {
                // Handle special cases like rolling off a ledge
                if (playerController.CurrentStateEnum == PlayerStateEnum.Roll)
                {
                    StartCoroutine(playerController.PlayerRollState.OnReachingLedgeWhileRolling(0.25f));
                }

                // Change state to falling
                playerController.ChangeState(PlayerStateEnum.Fall);
            }
        }
    }


    private void EndFallingGroundCheck()
    {
        if (playerController.rb.velocity.y < 0 && playerController.IsFalling)
        {
            RaycastHit2D rayCast1 = Physics2D.Raycast(groundCheckOrigin1.transform.position, Vector2.down, 0.25f, groundLayer);
            RaycastHit2D rayCast2 = Physics2D.Raycast(groundCheckOrigin2.transform.position, Vector2.down, 0.25f, groundLayer);
           
            if (rayCast1 || rayCast2)
            {
                playerController.PlayerFallState.OnPlayerGrounded();
               
            }
        }
    }

}
