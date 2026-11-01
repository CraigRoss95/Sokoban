using Godot;
using System;

public partial class MoveState : State

{
	MovementComponent movementComponent;
	// Called when the node enters the scene tree for the first time.

		public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

    public override void _ExitTree()
    {		
		movementComponent.brodcastDoneMoving -= () => finiteStateMachine.TransitionStateTo("IdleState");

    }


	private void _LateReady()
	{
		movementComponent = gameObject.GetComponent<MovementComponent>();
		movementComponent.brodcastDoneMoving += () => finiteStateMachine.TransitionStateTo("IdleState");

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
