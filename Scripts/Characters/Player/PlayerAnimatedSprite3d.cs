using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerAnimatedSprite3d : AnimatedSprite3D
{
    private ShaderMaterial _shaderMaterial;

    public override void _Ready()
    {
        base._Ready();
        _shaderMaterial = (ShaderMaterial)MaterialOverride;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        _shaderMaterial.SetShaderParameter("tex", SpriteFrames.GetFrameTexture(Animation, Frame));
        // _shaderMaterial.SetShaderParameter("billboard_mode", Billboard);
    }
}
