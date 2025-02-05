using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyAttackState : EnemyState
{
    private bool _isConnected = false;

    protected override void EnterState()
    {
        base.EnterState();
        ConnectSignals();
        var player = GetPlayerIn(CharacterNode.AttackArea);
        if (player != null)
        {
            Target = player;
            CharacterNode.CharacterSprite.Play(GameConstants.Anim.Attack);
            return;
        }

        CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
    }


    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished += HandleAnimationFinished;
        sprite.FrameChanged += HandleFrameChange;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished -= HandleAnimationFinished;
        sprite.FrameChanged -= HandleFrameChange;
        _isConnected = false;
    }

    private void HandleFrameChange()
    {
        var sprite = CharacterNode.CharacterSprite;
        if (sprite.Frame == 3 && sprite.Animation == GameConstants.Anim.Attack)
        {
            PerformHit();
        }
    }


    private void HandleAnimationFinished()
    {
        CharacterNode.EnableHitBox(false);

        // wait 0.1 secs, then transition back to chase
        var timer = GetTree().CreateTimer(AfterAttackDelay);
        timer.Timeout += () =>
        {
            if (GetPlayerIn(CharacterNode.AttackArea) != null)
            {
                CharacterNode.StateMachine.SwitchState<EnemyAttackState>();
                return;
            }

            if (GetPlayerIn(CharacterNode.ChaseArea) != null)
            {
                CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
                return;
            }

            CharacterNode.StateMachine.SwitchState<EnemyReturnState>();
        };
        CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
    }
}
