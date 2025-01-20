using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerState : Node
{
    protected Player CharacterNode;

    public override void _Ready()
    {
        base._Ready();
        SetPhysicsProcess(false);
        SetProcessInput(false);
        CharacterNode = FindParent("Player").GetNode<Player>(".");
        CharacterNode.stateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        switch (what)
        {
            case (int)GameConstants.States.StateChanged:
                EnterState();
                SetPhysicsProcess(true);
                SetProcessInput(true);
                break;
            case (int)GameConstants.States.PhysicsDisable:
                SetPhysicsProcess(false);
                SetProcessInput(false);
                break;
        }
    }

    protected virtual void EnterState()
    {
    }
}