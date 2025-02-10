using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Abilities;

public abstract partial class Ability : Node3D
{
    [Export] protected AnimatedSprite3D AnimatedSprite;

    [Export(PropertyHint.Range, "1, 100, 1")]
    public float Damage { get; private set; } = 10;
}
