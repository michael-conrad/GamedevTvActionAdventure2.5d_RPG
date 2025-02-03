using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Death);
    }

    private void HandleAnimationFinished()
    {
        CharacterNode.QueueFree();
    }
}
