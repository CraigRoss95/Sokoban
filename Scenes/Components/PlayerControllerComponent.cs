using Godot;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;

[Tool]
[GlobalClass]
public partial class PlayerControllerComponent : Node2D
{
	[Export]
	MovementComponent movementComponent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
    public override void _Input(InputEvent @event)
    {

		// TODO LAME! inplement an input buffer (so you can hold down the button and not zoom off screen)
        base._Input(@event);

		if (Input.GetVector("left","right","up","down") != new Vector2())
		{
			if (@event.IsActionPressed("up"))
			{
				UseInput(Vector2.Up);
			}
			else if (@event.IsActionPressed("down"))
			{
				UseInput(Vector2.Down);
			}
			else if (@event.IsActionPressed("left"))
			{
				UseInput(Vector2.Left);
			}
			else if (@event.IsActionPressed("right"))
			{
				UseInput(Vector2.Right);
			}
		}

    }

	private void UseInput(Vector2 inputDirection)
	{
		if (movementComponent.CanMove(inputDirection))
		{
			movementComponent.Move(inputDirection);
		}
		else
		{
			Push(inputDirection);
		}
	}

	private void Push (Vector2 direction)
	{
		Node pushObject = new Node(); //TODO use raycast to get push object

		// TODO Does this work?
		MovementComponent pushObjectMovmentComponent= pushObject.GetChildren().OfType<MovementComponent>().FirstOrDefault();

		if (pushObjectMovmentComponent != null)
		{
			if (pushObjectMovmentComponent.CanMove(direction))
			{
				pushObjectMovmentComponent.Move(direction);
			}
		}
		else
		{
			//TODO Play sad sound
		}

	}

}
