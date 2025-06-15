using GamedevTvActionAdventure25d_RPG.Scenes.Abilities.Interfaces;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Abilities;

public partial class AbilityHitbox : Area3D, IHitBox
{
    public float GetDamage()
    {
        var ability = GetParentOrNull<Ability>();
        if (ability == null)
        {
            return 0;
        }

        return ability.Damage;
    }

    public bool CanStun()
    {
        return true;
    }
}
