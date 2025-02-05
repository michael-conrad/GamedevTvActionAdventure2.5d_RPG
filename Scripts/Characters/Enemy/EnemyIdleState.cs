using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyIdleState : EnemyState
{
    private bool _isConnected = false;

    protected override void EnterState()
    {
        base.EnterState();
        ConnectSignals();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
        CharacterNode.NaviAgent.TargetPosition = GetPointGlobalPosition(0); //
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        var characterNodeChaseArea = CharacterNode.ChaseArea;
        characterNodeChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited += HandleChaseAreaBodyExited;

    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        var characterNodeChaseArea = CharacterNode.ChaseArea;
        characterNodeChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited -= HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var nav = CharacterNode.NaviAgent;
        if (IsNavigationReady() && !nav.IsNavigationFinished())
        {
            CharacterNode.StateMachine.SwitchState<EnemyReturnState>();
            return;
        }

        Move();
    }
}
