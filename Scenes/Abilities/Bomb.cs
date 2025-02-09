using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scenes.Abilities;

public partial class Bomb : Node3D
{
    [Export] private AnimatedSprite3D _animatedSprite3D;
    private AnimationPlayer _animationPlayer;

    [Export] private CollisionShape3D _collisionShape3D;

    private bool _isConnected = false;
    [Export] private Sprite3D _sprite;

    [Export] public int Damage { get; private set; }

    private void ConnectSignals()
    {
        if (_isConnected) return;
        _isConnected = true;
        _animatedSprite3D.AnimationFinished += HandleAnimationFinished;
        _animatedSprite3D.FrameChanged += HandleFrameChanged;
    }

    public override void _Ready()
    {
        base._Ready();
        ConnectSignals();
        _sprite.Visible = true;
        _animatedSprite3D.Visible = false;
        _collisionShape3D.Disabled = true;
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animationPlayer.Play(GameConstants.Anim.Expand);
    }

    private void HandleFrameChanged()
    {
        if (_animatedSprite3D.Frame >= 3)
        {
            _collisionShape3D.Disabled = false;
            return;
        }

        _collisionShape3D.Disabled = true;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected) return;
        _isConnected = false;
        _animatedSprite3D.AnimationFinished -= HandleAnimationFinished;
        _animatedSprite3D.FrameChanged -= HandleFrameChanged;
    }

    private void HandleAnimationFinished()
    {
        DisconnectSignals();
        QueueFree();
    }
}
