using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyPatrolState : EnemyState {
    private int pointIndex;

    protected override void EnterState() {
        base.EnterState();
        pointIndex = 1;
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.NaviAgent.TargetPosition = Destination;
        CharacterNode.NaviAgent.NavigationFinished += HandleNavigationFinished;
    }

    private void HandleNavigationFinished() {
        GD.Print("Navigation finished");
        pointIndex = (++pointIndex) % CharacterNode.PathNode.Curve.PointCount;
        Destination = GetPointGlobalPosition(pointIndex);
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        Move();
    }
}