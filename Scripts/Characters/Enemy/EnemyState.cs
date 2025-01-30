using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public abstract partial class EnemyState : CharacterState
{
    [Export(PropertyHint.Range, "0, 20, 0.1")]
    private float _speed = 1.0f;

    protected Vector3 Destination;

    protected Vector3 GetPointGlobalPosition(int index)
    {
        return GetPointPosition(index) + CharacterNode.PathNode.GlobalPosition;
    }

    protected Vector3 GetPointPosition(int index)
    {
        index %= CharacterNode.PathNode.Curve.PointCount;
        return CharacterNode.PathNode.Curve.GetPointPosition(index);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Move();
    }

    protected void Move()
    {
        var nav = CharacterNode.NaviAgent;
        var nextPosition = nav.GetNextPathPosition();
        var velocity = CharacterNode.GlobalPosition.DirectionTo(nextPosition) * _speed;
        if (nav.AvoidanceEnabled)
        {
            nav.Velocity = velocity;
            CharacterNode.MoveAndSlide();
            CharacterNode.Flip();
        }
        else
        {
            _velocityComputed(velocity);
        }
    }

    protected void _velocityComputed(Vector3 safeVelocity)
    {
        CharacterNode.Velocity = safeVelocity;
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D node3D)
    {
        if (node3D is Player.Player)
        {
            CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
        }
    }
}
