using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemyPatrolState : EnemyState {
    [Export(PropertyHint.Range, "0,20,0.1")]
    private float _maxIdleTime = 4;

    private int _pointIndex;
    [Export] private Timer _timer;

    public override void _Ready() {
        base._Ready();
        CharacterNode.NaviAgent.NavigationFinished += HandleNavigationFinished;
        _timer.Timeout += HandleTimeout;
    }

    protected override void EnterState() {
        base.EnterState();
        _pointIndex = 1;
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        Destination = GetPointGlobalPosition(_pointIndex);
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    private void HandleTimeout() {
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        _pointIndex = ++_pointIndex % CharacterNode.PathNode.Curve.PointCount;
        Destination = GetPointGlobalPosition(_pointIndex);
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    private void HandleNavigationFinished() {
        GD.Print("Navigation finished");
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
        var rng = new RandomNumberGenerator();
        _timer.WaitTime = rng.RandfRange(0, _maxIdleTime);
        _timer.Start();
        _pointIndex = ++_pointIndex % CharacterNode.PathNode.Curve.PointCount;
        Destination = GetPointGlobalPosition(_pointIndex);
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (!_timer.IsStopped()) {
            return;
        }
        // Move();
    }
}