using GamedevTvActionAdventure25d_RPG.Scenes.Abilities;
using GamedevTvActionAdventure25d_RPG.Scenes.Abilities.Interfaces;
using Godot;

public partial class BombHitbox : Area3D, IHitBox
{
    public float GetDamage()
    {
        var bomb = GetParentOrNull<Bomb>();
        if (bomb == null)
        {
            return 0;
        }

        return bomb.Damage;
    }
}
