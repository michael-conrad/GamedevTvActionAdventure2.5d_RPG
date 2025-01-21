using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public partial class FallingCharacterBody3D : CharacterBody3D
{
    private readonly double _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity", 9.8f).AsDouble();
    [Export] public double GravityFactor { get; private set; } = 2.0;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (!IsOnFloor())
        {
            Velocity += Vector3.Down * (float)_gravity * (float)GravityFactor;
            MoveAndSlide();
        }
    }
}