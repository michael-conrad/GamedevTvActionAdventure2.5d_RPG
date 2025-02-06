using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class UiContainer : Container
{
    [Export] public ContainerType Container { get; private set; }
    [Export] public Button StartButton { get; private set; }
}
