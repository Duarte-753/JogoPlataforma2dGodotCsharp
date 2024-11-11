using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpForce = -400.0f;

	private AnimatedSprite2D animation;

    public override void _Ready()
    {
        animation = GetNode<AnimatedSprite2D>("Animation");
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Gravidade.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Pular.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpForce;
		}

		//movimentação do player
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			animation.Play("run");

			if (direction.X !=0)
			{
              animation.FlipH = direction.X < 0;
			}
	
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			animation.Play("idle");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
