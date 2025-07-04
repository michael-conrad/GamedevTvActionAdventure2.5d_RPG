using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

[Tool]
public partial class PlayerAnimatedSprite3d : AnimatedSprite3D
{
    private bool _isConnected = false;
    private Timer _timer;
    public ShaderMaterial ShaderMaterialOverlay;

    public override void _Ready()
    {
        base._Ready();
        _timer = GetNode<Timer>("Timer");
        _timer.Autostart = false;
        _timer.WaitTime = 0.25f;
        _timer.OneShot = true;
        ShaderMaterialOverlay = (ShaderMaterial)MaterialOverlay;
        ConnectSignals();
        HandleFrameChanged(); //initialize tex with current frame. fixes editor display.
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        FrameChanged += HandleFrameChanged;
        _timer.Timeout += OnTimerOnTimeout;
    }

    private void OnTimerOnTimeout()
    {
        ShaderMaterialOverlay.SetShaderParameter("active", false);
    }

    private void HandleFrameChanged()
    {
        ShaderMaterialOverlay.SetShaderParameter("tex", SpriteFrames.GetFrameTexture(Animation, Frame));
    }

    public override void _ExitTree()
    {
        DisconnectSignals();
        base._ExitTree();
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        _timer.Timeout -= OnTimerOnTimeout;
        FrameChanged -= HandleFrameChanged;
    }
}
