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

    public override void _Ready()
    {
        base._Ready();
        CharacterNode.NaviAgent.NavigationFinished += HandleNavigationFinished;
    }

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_nextPointIndex());
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    private void HandleTimeout()
    {
        GD.Print("Timeout " + CharacterNode.Name);
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_nextPointIndex());
        CharacterNode.NaviAgent.TargetPosition = Destination;
        _timer.Timeout -= HandleTimeout;
    }

    private void HandleNavigationFinished()
    {
        GD.Print("Navigation finished" + CharacterNode.Name);
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
        CharacterNode.Velocity = Vector3.Zero;
        var rng = new RandomNumberGenerator();
        _timer.WaitTime = rng.RandfRange(0, _maxIdleTime);
        _timer.Timeout += HandleTimeout;
        _timer.Start();
        Destination = GetPointGlobalPosition(_nextPointIndex());
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (!_timer.IsStopped())
        {
            return;
        }
        // Move();
    }
}
