using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.UI;

public partial class UiContainer : CenterContainer
{
    [Export] public ContainerType Container { get; private set; }
    [Export] public Button StartButton { get; private set; }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (!Visible)
        {
            return;
        }

        if (Input.IsActionJustPressed("Start"))
        {
            // do start the game
            Visible = false;
            GetTree().Paused = false;
        }
    }
}
