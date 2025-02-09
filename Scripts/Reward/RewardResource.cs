using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Reward;

[GlobalClass]
public partial class RewardResource : Resource
{
    [Export] public Texture2D RewardTexture { get; private set; }
    [Export] public string RewardName { get; private set; }
    [Export] public Stat TargetStat { get; private set; }

    [Export(PropertyHint.Range, "1,100,1")]
    public float TargetValue { get; private set; }
}
