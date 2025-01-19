using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class PlayerIdleState : Node
{
    private Player _player;

    public override void _Ready()
    {
        _player = GetParent<Player>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.Anim.StateChanged)
        {
            _player.animationPlayer.Play(GameConstants.Anim.Idle);
        }
    }
}