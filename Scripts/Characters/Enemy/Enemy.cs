namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Enemy;

public partial class Enemy : Character
{
    public override void _Ready()
    {
        base._Ready();
        StateMachine.SwitchState<EnemyIdleState>();
    }
}
