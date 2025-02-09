using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scenes.Abilities;

public partial class Bomb : Node3D
{
    [Export] private AnimatedSprite3D _animatedSprite3D;

    private bool _isConnected = false;
    [Export] private Sprite3D _sprite;

    private void ConnectSignals()
    {
        if (_isConnected) return;
        _isConnected = true;
        _animatedSprite3D.AnimationFinished += HandleAnimationFinished;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected) return;
        _isConnected = false;
        _animatedSprite3D.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        DisconnectSignals();
        QueueFree();
    }
}
