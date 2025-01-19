using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public partial class StateMachine : Node
{
    [Export] private Node _currentState;
    [Export] private Node[] _states;

    public override void _Ready()
    {
        Notification(GameConstants.AnimStateNotification.Idle);
    }
}