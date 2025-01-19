using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : Node
{
    public override void _Ready()
    {
        Player player = GetParent<Player>();
        player.animationPlayer.Play(GameConstants.Anim.Idle);
    }
}
