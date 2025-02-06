using System.Collections.Generic;
using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class UiController : Control
{
    private Dictionary<ContainerType, UiContainer> _containers;

    private bool _isConnected = false;

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        GameEvents.OnEndGame += HandleOnEndGame;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        GameEvents.OnEndGame -= HandleOnEndGame;
    }

    private void HandleOnEndGame()
    {
        _containers[ContainerType.Stats].Visible = false;
        _containers[ContainerType.Defeat].Visible = true;
        GetTree().Paused = true;
    }

    public override void _Ready()
    {
        base._Ready();
        _containers = GetChildren().Where(e => e is UiContainer)
            .Cast<UiContainer>()
            .ToDictionary(e => e.Container, e => e);
        _containers[ContainerType.Start].Visible = true;
        ConnectSignals();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (!Visible)
        {
            return;
        }

        if (_containers[ContainerType.Start].Visible)
        {
            if (Input.IsActionJustPressed("Start"))
            {
                // do start the game
                _containers[ContainerType.Start].Visible = false;
                _containers[ContainerType.Stats].Visible = true;
                GetTree().Paused = false;
                GameEvents.RaiseStartGame();
            }
        }
    }
}
