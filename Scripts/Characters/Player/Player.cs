using GamedevTvActionAdventure25d_RPG.Scripts.General;
using GamedevTvActionAdventure25d_RPG.Scripts.Reward;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class Player : Character
{
    private bool _isConnected = false;

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        GameEvents.OnReward += HandleReward;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        GameEvents.OnReward -= HandleReward;
    }

    private void HandleReward(RewardResource resource)
    {
        var stat = GetStatResource(resource.TargetStat);
        stat.StatValue += resource.TargetValue;
    }

    public override void _Ready()
    {
        base._Ready();
        RayCast = GetNode<RayCast3D>("RayCast3D");
        StateMachine.SwitchState<PlayerIdleState>();
        ConnectSignals();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        DisconnectSignals();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var input = Input.GetVector(
            GameConstants.Input.MoveLeft,
            GameConstants.Input.MoveRight,
            GameConstants.Input.MoveUp,
            GameConstants.Input.MoveDown);
        Direction = new Vector3(input.X, 0, input.Y);
        RayCast.Position = LastFacing * RayDistance;
        RayCast.TargetPosition = new Vector3(0, -RayDepth, 0); // Relative to the RayCast3D Node's position!
        Flip();
    }
}
