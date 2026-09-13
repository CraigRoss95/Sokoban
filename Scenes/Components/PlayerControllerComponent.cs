using Godot;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;

[GlobalClass]
public partial class PlayerControllerComponent : Component
{
	public MovementComponent movementComponent;
	public AdjacentRayComponent adjacentRayComponent;
	public InputControllerComponent inputControllerComponent;

	private GameObject parent;



    // Called when the node enters the scene tree for the first time.
  		public override void _Ready()
	{
		parent = GetParent<GameObject>();
		Callable.From(_LateReady).CallDeferred();
	}
	public void _LateReady()
	{
		movementComponent = parent.GetComponent<MovementComponent>();
		adjacentRayComponent = parent.GetComponent<AdjacentRayComponent>();
		inputControllerComponent = parent.GetComponent<InputControllerComponent>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
    
	private void Push (Vector2 direction)
	{
		Node2D pushObject = adjacentRayComponent.GetAdjacentNode(direction);

		if (pushObject == null ||
			pushObject.GetChildren().OfType<MovementComponent>().FirstOrDefault() == null)
		{
			return;
		}
		//Not working with tilemap
		GD.Print(pushObject.Name);

		MovementComponent pushObjectMoveComponent = pushObject.GetChildren().OfType<MovementComponent>().FirstOrDefault();
		if (pushObjectMoveComponent.CanMove(direction))
		{
			pushObjectMoveComponent.Move(direction);
			movementComponent.ForceMove(direction);
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