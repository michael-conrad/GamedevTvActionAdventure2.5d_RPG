using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class StatsLabel : Label
{
    private bool _isConnected = false;

    [ExportGroup("Game Settings")]
    // Used to fence event subscriptions to prevent confusing error messages in the log
    [Export(PropertyHint.File, "*.tres")]
    private StatResource _stat;

    private void ConnectSignals()
    {
        if (_isConnected || _stat == null)
        {
            return;
        }

        _stat.OnUpdate += HandleUpdate;
        _isConnected = true;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected || _stat == null)
        {
            return;
        }

        _stat.OnUpdate -= HandleUpdate;
        _isConnected = false;
    }

    private void HandleUpdate()
    {
        if (_stat == null)
        {
            return;
        }

        Text = _stat.StatValue.ToString();
    }

    public override void _Ready()
    {
        base._Ready();
        HandleUpdate();
        ConnectSignals();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        DisconnectSignals();
    }
}
