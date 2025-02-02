using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public partial class StateMachine : Node
{
    [Export] private Node _currentState;
    [Export] private Node[] _states;

    public override void _Ready()
    {
        if (_currentState != null)
        {
            _currentState.Notification((int)GameConstants.States.EnterState);
        }
    }

    public void SwitchState<T>()
    {
        var newState = _states.FirstOrDefault(state => state is T);
        if (newState == null)
        {
            return;
        }

        _currentState.Notification((int)GameConstants.States.ExitState);
        _currentState = newState;
        _currentState.Notification((int)GameConstants.States.EnterState);
    }
}
