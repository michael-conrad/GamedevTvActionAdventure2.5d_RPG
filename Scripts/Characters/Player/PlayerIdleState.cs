using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (CheckForAttackInput())
        {
            return;
        }

        if (CheckForDashInput())
        {
            return;
        }

        if (CharacterNode.Direction != Vector3.Zero)
        {
            CharacterNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    private bool CheckForDashInput()
    {
        if (Input.IsActionJustPressed(GameConstants.Input.Dash))
        {
            CharacterNode.StateMachine.SwitchState<PlayerDashState>();
            return true;
        }

        return false;
    }


    protected override void EnterState()
    {
        base.EnterState();
        CharacterNode.CharacterSprite.Play(GameConstants.Anim.Idle);
    }
}
