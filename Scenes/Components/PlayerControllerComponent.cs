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

	// Called when the node enters the scene tree for the first time.
	


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		HandelDirectionalMovement();

    }

	private void HandelDirectionalMovement()
	{
		//Move and buffer for input
		Vector2 inputDirection = Input.GetVector("left","right","up","down");

		if (
			Global.directionList.Contains(inputDirection))
		{
			UseInput(inputDirection);
		}

		//TODO add smear lines if this if this triggers
		
	}

	private void Push (Vector2 direction)
	{
		Node2D pushObject = adjacentRayComponent.GetAdjacentNode(direction);
		GD.Print(pushObject.Name);

		if (!pushObject.HasNode("MovementComponent"))
		{
			return;
		}
		
		MovementComponent pushObjectMoveComponent = pushObject.GetNode<MovementComponent>("MovementComponent");
		if (pushObjectMoveComponent.CanMove(direction))
		{
			pushObjectMoveComponent.Move(direction);
		}
		
		else
		{
			//TODO Play sad sound
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
}