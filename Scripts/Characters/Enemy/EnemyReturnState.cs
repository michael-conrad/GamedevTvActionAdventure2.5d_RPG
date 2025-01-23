using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState {
    private Vector3 _destination;

    [Export(PropertyHint.Range, "0, 20, 0.1")]
    private float _speed = 1.0f;

    public override void _Ready() {
        base._Ready();
        _destination = CharacterNode.PathNode.Curve.GetPointPosition(0) + CharacterNode.PathNode.GlobalPosition;
        var nav = CharacterNode.NaviAgent;
        nav.VelocityComputed += _velocityComputed;
    }

    private void _velocityComputed(Vector3 safeVelocity) {
        CharacterNode.Velocity = safeVelocity;
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        var nav = CharacterNode.NaviAgent;
        if (nav.IsNavigationFinished()) {
            CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
            GD.Print("Arrived. Switching to idle state.");
            return;
        }

        // CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(_destination);
        var nextPosition = nav.GetNextPathPosition();
        var velocity = CharacterNode.GlobalPosition.DirectionTo(nextPosition) * _speed;
        if (CharacterNode.NaviAgent.AvoidanceEnabled) {
            CharacterNode.NaviAgent.Velocity = velocity;
        }
        else {
            _velocityComputed(velocity);
        }
    }

    protected override void EnterState() {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        CharacterNode.NaviAgent.TargetPosition = _destination;
    }
}