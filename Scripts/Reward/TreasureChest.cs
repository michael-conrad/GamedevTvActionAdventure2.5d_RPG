using GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Reward;

public partial class TreasureChest : StaticBody3D
{
    private Area3D _areaNode;
    private Sprite3D _chestClosed;
    private Sprite3D _chestOpen;
    private bool _isConnected = false;
    private Sprite3D _notificationIcon;

    [Export] private RewardResource _reward;

    public override void _Ready()
    {
        base._Ready();
        _areaNode = GetNodeOrNull<Area3D>("Area3D");
        _notificationIcon = GetNodeOrNull<Sprite3D>("InteractiveIcon");
        _chestOpen = GetNodeOrNull<Sprite3D>("ChestOpen");
        _chestClosed = GetNodeOrNull<Sprite3D>("ChestClosed");
        ConnectSignals();
        ShowNotificationIcon(false);
        ShowChestOpen(false);
    }

    private void ShowChestOpen(bool isOpen)
    {
        if (!isOpen)
        {
            _chestOpen.Visible = false;
            _chestClosed.Visible = true;
            return;
        }

        _chestOpen.Visible = true;
        _chestClosed.Visible = false;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!_notificationIcon.Visible || !Input.IsActionJustPressed(GameConstants.Input.Interact))
        {
            return;
        }

        ShowChestOpen(true);
        GD.Print("Treasure Chest Opened");
    }

    private void ShowNotificationIcon(bool visible)
    {
        if (_notificationIcon == null)
        {
            return;
        }

        _notificationIcon.Visible = visible;
    }

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        _areaNode.BodyEntered += HandleBodyEntered;
        _areaNode.BodyExited += HandleBodyExited;
    }

    private void HandleBodyExited(Node3D body)
    {
        if (body is Player)
        {
            ShowNotificationIcon(false);
        }
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (body is Player)
        {
            ShowNotificationIcon(true);
        }
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        _areaNode.AreaEntered -= HandleBodyEntered;
        _areaNode.AreaExited -= HandleBodyExited;
    }
}
