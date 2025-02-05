using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerAttackState : PlayerState
{
    private int _comboCount = 0;
    [Export] private float _comboTime = 0.5f;
    private bool _isConnected = false;

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
        if (_isConnected) return;
        _isConnected = true;
        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished += HandleAnimationFinished;
        sprite.FrameChanged += HandleFrameChange;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected) return;
        _isConnected = false;
        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished -= HandleAnimationFinished;
        sprite.FrameChanged -= HandleFrameChange;
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
