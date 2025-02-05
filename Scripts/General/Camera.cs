using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.General;

public partial class Camera : Camera3D
{
    private bool _isConnected = false;
    [Export] private Vector3 _offset;
    [Export] private Node _target;

    public override void _Ready()
    {
        base._Ready();
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        if (_isConnected) return;
        _isConnected = true;
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }

    private void HandleStartGame()
    {
        Reparent(_target);
        Position = _offset;
    }
}
