using GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public partial class StateMachine : Node
{
    [Export] private Node _currentState;
    private Player.Player _player;
    [Export] private Node[] _states;

    public override void _Ready()
    {
        if (_currentState != null)
        {
            _currentState.Notification(GameConstants.Anim.StateChanged);
        }

        _player = GetParent<Player.Player>();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (_player.direction != Vector3.Zero)
        {
            _player.stateMachineNode.SwitchState<PlayerMoveState>();
        }
        else
        {
            _player.stateMachineNode.SwitchState<PlayerIdleState>();
        }
    }

    public void SwitchState<T>()
    {
        Node newState = null;
        foreach (var state in _states)
        {
            if (state is T)
            {
                newState = state;
            }
        }

        if (newState == null)
        {
            return;
        }

        _currentState = newState;
        _currentState.Notification(GameConstants.Anim.StateChanged);
    }
}