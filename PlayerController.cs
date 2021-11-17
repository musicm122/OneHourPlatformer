using Godot;

public class PlayerController : KinematicBody2D
{
    [Export]
    public int Speed;

    [Export]
    public int JumpForce = 800;

    [Export]
    public int Gravity;


    [Export]
    public float SnapThreshold = 16;

    private Vector2 CurrentVelocity = Vector2.Zero;
    private AnimatedSprite animatedSprite;
    private CollisionShape2D collider;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        collider = GetNode<CollisionShape2D>("CollisionShape2D");

    }

    void JumpCheck()
    {
        if (CanJump())
        {
            CurrentVelocity.y -= JumpForce;
        }
    }

    Vector2 GetSnap() =>
         !Input.IsActionJustPressed(InputConstants.Jump) && IsOnFloor() ?
             Vector2.Down * SnapThreshold :
             Vector2.Zero;

    private void AnimationCheck(Vector2 velocity)
    {
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
            return;
        }
        if (velocity.x != 0)
        {
            animatedSprite.Animation = AnimationConstants.Walk;
            animatedSprite.FlipV = false;
            animatedSprite.FlipH = velocity.x < 0;
        }
        else
        {
            animatedSprite.Animation = AnimationConstants.Idle;
        }

        if (velocity.y < 0 && !IsOnFloor())
        {
            animatedSprite.Animation = AnimationConstants.Jump;
        }
    }

    public void Die()
    {
        GD.Print("You Died");
        GetTree().ReloadCurrentScene();
    }

    public override void _PhysicsProcess(float delta)
    {
        CurrentVelocity.x = 0;
        GetInputMovement();
        JumpCheck();
        var snap = GetSnap();
        CurrentVelocity = MoveAndSlideWithSnap(CurrentVelocity, snap, Vector2.Up);
        CurrentVelocity.y += Gravity * delta;
        AnimationCheck(CurrentVelocity);
    }

    bool CanJump() => Input.IsActionJustPressed(InputConstants.Jump) && IsOnFloor();

    private void GetInputMovement()
    {
        bool isRunning = Input.IsActionPressed(InputConstants.Run);

        if (Input.IsActionPressed(InputConstants.Right) && isRunning)
        {
            CurrentVelocity.x += Speed;
        }
        else if (Input.IsActionPressed(InputConstants.Right))
        {
            CurrentVelocity.x += Speed;
        }

        if (Input.IsActionPressed(InputConstants.Left) && isRunning)
        {
            CurrentVelocity.x -= Speed;
        }
        else if (Input.IsActionPressed(InputConstants.Left))
        {
            CurrentVelocity.x -= Speed;
        }
    }
}