using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : Node
{
    private Player _player;

    public override void _Ready()
    {
        SetPhysicsProcess(false);
        SetProcessInput(false);
        _player = FindParent("Player").GetNode<Player>(".");
        _player.stateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (_player.direction != Vector3.Zero) _player.stateMachineNode.SwitchState<PlayerMoveState>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        switch (what)
        {
            case (int)GameConstants.States.StateChanged:
                _player.sprite3D.Play(GameConstants.Anim.Idle);
                SetPhysicsProcess(true);
                SetProcessInput(true);
                break;
            case (int)GameConstants.States.PhysicsDisable:
                SetPhysicsProcess(false);
                SetProcessInput(false);
                break;
        }
    }

    // TODO: Do this better, using a proper state machine class.
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (Input.IsActionJustPressed("Dash"))
        {
            _player.stateMachineNode.SwitchState<PlayerDashState>();
        }
    }
}