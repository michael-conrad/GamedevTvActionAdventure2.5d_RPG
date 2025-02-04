using System.Collections.Generic;
using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scripts.UI;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.General;

public partial class UiController : Control
{
    private Dictionary<ContainerType, UiContainer> _containers;

    public override void _Ready()
    {
        base._Ready();
        _containers = GetChildren().Where(e => e is UiContainer)
            .Cast<UiContainer>()
            .ToDictionary(e => e.Container, e => e);

        _containers[ContainerType.Start].Visible = true;
    }
}
