using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Levels;

public partial class Main : Node3D
{
    public override void _Ready()
    {
        base._Ready();
        GetTree().Paused = true;
    }
}
