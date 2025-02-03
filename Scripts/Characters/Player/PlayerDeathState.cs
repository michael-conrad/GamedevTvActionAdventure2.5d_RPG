using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Death);
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        CharacterNode.QueueFree();
    }

    protected override void ExitState()
    {
        base.ExitState();
    }
}
