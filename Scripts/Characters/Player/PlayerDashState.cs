using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerDashState : PlayerState
{
    [Export] private PackedScene _bombScene;
    private bool _isConnected = false;

    [Export(PropertyHint.Range, "0,20,0.1")]
    private float _speed = 10;

    [Export] private Timer _timer;

    public override void _Ready()
    {
        base._Ready();
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        _timer.Timeout += HandleDashTimeOut;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        _timer.Timeout -= HandleDashTimeOut;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (IsFacingEdge())
        {
            CharacterNode.Velocity = Vector3.Zero;
        }

        CharacterNode.MoveAndSlide();
        CharacterNode.Flip();
    }

    private void HandleDashTimeOut()
    {
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
        CharacterNode.Velocity = Vector3.Zero;
    }

    protected override void EnterState()
    {
        ConnectSignals();

        if (!CharacterNode.IsOnFloor())
        {
            HandleDashTimeOut();
            return;
        }

        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Dash);
        SetPhysicsProcess(true);
        if (CharacterNode.Direction != Vector3.Zero)
        {
            CharacterNode.Velocity = CharacterNode.Direction * _speed;
        }
        else
        {
            CharacterNode.Velocity =
                CharacterNode.CharacterSprite.FlipH ? Vector3.Left * _speed : Vector3.Right * _speed;
        }

        _timer.Start();
        if (_bombScene != null)
        {
            var bomb = _bombScene.Instantiate<Node3D>();
            GetTree().CurrentScene.AddChild(bomb);
            bomb.GlobalPosition = CharacterNode.GlobalPosition;
        }
    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
    }
}
