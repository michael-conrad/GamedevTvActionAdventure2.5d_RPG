using Godot;
using System;
using GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;
using GamedevTvActionAdventure25d_RPG.Scripts.General;

public partial class MoveState : Node
{
    public override void _Ready()
    {
        Player player = GetParent<Player>();
        player.animationPlayer.Play(GameConstants.Anim.Move);
    }
}
