using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class Enemy : Character
{
    private Timer _flashTimer;
    private ShaderMaterial _myShaderMaterial;

    public override void _Ready()
    {
        base._Ready();
        _flashTimer = GetNode<Timer>("AnimatedSprite3D/Timer");
        _myShaderMaterial = GetNode<EnemyAnimatedSprite3d>("AnimatedSprite3D").MyShaderMaterial;
        StateMachine.SwitchState<EnemyIdleState>();
    }

    protected override void HandleHurtBoxEnter(Area3D area)
    {
        base.HandleHurtBoxEnter(area);
        _myShaderMaterial.SetShaderParameter("active", true);
        _flashTimer.Start();
    }
}
