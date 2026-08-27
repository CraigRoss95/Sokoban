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
	[Export]
	Timer inputBufferTimer;
	[Export]
	RayCast2D pushRayCast;

	private bool bufferInputs = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inputBufferTimer.Timeout += StopBufferingInput;
	}

    public override void _ExitTree()
    {
        inputBufferTimer.Timeout -= StopBufferingInput;
    }


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

		if (bufferInputs == false &&
			Global.directionList.Contains(inputDirection))
		{
			StartBufferInput();
			UseInput(inputDirection);
		}

		//TODO add smear lines if this if this triggers in quick succession
		//Release Buffer if movment key is released
		if (bufferInputs == true 
		&& inputDirection == new Vector2())
		{
			StopBufferingInput();
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

	private void StopBufferingInput() {bufferInputs = false;}

	private void StartBufferInput ()
	{
		bufferInputs = true;
		inputBufferTimer.Start();
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