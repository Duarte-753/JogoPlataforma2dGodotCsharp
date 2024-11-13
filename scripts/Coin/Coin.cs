using Godot;
using System;

public partial class Coin : Area2D
{

	private AnimatedSprite2D animate;

    //[Signal]
	//public delegate void TerminouAnimacaoEventHandler();

	public override void _Ready()
	{
		animate = GetNode<AnimatedSprite2D>("Anim");
		
		// Aqui, `BodyEntered` é um evento associado ao sinal `body_entered`.
        BodyEntered += OnBodyEntered;

		// Conecta o sinal `animation_finished` ao evento diretamente
        animate.AnimationFinished += OnAnimAnimationFinished;
		
	}

	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
      GD.Print($"{body.Name} entrou na área da moeda!");
	  animate.Play("collect");
	}

	public void OnAnimAnimationFinished()
	{
	  GD.Print("Animação finalizada!");
      QueueFree();
	}
}
