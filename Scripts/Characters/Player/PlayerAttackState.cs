using GamedevTvActionAdventure25d_RPG.Scripts.Abilities;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerAttackState : PlayerState
{
    private int _comboCount = 0;
    [Export] private float _comboTime = 0.5f;
    private bool _isConnected = false;
    [Export] private PackedScene _lightningScene;

    private int _maxComboCount = 2;
    private Timer _timer;

    public override void _Ready()
    {
        base._Ready();
        _timer = new Timer();
        _timer.OneShot = true;
        _timer.WaitTime = _comboTime;
        AddChild(_timer);
        _timer.Timeout += () => _comboCount = 0;
    }

    protected override void EnterState()
    {
        base.EnterState();
        ConnectSignals();
        var sprite = CharacterNode.CharacterSprite;
        sprite.Play(GameConstants.Anim.Attack + (_comboCount + 1));
        _comboCount = ++_comboCount % _maxComboCount;
        _timer.Stop();
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished += HandleAnimationFinished;
        sprite.FrameChanged += HandleFrameChange;
        var hitbox = CharacterNode.HitBox;
        hitbox.BodyEntered += HandleBodyEntered;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (_comboCount + 1 != _maxComboCount)
        {
            return;
        }

        var lightning = _lightningScene.Instantiate<Lightning>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished -= HandleAnimationFinished;
        sprite.FrameChanged -= HandleFrameChange;

        var hitbox = CharacterNode.HitBox;
        hitbox.BodyEntered -= HandleBodyEntered;
    }

    private void HandleFrameChange()
    {
        var sprite = CharacterNode.CharacterSprite;
        if (sprite.Frame == 4 && sprite.Animation == GameConstants.Anim.Attack + 1)
        {
            PerformHit();
        }

        if (sprite.Frame == 3 && sprite.Animation == GameConstants.Anim.Attack + 2)
        {
            PerformHit();
        }
    }


    private void HandleAnimationFinished()
    {
        CharacterNode.EnableHitBox(false);
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        DisconnectSignals();
        _timer.Start(_comboTime);
    }
}
