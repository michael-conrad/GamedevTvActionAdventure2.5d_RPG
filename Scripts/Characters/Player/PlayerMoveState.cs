using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerMoveState : Node
{
    private Player _player;

    public override void _Ready()
    {
        SetPhysicsProcess(false);
        // _player = GetParent<Player>();
        _player = FindParent("Player").GetNode<Player>(".");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (_player.direction == Vector3.Zero) _player.stateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == (int)GameConstants.States.StateChanged)
        {
            _player.sprite3D.Play(GameConstants.Anim.Move);
            SetPhysicsProcess(true);
        }

        if (what == (int)GameConstants.States.PhysicsDisable)
        {
            SetPhysicsProcess(false);
        }
    }
}