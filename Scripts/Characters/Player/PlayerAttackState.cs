using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerAttackState : PlayerState
{
    private int _comboCount = 0;
    [Export] private float _comboTime = 0.5f;
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

        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Attack + (_comboCount + 1));
        CharacterNode.CharacterSprite.AnimationFinished += HandleAnimationFinished;

        _comboCount = ++_comboCount % _maxComboCount;
        _timer.Stop();
    }

    private void HandleAnimationFinished()
    {
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void ExitState()
    {
        base.ExitState();
        CharacterNode.CharacterSprite.AnimationFinished -= HandleAnimationFinished;
        _timer.Start(_comboTime);
    }
}
