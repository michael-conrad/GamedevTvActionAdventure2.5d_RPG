using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Death);
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
    }

    protected virtual void HandleAnimationFinished()
    {
        CharacterNode.QueueFree();
    }

    protected override void ExitState()
    {
        base.ExitState();
    }
}
