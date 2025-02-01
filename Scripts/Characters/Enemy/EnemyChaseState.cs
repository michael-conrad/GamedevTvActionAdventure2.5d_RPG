using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer _timer;

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Target = (Player.Player)CharacterNode.ChaseArea.GetOverlappingBodies().FirstOrDefault(p => p is Player.Player);
        _timer.Timeout += HandleTimeout;
        CharacterNode.ChaseArea.BodyExited += HandleChaseAreaBodyExited;
        CharacterNode.AttackArea.BodyEntered += HandleAttackAreaBodyEntered;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        _timer.Timeout -= HandleTimeout;
        CharacterNode.ChaseArea.BodyExited -= HandleChaseAreaBodyExited;
        CharacterNode.AttackArea.BodyEntered -= HandleAttackAreaBodyEntered;
    }

    private void HandleTimeout()
    {
        Destination = Target.GlobalPosition;
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
