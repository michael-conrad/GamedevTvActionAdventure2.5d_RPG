using GamedevTvActionAdventure25d_RPG.Scripts.General;
using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    private bool _isConnected = false;

    public override void _Ready()
    {
        base._Ready();
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        var health = CharacterNode.GetStatResource(Stat.Health);
        health.OnZero += HandleZeroHealth;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        var health = CharacterNode.GetStatResource(Stat.Health);
        health.OnZero -= HandleZeroHealth;
    }

    protected override void Dispose(bool disposing)
    {
        DisconnectSignals();
        base.Dispose(disposing);
    }

    private void HandleZeroHealth()
    {
        CharacterNode.StateMachine.SwitchState<PlayerDeathState>();
    }

    private bool IsNotFacingEdge()
    {
        return CharacterNode.RayCast.IsColliding();
    }

    protected bool IsFacingEdge()
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
