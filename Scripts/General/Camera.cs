using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

public partial class Camera : Camera3D
{
    [Export] private Vector3 _offset;
    [Export] private Node _target;

    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnStartGame += HandleStartGame;
    }

    private void HandleStartGame()
    {
        Reparent(_target);
        Position = _offset;
    }
}
