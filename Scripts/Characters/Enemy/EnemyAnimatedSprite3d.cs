using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

[Tool]
public partial class EnemyAnimatedSprite3d : AnimatedSprite3D
{
    private bool _isConnected;
    private Timer _timer;
    public ShaderMaterial MyShaderMaterial;

    public override void _Ready()
    {
        base._Ready();
        _timer = GetNode<Timer>("Timer");
        _timer.Autostart = false;
        _timer.WaitTime = 0.25f;
        _timer.OneShot = true;
        MyShaderMaterial = (ShaderMaterial)MaterialOverlay;
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
        MyShaderMaterial.SetShaderParameter("active", false);
    }

    private void HandleFrameChanged()
    {
        MyShaderMaterial.SetShaderParameter("tex", SpriteFrames.GetFrameTexture(Animation, Frame));
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
