using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerDashState : PlayerState {
    [Export(PropertyHint.Range, "0,20,0.1")]
    private float _speed = 10;

    [Export] private Timer _timer;

    public override void _Ready() {
        base._Ready();
        _timer.Timeout += HandleDashTimeOut;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (IsFacingEdge()) {
            CharacterNode.Velocity = Vector3.Zero;
        }
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    private void HandleDashTimeOut() {
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
        CharacterNode.Velocity = Vector3.Zero;
    }

    protected override void EnterState() {
        if (!CharacterNode.IsOnFloor()) {
            HandleDashTimeOut();
            return;
        }

        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Dash);
        SetPhysicsProcess(true);
        if (CharacterNode.Direction != Vector3.Zero) {
            CharacterNode.Velocity = CharacterNode.Direction * _speed;
        }
        else {
            CharacterNode.Velocity =
                CharacterNode.CharacterSprite.FlipH ? Vector3.Left * _speed : Vector3.Right * _speed;
        }

        _timer.Start();
    }
}