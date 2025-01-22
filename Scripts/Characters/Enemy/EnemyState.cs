using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public abstract partial class EnemyState : CharacterState {
    protected override void EnterState() {
        base.EnterState();
        GD.Print("Entering Enemy State: " + CharacterNode.Name);
    }
}