using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerAttackState : PlayerState
{
    private int _comboCount = 0;
    [Export] private float _comboTime = 0.5f;

    [Export(PropertyHint.Range, "0, 10, 0.1")]
    private float _hitBoxDistance = 0.75f;

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

        var sprite = CharacterNode.CharacterSprite;
        sprite.Play(GameConstants.Anim.Attack + (_comboCount + 1));
        sprite.AnimationFinished += HandleAnimationFinished;
        sprite.FrameChanged += HandleFrameChange;


        _comboCount = ++_comboCount % _maxComboCount;
        _timer.Stop();
    }

    private void HandleFrameChange()
    {
        var sprite = CharacterNode.CharacterSprite;
        if (sprite.Frame == 4 && sprite.Animation == GameConstants.Anim.Attack + 1)
        {
            PerformHit();
        }

        if (sprite.Frame == 3 && sprite.Animation == GameConstants.Anim.Attack + 1)
        {
            PerformHit();
        }
    }


    private void HandleAnimationFinished()
    {
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        var sprite = CharacterNode.CharacterSprite;
        sprite.AnimationFinished -= HandleAnimationFinished;
        sprite.FrameChanged -= HandleFrameChange;
        _timer.Start(_comboTime);
    }

    private void PerformHit()
    {
        CharacterNode.HitBox.Position = CharacterNode.Direction * _hitBoxDistance;
        GD.Print("Perform hit");
    }
}
