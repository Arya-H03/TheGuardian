using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : MonoBehaviour, IPlayerState
{
    protected PlayerStateEnum stateEnum;
    protected PlayerController playerController;

    public PlayerBaseState()
    {

    }

    public virtual void InitState(PlayerConfig config)
    {

    }

    public virtual void Start()
    {
        InitState(playerController.PlayerConfig);
    }
    public void SetOnInitializeVariables(PlayerController statesManagerRef)
    {
        this.playerController = statesManagerRef;
    }
    public PlayerStateEnum GetStateEnum()
    {
        return stateEnum;
    }


    public virtual void OnEnterState()
    {

    }

    public virtual void OnExitState()
    {

    }

    public virtual void HandleState()
    {

    }
}
