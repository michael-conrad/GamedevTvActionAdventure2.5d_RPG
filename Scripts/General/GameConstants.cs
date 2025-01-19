namespace GamedevTvActionAdventure25d_RPG.Scripts.General;

public abstract class GameConstants
{
    public enum States
    {
        StateChanged = 5_001,
        PhysicsDisable = 5_002
    }

    public abstract class Anim
    {
        public const string Idle = "Idle";
        public const string Move = "Move";
    }

    public abstract class Input
    {
        public const string MoveLeft = "MoveLeft";
        public const string MoveRight = "MoveRight";
        public const string MoveUp = "MoveForward";
        public const string MoveDown = "MoveBackward";
    }
}