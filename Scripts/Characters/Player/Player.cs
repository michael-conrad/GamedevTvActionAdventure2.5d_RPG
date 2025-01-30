using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        RayCast = GetNode<RayCast3D>("RayCast3D");
        StateMachine.SwitchState<PlayerIdleState>();
    }

    public override void _Input(InputEvent @event)
    {
        var input = Input.GetVector(
            GameConstants.Input.MoveLeft,
            GameConstants.Input.MoveRight,
            GameConstants.Input.MoveUp,
            GameConstants.Input.MoveDown);
        Direction = new Vector3(input.X, 0, input.Y);
        RayCast.Position = Direction * RayDistance;
        RayCast.TargetPosition = new Vector3(0, -RayDepth, 0); // Relative to the RayCast3D Node's position!
        Flip();
    }
}
