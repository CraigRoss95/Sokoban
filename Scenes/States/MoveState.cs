using Godot;
using System;

public partial class MoveState : State

{
	MovementComponent movementComponent;
	InputManager inputManager;
	// Called when the node enters the scene tree for the first time.

		public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

    public override void _ExitTree()
    {		
		inputManager.brodcastNoInputAtEndOfTurn -= () => this.RequestTransitionToState("IdleState");
    }


	private void _LateReady()
	{
		inputManager =  GetNode<InputManager>("/root/InputManager");

		inputManager.brodcastNoInputAtEndOfTurn += () => this.RequestTransitionToState("IdleState");
		//movementComponent.brodcastDoneMoving += finiteStateMachine.ChangeStateAction("IdleState");

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
