using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

[Tool]
public partial class PlayerAnimatedSprite3d : AnimatedSprite3D
{
    private bool _isConnected = false;
    private ShaderMaterial _shaderMaterial;

    public override void _Ready()
    {
        base._Ready();
        ConnectSignals();
        _shaderMaterial = (ShaderMaterial)MaterialOverride;

    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        FrameChanged += HandleFrameChanged;
    }

    private void HandleFrameChanged()
    {
        _shaderMaterial.SetShaderParameter("tex", SpriteFrames.GetFrameTexture(Animation, Frame));
    }

    protected override void Dispose(bool disposing)
    {
        DisconnectSignals();
        base.Dispose(disposing);
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        FrameChanged -= HandleFrameChanged;
    }
}
