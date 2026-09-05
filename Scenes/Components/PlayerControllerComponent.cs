using Godot;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;

[Tool]
[GlobalClass]
public partial class PlayerControllerComponent : Node2D
{
	[Export] MovementComponent movementComponent;
	[Export] AdjacentRayComponent	adjacentRayComponent;

	[Export] InputControllerComponent inputControllerComponent;



	// Called when the node enters the scene tree for the first time.
	


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		inputControllerComponent.UseBufferedInputs(delta);
	}
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		inputControllerComponent.BufferInputs();

    }

	

	private void Push (Vector2 direction)
	{
		Node2D pushObject = adjacentRayComponent.GetAdjacentNode(direction);
		//Not working with tilemap
		GD.Print(pushObject.Name);

		if (pushObject.GetChildren().OfType<MovementComponent>().FirstOrDefault() == null)
		{
			return;
		}
		
		MovementComponent pushObjectMoveComponent = pushObject.GetChildren().OfType<MovementComponent>().FirstOrDefault();
		if (pushObjectMoveComponent.CanMove(direction))
		{
			pushObjectMoveComponent.Move(direction);
		}
		
		else
		{
			//TODO Play sad sound
		}

	}

	public void UseDirectionalInput(Vector2 inputDirection)
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
}