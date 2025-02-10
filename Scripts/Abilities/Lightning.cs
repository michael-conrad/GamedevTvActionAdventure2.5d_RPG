using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Abilities;

public partial class Lightning : Ability
{
    private CollisionShape3D _hitBox;
    private bool _isConnected = false;

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        AnimatedSprite.AnimationFinished += HandleLightningFinished;
        AnimatedSprite.FrameChanged += HandleFrameChanged;
    }

    private void HandleFrameChanged()
    {
        var frame = AnimatedSprite.Frame;
        if (frame == 0)
        {
            _hitBox.Disabled = true;
            return;
        }

        if (frame == 5)
        {
            _hitBox.Disabled = false;
            return;
        }

        if (frame == 10)
        {
            _hitBox.Disabled = true;
        }
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        AnimatedSprite.AnimationFinished -= HandleLightningFinished;
        AnimatedSprite.FrameChanged -= HandleFrameChanged;
    }

    public override void _Ready()
    {
        base._Ready();
        _hitBox = GetNodeOrNull<CollisionShape3D>("HitBox/CollisionShape3D");
        ConnectSignals();
        AnimatedSprite.Stop();
        AnimatedSprite.Play();
    }

    private void HandleLightningFinished()
    {
        DisconnectSignals();
        QueueFree();
    }
}
