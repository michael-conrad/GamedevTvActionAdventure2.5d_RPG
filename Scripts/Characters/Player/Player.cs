using GamedevTvActionAdventure25d_RPG.Scripts.General;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters.Player;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer animationPlayer;
    [Export] public Sprite3D sprite3D;

    [ExportGroup("Gameplay Settings")]
    [Export] public int speed = 5;
    
    private Vector3 _direction = Vector3.Zero;

    public override void _Ready()
    {
        animationPlayer.Play(GameConstants.Anim.Idle);
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = _direction;
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        var input = Input.GetVector(
            GameConstants.Input.MoveLeft,
            GameConstants.Input.MoveRight,
            GameConstants.Input.MoveUp,
            GameConstants.Input.MoveDown);
        _direction = new Vector3(input.X, 0, input.Y) * speed;
        Flip(_direction);
    }

    private void Flip(Vector3 motion)
    {
        if (motion.X < 0)
        {
            sprite3D.FlipH = true;
        }
        else if (motion.X > 0)
        {
            sprite3D.FlipH = false;
        }
    }
}