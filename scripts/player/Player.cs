using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpForce = -400.0f;

	private bool IsJump = false;
	private bool IsRun = false;
	private bool IsIdle = false;

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
			IsRun = false;
			IsJump = true;
		}

		// Pular.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpForce;
			IsJump = true;
			IsRun = false;		
		}

		//movimentação do player (horizontal)
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if(IsOnFloor())
			{
			  //velocity.X = direction.X * Speed;

			  IsRun = true;
				if(IsRun)
				{
					IsJump = false;
					animation.Play("run");
				}			      
			}

			else if(!IsOnFloor())
			{
				//velocity.X = direction.X * 200.0f;
				animation.Play("jump");
			}	
			// Inverte a animação dependendo da direção do movimento
			animation.FlipH = direction.X < 0;			
		}
		else if (IsOnFloor())
		{
			 // Se o jogador não estiver se movendo, toque a animação "idle"
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

			if(IsJump)
			{
				animation.Play("jump");
				if(IsOnFloor())
				{
					IsJump =  false;					
					IsIdle = true;
				}
			}
			else if(IsIdle)
			{				
                animation.Play("idle");
			}			
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
