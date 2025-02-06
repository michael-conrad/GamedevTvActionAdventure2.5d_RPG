using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class EnemyCountLabel : Label
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
        GameEvents.OnNewEnemyCount += HandleNewEnemyCount;
    }

    private void HandleNewEnemyCount(int count)
    {
        Text = $"{count:000}";
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        DisconnectSignals();
    }
}
