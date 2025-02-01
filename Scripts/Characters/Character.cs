using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public abstract partial class Character : CharacterBody3D
{
    protected internal Area3D AttackArea;

    [ExportGroup("Game Settings")] //
    [Export(PropertyHint.Range, "0, 10, 0.1")]
    protected float RayDepth = 1.5f;

    [Export(PropertyHint.Range, "0, 10, 0.1")]
    protected float RayDistance = 0.4f;

    [ExportGroup("Required Nodes")] //
    [Export]
    public AnimatedSprite3D CharacterSprite { get; private set; }

    [Export] public StateMachine StateMachine { get; private set; }

    [ExportGroup("AI Nodes")] //
    [Export]
    protected internal Area3D ChaseArea { get; private set; }

    [Export] protected internal NavigationAgent3D NaviAgent { get; private set; }

    [Export] protected internal Path3D PathNode { get; private set; }

    public Vector3 Direction { get; set; } = Vector3.Zero;
    public RayCast3D RayCast { get; set; }

    public override void _Ready()
    {
        base._Ready();
        // I really find setting these via the Godot editor asinine. So no export for you deary!
        AttackArea = GetNodeOrNull<Area3D>("AttackArea");
    }

    public void Flip()
    {
        if (Direction.X < 0 || Velocity.X < 0)
        {
            CharacterSprite.FlipH = true;
        }
        else
        {
            if (Direction.X > 0 || Velocity.X > 0)
            {
                CharacterSprite.FlipH = false;
            }
        }
    }
}
