using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState {
    private Vector3 _destination;

    public override void _Ready() {
        base._Ready();
        _destination = CharacterNode.PathNode.Curve.GetPointPosition(0) + CharacterNode.PathNode.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);

        if (CharacterNode.GlobalPosition == _destination) {
            return;
        }

        CharacterNode.Velocity = CharacterNode.GlobalPosition.DirectionTo(_destination);
        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    protected override void EnterState() {
        base.EnterState();
        GD.Print(CharacterNode.Name + ", Enter State: " + GetName());
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
    }
}