using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class UiContainer : CenterContainer
{
    [Export] public ContainerType Container { get; private set; }
}
