using GamedevTvActionAdventure25d_RPG.Scripts.General;
using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();
        var health = CharacterNode.GetStatResource(Stat.Health);
        health.OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachine.SwitchState<PlayerDeathState>();
    }

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
