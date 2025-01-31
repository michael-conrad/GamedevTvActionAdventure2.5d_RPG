using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();
        Destination = GetPointGlobalPosition(0);
        var nav = CharacterNode.NaviAgent;
        nav.VelocityComputed += _velocityComputed;
        nav.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        var nav = CharacterNode.NaviAgent;
        if (nav.IsNavigationFinished())
        {
            GD.Print("Returning to patrol: " + CharacterNode.Name);
            CharacterNode.StateMachine.SwitchState<EnemyPatrolState>();
            return;
        }

        base._PhysicsProcess(delta);
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        CharacterNode.NaviAgent.TargetPosition = Destination;
        var characterNodeChaseArea = CharacterNode.ChaseArea;
        characterNodeChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        base.ExitState();
        var characterNodeChaseArea = CharacterNode.ChaseArea;
        characterNodeChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited -= HandleChaseAreaBodyExited;
    }
}
