using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

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
            var sprite = CharacterNode.CharacterSprite;
            sprite.Play(GameConstants.Anim.Attack);
            sprite.AnimationFinished += HandleAnimationFinished;
            sprite.FrameChanged += HandleFrameChange;
            return;
        }

        CharacterNode.StateMachine.SwitchState<EnemyIdleState>();
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
            }
            else if (GetPlayerIn(CharacterNode.ChaseArea) != null)
            {
                CharacterNode.StateMachine.SwitchState<EnemyChaseState>();
            }
            else
            {
                CharacterNode.StateMachine.SwitchState<EnemyReturnState>();
            }
        };
    }

    protected override void ExitState()
    {
        base.ExitState();
        var animationFinished = AnimatedSprite3D.SignalName.AnimationFinished;
        var callableAnimationFinished = Callable.From(HandleAnimationFinished);
        if (CharacterNode.CharacterSprite.IsConnected(animationFinished, callableAnimationFinished))
        {
            CharacterNode.CharacterSprite.AnimationFinished -= HandleAnimationFinished;
        }

        var frameChanged = AnimatedSprite3D.SignalName.FrameChanged;
        var callableFrameChanged = Callable.From(HandleFrameChange);
        if (CharacterNode.CharacterSprite.IsConnected(frameChanged, callableFrameChanged))
        {
            CharacterNode.CharacterSprite.AnimationFinished -= HandleFrameChange;
        }
    }
}
