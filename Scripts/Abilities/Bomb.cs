using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Abilities;

public partial class Bomb : Ability
{
    private AnimationPlayer _animationPlayer;

    [Export] private CollisionShape3D _collisionShape3D;

    private bool _isConnected = false;
    [Export] private Sprite3D _sprite;

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        AnimatedSprite.AnimationFinished += HandleAnimationFinished;
        AnimatedSprite.FrameChanged += HandleFrameChanged;
    }

    public override void _Ready()
    {
        base._Ready();
        ConnectSignals();
        _sprite.Visible = true;
        AnimatedSprite.Visible = false;
        _collisionShape3D.Disabled = true;
        _animationPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        _animationPlayer.Play(GameConstants.Anim.Expand);
    }

    private void HandleFrameChanged()
    {
        if (AnimatedSprite.Frame >= 3)
        {
            _collisionShape3D.Disabled = false;
            return;
        }

        _collisionShape3D.Disabled = true;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        AnimatedSprite.AnimationFinished -= HandleAnimationFinished;
        AnimatedSprite.FrameChanged -= HandleFrameChanged;
    }

    private void HandleAnimationFinished()
    {
        DisconnectSignals();
        QueueFree();
    }
}
