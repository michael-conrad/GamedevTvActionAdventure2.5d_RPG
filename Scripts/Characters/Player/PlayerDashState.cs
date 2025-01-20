using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerDashState : Node
{
    private Player _player;
    [Export] private Timer _timer;

    public override void _Ready()
    {
        SetPhysicsProcess(false);
        // _player = GetParent<Player>(); // doesn't work with nested setup - results in Class Cast exception errors
        _player = FindParent("Player").GetNode<Player>(".");
        _timer.Timeout += HandleDashTimeOut;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (_player.direction == Vector3.Zero) _player.stateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void HandleDashTimeOut()
    {
        _player.stateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == (int)GameConstants.States.StateChanged)
        {
            _player.sprite3D.Play(GameConstants.Anim.Dash);
            SetPhysicsProcess(true);
            _timer.Start();
        }

        if (what == (int)GameConstants.States.PhysicsDisable) SetPhysicsProcess(false);
    }
}