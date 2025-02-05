using System;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Resources;

[GlobalClass]
public partial class StatResource : Resource
{
    private float _statValue;
    [Export] public Stat StatType { get; private set; }

    [Export] public float StatValue
    {
        get => _statValue;
        set
        {
            _statValue = Mathf.Clamp(value, 0, Mathf.Inf);
            if (_statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }

    public event Action OnZero;

    // public StatResource()
    // {
    //     ResourceLocalToScene = true;
    // }
}
