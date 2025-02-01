using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();
        var player = GetPlayerIn(CharacterNode.AttackArea);
        if (player != null)
        {
            Target = player;
            CharacterNode.CharacterSprite.Play(GameConstants.Anim.Attack);
            CharacterNode.CharacterSprite.AnimationFinished += AttackFinished;
            return;
        }

        CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
    }

    private void AttackFinished()
    {
        // wait 0.1 secs, then transition back to chase
        var timer = GetTree().CreateTimer(0.1f);
        timer.Timeout += () => CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
        if (GetPlayerIn(CharacterNode.AttackArea) != null)
        {
            CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
        }
        else if (GetPlayerIn(CharacterNode.ChaseArea) != null)
        {
            CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
        }
        else
        {
            CharacterNode.StateMachine.SwitchState<EnemyReturnState>();
        }
    }

    protected override void ExitState()
    {
        base.ExitState();
        CharacterNode.CharacterSprite.AnimationFinished -= AttackFinished;
    }
}
