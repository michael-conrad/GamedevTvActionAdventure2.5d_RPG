using Godot;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] private AnimationPlayer _animationPlayer;
    [Export] private Sprite3D _sprite3D;
    
    private Vector3 _direction = Vector3.Zero;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = _direction;
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        var input = Input.GetVector(
            "MoveLeft",
            "MoveRight",
            "MoveForward",
            "MoveBackward");
        _direction = new Vector3(input.X, 0, input.Y) * 5;
    }
}
