using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState {
    public override void _Ready() {
        base._Ready();
        Destination = GetPointGlobalPosition(0);
        var nav = CharacterNode.NaviAgent;
        nav.VelocityComputed += _velocityComputed;
        nav.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        var nav = CharacterNode.NaviAgent;
        if (nav.IsNavigationFinished()) {
            CharacterNode.StateMachine.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }

    protected override void EnterState() {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }
}