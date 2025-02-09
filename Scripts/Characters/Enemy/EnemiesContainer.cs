using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class EnemiesContainer : Node3D
{
    private bool _isConnected = false;
    private int _totalEnemies = 0;

    public override void _Ready()
    {
        base._Ready();
        _totalEnemies = GetChildCount();
        GameEvents.RaiseNewEnemyCount(_totalEnemies);
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        ChildExitingTree += HandleChildExitingTree;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        ChildExitingTree -= HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        GameEvents.RaiseNewEnemyCount(--_totalEnemies);
        if (_totalEnemies == 0)
        {
            GameEvents.RaiseVictory();
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == NotificationWMCloseRequest)
        {
            DisconnectSignals();
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        DisconnectSignals();
    }
}
