using GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;
using GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

public partial class EnemyChaseState : EnemyState
{
    private CharacterBody3D _target;

    [Export] private Timer _timer;

    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Move);
        foreach (var body in CharacterNode.ChaseArea.GetOverlappingBodies())
        {
            if (body is not Player player)
            {
                continue;
            }

            _target = player;
            break;
        }

        _timer.Timeout += HandleTimeout;
        CharacterNode.ChaseArea.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        base.ExitState();
        _timer.Timeout -= HandleTimeout;
        CharacterNode.ChaseArea.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleTimeout()
    {
        Destination = _target.GlobalPosition;
        CharacterNode.NaviAgent.TargetPosition = Destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
