using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerMoveState : Node
{
    public override void _Ready()
    {
        // ignore
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.AnimStateNotification.Move)
        {
            Player player = GetParent<Player>();
            player.animationPlayer.Play(GameConstants.Anim.Move);
        }
    }
}