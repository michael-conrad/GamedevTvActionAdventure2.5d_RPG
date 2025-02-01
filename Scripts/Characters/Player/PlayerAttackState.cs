using GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;
using GamedevTvActionAdventure25d_RPG.Scripts.General;

public partial class PlayerAttackState : PlayerState
{
    private int _comboCount = 0;
    private int _maxComboCount = 2;

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Attack + (_comboCount + 1));
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        _comboCount = ++_comboCount % _maxComboCount;
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        CharacterNode.CharacterSprite.AnimationFinished -= HandleAnimationFinished;
    }
}
