using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : PlayerState {
    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (CharacterNode.Direction != Vector3.Zero) {
            CharacterNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }


    public override void _Input(InputEvent @event) {
        base._Input(@event);
        if (Input.IsActionJustPressed(GameConstants.Input.Dash)) {
            CharacterNode.StateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState() {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
    }
}