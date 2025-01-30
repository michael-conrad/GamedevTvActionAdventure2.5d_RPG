using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyPatrolState : EnemyState
{
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
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_nextPointIndex());
        CharacterNode.NaviAgent.TargetPosition = Destination;
        _timer.Timeout += HandleTimeout;
        CharacterNode.NaviAgent.NavigationFinished += HandleNavigationFinished;
        CharacterNode.ChaseArea.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();
        _timer.Timeout -= HandleTimeout;
        CharacterNode.NaviAgent.NavigationFinished -= HandleNavigationFinished;
        CharacterNode.ChaseArea.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    private void HandleTimeout()
    {
        GD.Print("Timeout " + CharacterNode.Name);
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_nextPointIndex());
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    private void HandleNavigationFinished()
    {
        GD.Print("Navigation finished" + CharacterNode.Name);
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
