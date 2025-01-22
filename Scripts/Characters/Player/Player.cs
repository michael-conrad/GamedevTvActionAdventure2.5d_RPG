using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class Player : CharacterBody3D {
    [Export(PropertyHint.Range, "0, 10, 0.1")]
    private float _rayDepth = 1.5f;

    [ExportGroup("Game Settings")] //
    [Export(PropertyHint.Range, "0, 10, 0.1")]
    private float _rayDistance = 0.6f;

    public Vector3 Direction { get; private set; } = Vector3.Zero;
    public RayCast3D RayCast { get; private set; }

    [ExportGroup("Required Nodes")] //
    [Export]
    public AnimatedSprite3D CharacterSprite { get; private set; }

    [Export] public StateMachine StateMachine { get; private set; }

    public override void _Ready() {
        base._Ready();
        RayCast = GetNode<RayCast3D>("RayCast3D");
    }

    public override void _Input(InputEvent @event) {
        var input = Input.GetVector(
            GameConstants.Input.MoveLeft,
            GameConstants.Input.MoveRight,
            GameConstants.Input.MoveUp,
            GameConstants.Input.MoveDown);
        Direction = new Vector3(input.X, 0, input.Y);
        RayCast.Position = Direction * _rayDistance;
        RayCast.TargetPosition = new Vector3(0, -_rayDepth, 0); // Relative to the RayCast3D Node's position!
        Flip();
    }

    public void Flip() {
        if (Direction.X < 0) {
            CharacterSprite.FlipH = true;
        }
        else {
            if (Direction.X > 0) {
                CharacterSprite.FlipH = false;
            }
        }
    }
}