using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyChaseState : EnemyState
{
    private bool _isConnected = false;
    [Export] private Timer _timer;

    protected override void EnterState()
    {
        base.EnterState();
        ConnectSignals();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Target = (Player.Player)CharacterNode.ChaseArea.GetOverlappingBodies().FirstOrDefault(p => p is Player.Player);
    }

    private void ConnectSignals()
    {
        if (_isConnected) return;
        _isConnected = true;
        _timer.Timeout += HandleTimeout;
        CharacterNode.ChaseArea.BodyExited += HandleChaseAreaBodyExited;
        CharacterNode.AttackArea.BodyEntered += HandleAttackAreaBodyEntered;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        _timer.Timeout -= HandleTimeout;
        CharacterNode.ChaseArea.BodyExited -= HandleChaseAreaBodyExited;
        CharacterNode.AttackArea.BodyEntered -= HandleAttackAreaBodyEntered;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
    }

    private void HandleTimeout()
    {
        if (Target == null)
        {
            CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
            return;
        }

        Destination = Target.GlobalPosition;
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }
}
