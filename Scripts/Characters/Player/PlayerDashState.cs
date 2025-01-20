using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerDashState : Node
{
    private Player _player;
    [Export] private float _speed = 10;
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
        _player.MoveAndSlide();
        _player.Flip();
    }

    private void HandleDashTimeOut()
    {
        _player.stateMachineNode.SwitchState<PlayerIdleState>();
        _player.Velocity = Vector3.Zero;
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == (int)GameConstants.States.StateChanged)
        {
            _player.sprite3D.Play(GameConstants.Anim.Dash);
            SetPhysicsProcess(true);
            if (_player.direction != Vector3.Zero)
            {
                _player.Velocity = _player.direction * _speed;
            }
            else
            {
                _player.Velocity = _player.sprite3D.FlipH ? Vector3.Left * _speed : Vector3.Right * _speed;
            }

            _timer.Start();
        }

        if (what == (int)GameConstants.States.PhysicsDisable) SetPhysicsProcess(false);
    }
}