using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerDashState : PlayerState
{
    [Export] private float _speed = 10;
    [Export] private Timer _timer;

    public override void _Ready()
    {
        base._Ready();
        _timer.Timeout += HandleDashTimeOut;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    private void HandleDashTimeOut()
    {
        CharacterNode.stateMachineNode.SwitchState<PlayerIdleState>();
        CharacterNode.Velocity = Vector3.Zero;
    }

    protected override void EnterState()
    {
        CharacterNode.sprite3D.Play(GameConstants.Anim.Dash);
        SetPhysicsProcess(true);
        if (CharacterNode.direction != Vector3.Zero)
        {
            CharacterNode.Velocity = CharacterNode.direction * _speed;
        }
        else
        {
            CharacterNode.Velocity = CharacterNode.sprite3D.FlipH ? Vector3.Left * _speed : Vector3.Right * _speed;
        }

        _timer.Start();
    }
}