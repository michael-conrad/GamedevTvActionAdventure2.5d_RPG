using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : Node
{
    public override void _Ready()
    {
        // ignore
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.AnimStateNotification.Idle)
        {
            Player player = GetParent<Player>();
            player.animationPlayer.Play(GameConstants.Anim.Idle);
        }
    }
}