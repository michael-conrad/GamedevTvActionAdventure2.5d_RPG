using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : PlayerState {
    public override void _Ready() {
        base._Ready();
        CharacterNode.StateMachine.SwitchState<PlayerIdleState>();
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (CharacterNode.Direction != Vector3.Zero) {
            CharacterNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }


    // TODO: Do this better, using a proper state machine class.
    public override void _Input(InputEvent @event) {
        base._Input(@event);
        if (Input.IsActionJustPressed("Dash")) {
            CharacterNode.StateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState() {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
    }
}