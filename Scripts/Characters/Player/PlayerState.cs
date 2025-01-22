using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public abstract partial class PlayerState : Node {
    protected Player CharacterNode;

    public override void _Ready() {
        base._Ready();
        SetPhysicsProcess(false);
        SetProcessInput(false);
        CharacterNode = FindParent("Player").GetNode<Player>(".");
    }

    public override void _Notification(int what) {
        base._Notification(what);
        switch (what) {
            case (int)GameConstants.States.EnterState:
                EnterState();
                SetPhysicsProcess(true);
                SetProcessInput(true);
                break;
            case (int)GameConstants.States.ExitState:
                SetPhysicsProcess(false);
                SetProcessInput(false);
                break;
        }
    }

    protected virtual void EnterState() { }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
        if (CharacterNode.IsOnFloor()) {
            return;
        }

        var velocity = CharacterNode.Velocity;
        velocity += CharacterNode.GetGravity(); // * CharacterNode.GravityFactor;
        CharacterNode.Velocity = velocity;
        CharacterNode.MoveAndSlide();
    }

    public bool IsNotFacingEdge() {
        return CharacterNode.RayCast.IsColliding();
    }

    public bool IsFacingEdge() {
        return !IsNotFacingEdge();
    }
}