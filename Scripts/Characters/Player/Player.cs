using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class Player : CharacterBody3D
{
    public Vector3 Direction { get; private set; } = Vector3.Zero;

    [ExportGroup("Required Nodes")] //
    [Export]
    public AnimatedSprite3D CharacterSprite { get; private set; }

    [Export] public StateMachine StateMachine { get; private set; }

    public override void _Ready()
    {
        // ignore
    }

    public override void _Input(InputEvent @event)
    {
        var input = Input.GetVector(
            GameConstants.Input.MoveLeft,
            GameConstants.Input.MoveRight,
            GameConstants.Input.MoveUp,
            GameConstants.Input.MoveDown);
        Direction = new Vector3(input.X, 0, input.Y);
    }

    public void Flip()
    {
        if (Direction.X < 0)
        {
            CharacterSprite.FlipH = true;
        }
        else
        {
            if (Direction.X > 0) CharacterSprite.FlipH = false;
        }
    }
}