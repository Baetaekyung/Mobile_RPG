using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine
{
    private readonly Dictionary<EPlayerStateEnum, PlayerState> _stateDictionary;

    private Player      _player;
    private PlayerState _currentState;

    public PlayerState GetCurrentState => _currentState;

    public PlayerStateMachine(Player player)
    {
        _player = player;
        _stateDictionary = new();
    }

    public void ChangeState(EPlayerStateEnum newState, bool isAllowDebug = false)
    {
        if (_player.GetCanStateChange == false)
            return;

        if(isAllowDebug)
            Debug.Log($"State changed from {_currentState.GetStateEnum} to {newState.ToString()}");

        _currentState?.ExitState();
        SetState(newState);
    }

    public void UpdateStateMachine()
    {
        _currentState.UpdateState();
    }

    public void FixedUpdateMachine()
    {
        _currentState.FixedUpdateState();
    }

    #region Get, Set, Add

    public void AddState(EPlayerStateEnum stateEnum, PlayerState state)
    {
        if (HasState(stateEnum) == false)
        {
            _stateDictionary.Add(stateEnum, state);
        }
    }

    public void SetState(EPlayerStateEnum newState)
    {
        _currentState = GetState(newState);
        _currentState?.EnterState();
    }

    public PlayerState GetState(EPlayerStateEnum stateEnum)
    {
        if (HasState(stateEnum) == true)
        {
            return _stateDictionary[stateEnum];
        }

        Debug.LogWarning($"존재하지 않는 상태입니다. StateName: {stateEnum.ToString()}");
        return null;
    }

    #endregion

    #region Valid Check: HasState()
    private bool HasState(EPlayerStateEnum stateEnum)
    {
        if (_stateDictionary.ContainsKey(stateEnum))
        {
            return true;
        }

        return false;
    }

    #endregion
}
