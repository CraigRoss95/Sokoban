using Godot;
using System;

public partial class IdleState : State
{
		private MovementComponent movementComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();

	}

	 public override void _ExitTree()
	{
		movementComponent.brodcastMove -= TransitionStateToMove;
	}
	private void _LateReady()
	{
		movementComponent = gameObject.GetComponent<MovementComponent>();

		movementComponent.brodcastMove += TransitionStateToMove;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void UpdateState(double delta)
    {
		//TODO IMPLEMENT CODE
    }

	private void TransitionStateToMove(Vector2 direction)
	{
		finiteStateMachine.TransitionStateTo("MoveState");
		GD.Print("box moved");
	
	}

}
