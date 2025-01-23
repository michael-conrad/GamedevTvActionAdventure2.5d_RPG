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
        if (CharacterNode.NaviAgent.IsNavigationFinished()) {
            CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
            GD.Print("Arrived. Switching to idle state.");
        }
    }

    protected override void EnterState() {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        CharacterNode.NaviAgent.TargetPosition = _destination;
    }
}