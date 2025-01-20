using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0, 20, 0.1")]
    private float _speed = 5;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (CharacterNode.direction == Vector3.Zero)
        {
            CharacterNode.stateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        CharacterNode.Velocity = CharacterNode.direction * _speed;
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (Input.IsActionJustPressed("Dash"))
        {
            CharacterNode.stateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.sprite3D.Play(GameConstants.Anim.Move);
    }
}