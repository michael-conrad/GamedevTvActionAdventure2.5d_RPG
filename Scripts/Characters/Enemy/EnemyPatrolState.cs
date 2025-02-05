using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyPatrolState : EnemyState
{
    private bool _isConnected = false;

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float _maxIdleTime = 4;

    private int _pointIndex = 0;
    [Export] private Timer _timer;

    private int _nextPointIndex()
    {
        return ++_pointIndex % CharacterNode.PathNode.Curve.PointCount;
    }

    protected override void EnterState()
    {
        base.EnterState();
        ConnectSignals();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_nextPointIndex());
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    private void ConnectSignals()
    {
        if (_isConnected) return;
        _isConnected = true;
        _timer.Timeout += HandleTimeout;
        CharacterNode.NaviAgent.NavigationFinished += HandleNavigationFinished;
        var characterNodeChaseArea = CharacterNode.ChaseArea;
        characterNodeChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited += HandleChaseAreaBodyExited;

    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
    }

    private void DisconnectSignals()
    {
        if (!_isConnected) return;
        _isConnected = false;
        _timer.Timeout -= HandleTimeout;
        CharacterNode.NaviAgent.NavigationFinished -= HandleNavigationFinished;
        var characterNodeChaseArea = CharacterNode.ChaseArea;
        characterNodeChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNodeChaseArea.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleTimeout()
    {
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_nextPointIndex());
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    private void HandleNavigationFinished()
    {
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
        CharacterNode.Velocity = Vector3.Zero;
        var rng = new RandomNumberGenerator();
        _timer.WaitTime = rng.RandfRange(0, _maxIdleTime);
        _timer.Start();
        Destination = GetPointGlobalPosition(_nextPointIndex());
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_timer.IsStopped())
        {
            return;
        }

        base._PhysicsProcess(delta);
    }
}
