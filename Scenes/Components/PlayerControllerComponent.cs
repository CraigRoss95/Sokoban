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
	[Export] double moveSpeedBufferMax = 0; 
	private Vector2 directionalMovementBuffer = new Vector2();
	private double currentMoveBufferWait = 0.0;

	// Called when the node enters the scene tree for the first time.
	


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UseBufferedInputs(delta);
	}
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		BufferInputs();

    }

	private void BufferInputs()
	{
		//Move and buffer for input
		Vector2 inputDirection = Input.GetVector("left","right","up","down");

		if (
			Global.directionList.Contains(inputDirection))
		{
			directionalMovementBuffer = inputDirection;
		}	

		//Put Use Input here	
	}

	private void UseBufferedInputs(double delta)
	{
		currentMoveBufferWait += delta ;
		if (directionalMovementBuffer != new Vector2()
		&& moveSpeedBufferMax <= currentMoveBufferWait)
		{
			currentMoveBufferWait = 0.0;
			UseDirectionalInput(directionalMovementBuffer);
			directionalMovementBuffer = new Vector2();
		}
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

	private void UseDirectionalInput(Vector2 inputDirection)
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