using System.Collections.Generic;
using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scripts.General;
using GamedevTvActionAdventure25d_RPG.Scripts.Reward;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class UiController : Control
{
    private Dictionary<ContainerType, UiContainer> _containers;

    private bool _isConnected = false;

    private bool _isPaused = false;

    private void ConnectSignals()
    {
        if (_isConnected)
        {
            return;
        }

        _isConnected = true;
        GameEvents.OnEndGame += HandleOnEndGame;
        GameEvents.OnVictory += HandleOnVictory;
        GameEvents.OnPauseToggle += HandleOnPauseToggle;
        GameEvents.OnReward += HandleOnReward;
    }

    private void HandleOnReward(RewardResource reward)
    {
        var uiContainer = _containers[ContainerType.Reward];
        uiContainer.Visible = true;
        GetTree().Paused = true;
        uiContainer.RewardTexture.Texture = reward.RewardTexture;
        uiContainer.RewardTextLabel.Text = reward.RewardName;
    }

    private void HandleOnPauseToggle()
    {
        if (_containers.Count(c => c.Value.Visible && c.Key != ContainerType.Stats) > 1)
        {
            return;
        }

        _isPaused = !_isPaused;
        GetTree().Paused = _isPaused;
        _containers[ContainerType.Pause].Visible = _isPaused;
    }


    private void HandleOnVictory()
    {
        _containers[ContainerType.Stats].Visible = false;
        _containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void DisconnectSignals()
    {
        if (!_isConnected)
        {
            return;
        }

        _isConnected = false;
        GameEvents.OnEndGame -= HandleOnEndGame;
        GameEvents.OnVictory -= HandleOnVictory;
        GameEvents.OnPauseToggle -= HandleOnPauseToggle;
        GameEvents.OnReward -= HandleOnReward;
    }

    private void HandleOnEndGame()
    {
        _containers[ContainerType.Stats].Visible = false;
        _containers[ContainerType.Defeat].Visible = true;
    }

    public override void _Ready()
    {
        base._Ready();
        _containers = GetChildren().Where(e => e is UiContainer)
            .Cast<UiContainer>()
            .ToDictionary(e => e.Container, e => e);
        foreach (var container in _containers.Values)
        {
            container.Visible = false;
        }

        _containers[ContainerType.Start].Visible = true;
        ConnectSignals();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        var isStartJustPressed = Input.IsActionJustPressed(GameConstants.Input.Start);
        if (isStartJustPressed)
        {
            if (_containers[ContainerType.Start].Visible)
            {
                // do start the game
                _containers[ContainerType.Start].Visible = false;
                _containers[ContainerType.Stats].Visible = true;
                GetTree().Paused = false;
                GameEvents.RaiseStartGame();
                return;
            }

            if (_containers[ContainerType.Reward].Visible)
            {
                // do close the reward
                _containers[ContainerType.Reward].Visible = false;
                GetTree().Paused = false;
                return;
            }

            if (_containers[ContainerType.Stats].Visible)
            {
                // do pause the game
                GameEvents.RaisePauseToggle();
                return;
            }
        }
    }
}
