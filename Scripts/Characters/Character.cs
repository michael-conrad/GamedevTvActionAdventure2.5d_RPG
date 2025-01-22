using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public abstract partial class Character : CharacterBody3D {
    [ExportGroup("Game Settings")] //
    [Export(PropertyHint.Range, "0, 10, 0.1")]
    protected float RayDepth = 1.5f;

    [Export(PropertyHint.Range, "0, 10, 0.1")]
    protected float RayDistance = 0.4f;

    public Vector3 Direction { get; set; } = Vector3.Zero;
    public RayCast3D RayCast { get; set; }

    [ExportGroup("Required Nodes")] //
    [Export]
    public AnimatedSprite3D CharacterSprite { get; private set; }

    [Export] public StateMachine StateMachine { get; private set; }

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