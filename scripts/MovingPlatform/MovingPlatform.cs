using Godot;
using System;


public partial class MovingPlatform : Node2D
{
	private const Double WaitDuration = 1.0;

	private AnimatableBody2D platform;

    [Export]
	public float moveSpeed = 3.0f;

	[Export]
	public int distance = 288; //192 * 1.5 que é nossa escala que aumentamos do background

	[Export]
	public bool moveHorizontal = true;
	
    // Criando uma variável do tipo Vector2 chamada Follow
	Vector2 follow = Vector2.Zero;

	[Export]
	public int platformCenter = 16;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		platform = GetNode<AnimatableBody2D>("Platform");
		MovePlatform();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		platform.Position = platform.Position.Lerp(follow, 0.5f);
	}

	public void MovePlatform()
	{
		Vector2 moveDirection = Vector2.Zero;
		
		if(moveHorizontal)
		{
			 moveDirection = Vector2.Right * distance; // Direção horizontal    
		}
		else
		{
			 moveDirection =  Vector2.Up * distance; //Direção Vertical
		}
		var duration =  moveDirection.Length() / (moveSpeed * platformCenter);

		var platformTween = CreateTween().SetLoops().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);

		platformTween.TweenProperty(this,"follow", moveDirection, duration).SetDelay(WaitDuration);// self no C# é this
		platformTween.TweenProperty(this,"follow", Vector2.Zero, duration).SetDelay(duration + WaitDuration * 2);// self no C# é this
	}
}
