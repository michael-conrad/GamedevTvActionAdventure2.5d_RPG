using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyStunState : EnemyState
{
    private bool _isConnected;

    private void StunConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        if (CharacterNode.AttackArea.HasOverlappingBodies())
        {
            CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
        }
        else if (CharacterNode.ChaseArea.HasOverlappingBodies())
        {
            CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
        }
        else
        {
            CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
        }
    }

    protected override void ExitState()
    {
        StunDisconnectSignals();
        base.ExitState();
    }

    private void StunDisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        CharacterNode.CharacterSprite.AnimationFinished -= HandleAnimationFinished;
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Stun);
        StunConnectSignals();
    }
}
