using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 800.0f;
	public const float JumpVelocity = -400.0f;

	Vector2 direction = Vector2.Left;

	private RayCast2D _WallDetector;
	private Sprite2D _Texture;

    public override void _Ready()
    {
       _WallDetector = GetNode<RayCast2D>("WalkDetector");
	   _Texture = GetNode<Sprite2D>("Texture");
    }

    public override void _PhysicsProcess(double delta)
	{

		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}	
				
		if(_WallDetector.IsColliding())
		{			
               direction *= Vector2.Left;
			   _WallDetector.Scale *= -1;
		}

		if(direction.X == 1)
		{
			_Texture.FlipH = true;
		}
		
		if(direction.X == -1)
		{
			_Texture.FlipH = false;
		}

		velocity.X = direction.X * Speed * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}
}
