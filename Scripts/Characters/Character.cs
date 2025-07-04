using System.Linq;
using GamedevTvActionAdventure25d_RPG.Scenes.Abilities.Interfaces;
using GamedevTvActionAdventure25d_RPG.Scripts.Resources;
using Godot;

namespace GamedevTvActionAdventure25d_RPG.Scripts.Characters;

public abstract partial class Character : CharacterBody3D
{
    private bool _isConnected = false;

    protected internal Area3D AttackArea;

    [ExportGroup("Game Settings")] //
    [Export(PropertyHint.Range, "0, 10, 0.1")]
    protected float RayDepth = 1.5f;

    [Export(PropertyHint.Range, "0, 10, 0.1")]
    protected float RayDistance = 0.4f;

    public Area3D HitBox { get; private set; }
    public Area3D HurtBox { get; private set; }

    [Export] public StatResource[] Stats { get; private set; }

    [ExportGroup("Required Nodes")] //
    [Export]
    public AnimatedSprite3D CharacterSprite { get; private set; }

    [Export] public StateMachine StateMachine { get; private set; }

    [ExportGroup("AI Nodes")] //
    [Export]
    protected internal Area3D ChaseArea { get; private set; }

    [Export] protected internal NavigationAgent3D NaviAgent { get; private set; }

    [Export] protected internal Path3D PathNode { get; private set; }

    public Vector3 Direction { get; protected set; } = Vector3.Zero;
    public Vector3 LastFacing { get; protected set; } = Vector3.Zero;

    public RayCast3D RayCast { get; set; }

    public override void _Ready()
    {
        base._Ready();
        AttackArea = GetNodeOrNull<Area3D>("AttackArea");
        HitBox = GetNodeOrNull<Area3D>("HitBox");
        HurtBox = GetNodeOrNull<Area3D>("HurtBox");
        ConnectSignals();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        DisconnectSignals();
    }

    private void DisconnectSignals()
    {
        if (!_isConnected || HurtBox == null)
        {
            return;
        }

        _isConnected = false;
        HurtBox.AreaEntered -= HandleHurtBoxEnter;
    }

    private void ConnectSignals()
    {
        if (_isConnected || HurtBox == null)
        {
            return;
        }

        _isConnected = true;
        HurtBox.AreaEntered += HandleHurtBoxEnter;
    }

    protected virtual void HandleHurtBoxEnter(Area3D area)
    {
        if (area is not IHitBox hitbox)
        {
            return;
        }

        var health = GetStatResource(Stat.Health);
        health.StatValue -= hitbox.GetDamage();
    }

    public StatResource GetStatResource(Stat stat)
    {
        return Stats.FirstOrDefault(e => e.StatType == stat);
    }

    public void Flip()
    {
        if (Direction.X < 0 || Velocity.X < 0)
        {
            CharacterSprite.FlipH = false;
        }
        else
        {
            if (Direction.X > 0 || Velocity.X > 0)
            {
                CharacterSprite.FlipH = true;
            }
        }
    }

    public void EnableHitBox(bool flag)
    {
        HitBox.Position = LastFacing * 0.5f;
        var shape = HitBox.GetNodeOrNull<CollisionShape3D>("CollisionShape3D");
        if (shape != null)
        {
            shape.Disabled = !flag;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Velocity != Vector3.Zero)
        {
            LastFacing = Velocity.Normalized();
        }
    }
}
