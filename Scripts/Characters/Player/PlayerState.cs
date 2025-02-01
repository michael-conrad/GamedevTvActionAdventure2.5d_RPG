using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    public bool IsNotFacingEdge()
    {
        return CharacterNode.RayCast.IsColliding();
    }

    public bool IsFacingEdge()
    {
        return !IsNotFacingEdge();
    }

    protected void CheckForAttackInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.Input.Attack))
        {
            CharacterNode.StateMachine.SwitchState<PlayerAttackState>();
        }
    }
}
