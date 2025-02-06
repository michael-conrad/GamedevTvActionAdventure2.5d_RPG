using GamedevTvActionAdventure25d_RPG.Scripts.General;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyReturnState : EnemyState
{
    private bool _isConnected = false;

    public override void _Ready()
    {
        base._Ready();
        Destination = GetPointGlobalPosition(0);
        var nav = CharacterNode.NaviAgent;
        nav.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        var nav = CharacterNode.NaviAgent;
        if (nav.IsNavigationFinished())
        {
            CharacterNode.StateMachine.SwitchState<EnemyPatrolState>();
            return;
        }

        base._PhysicsProcess(delta);
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        CharacterNode.NaviAgent.TargetPosition = Destination;
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;

        var nav = CharacterNode.NaviAgent;
        var characterNodeChaseArea = CharacterNode.ChaseArea;

        nav.VelocityComputed += _velocityComputed;
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
        var nav = CharacterNode.NaviAgent;
        var characterNodeChaseArea = CharacterNode.ChaseArea;

        nav.VelocityComputed -= _velocityComputed;
        characterNodeChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited -= HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        base.ExitState();
    }
}
