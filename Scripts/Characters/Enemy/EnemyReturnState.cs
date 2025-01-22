using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState {
    private Vector3 _destination;

    public override void _Ready() {
        base._Ready();
        _destination = CharacterNode.PathNode.Curve.GetPointPosition(0) + CharacterNode.PathNode.GlobalPosition;
    }

    protected override void EnterState() {
        base.EnterState();
        GD.Print(CharacterNode.Name + ", Enter State: " + GetName());
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        CharacterNode.GlobalPosition = _destination;
    }
}