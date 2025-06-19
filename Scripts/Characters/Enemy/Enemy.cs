using GamedevTvActionAdventure25d_RPG.Scenes.Abilities.Interfaces;
using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class Enemy : Character
{
    private Timer _flashTimer;
    private bool _isConnected;
    private ShaderMaterial _myShaderMaterial;

    public override void _Ready()
    {
        base._Ready();
        _flashTimer = GetNode<Timer>("EnemyAnimatedSprite3d/Timer");
        _myShaderMaterial = GetNode<EnemyAnimatedSprite3d>("EnemyAnimatedSprite3d").MyShaderMaterial;
        StateMachine.SwitchState<EnemyIdleState>();
        MyConnectSignals();
    }

    private void MyConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        HurtBox.AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area3D area)
    {
        if (area is not IHitBox hitbox)
        {
            return;
        }

        if (hitbox.CanStun() && GetStatResource(Stat.Health).StatValue > 0)
        {
            StateMachine.SwitchState<EnemyStunState>();
        }
    }

    public override void _ExitTree()
    {
        MyDisconnectSignals();
        base._ExitTree();
    }

    private void MyDisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        HurtBox.AreaEntered -= HandleAreaEntered;
    }

    protected override void HandleHurtBoxEnter(Area3D area)
    {
        base.HandleHurtBoxEnter(area);
        _myShaderMaterial.SetShaderParameter("active", true);
        _flashTimer.Start();
    }
}
