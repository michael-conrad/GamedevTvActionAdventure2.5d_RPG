namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public abstract partial class PlayerState : CharacterState
{
    public bool IsNotFacingEdge()
    {
        return CharacterNode.RayCast.IsColliding();
    }

    public bool IsFacingEdge()
    {
        return !IsNotFacingEdge();
    }
}
