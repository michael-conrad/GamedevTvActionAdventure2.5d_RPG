using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public abstract partial class EnemyState : CharacterState {
    [Export(PropertyHint.Range, "0, 20, 0.1")]
    private float _speed = 1.0f;

    protected Vector3 Destination;

    protected Vector3 GetPointGlobalPosition(int index) {
        return CharacterNode.PathNode.Curve.GetPointPosition(index) + CharacterNode.PathNode.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        Move();
    }

    protected void Move() {
        var nav = CharacterNode.NaviAgent;
        var nextPosition = nav.GetNextPathPosition();
        var velocity = CharacterNode.GlobalPosition.DirectionTo(nextPosition) * _speed;
        if (nav.AvoidanceEnabled) {
            nav.Velocity = velocity;
        }
        else {
            _velocityComputed(velocity);
        }

        CharacterNode.Flip();
    }

    protected void _velocityComputed(Vector3 safeVelocity) {
        CharacterNode.Velocity = safeVelocity;
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }
}