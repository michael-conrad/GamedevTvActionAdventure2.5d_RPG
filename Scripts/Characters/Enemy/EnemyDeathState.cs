using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyDeathState : EnemyState
{
    private bool _isConnected = false;

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Death);
    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        CharacterNode.CharacterSprite.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        CharacterNode.QueueFree();
        CharacterNode.PathNode.QueueFree();
    }
}
