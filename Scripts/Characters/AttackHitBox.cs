using GamedevTvActionAdventure25d_RPG.Scenes.Abilities.Interfaces;
using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public partial class AttackHitBox : Area3D, IHitBox
{
    public float GetDamage()
    {
        var strengthValue = GetOwnerOrNull<Character>().GetStatResource(Stat.Strength).StatValue;
        return strengthValue;
    }
}
