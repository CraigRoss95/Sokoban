using Godot;
using System;

public partial class FailedPushState : State

{
	InputManager inputManager;
	// Called when the node enters the scene tree for the first time.

		public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

    public override void _ExitTree()
    {
		inputManager.brodcastNoInputAtEndOfTurn -= () => finiteStateMachine.TransitionStateTo("IdleState");
    }


	private void _LateReady()
	{
		inputManager =  GetNode<InputManager>("/root/InputManager");
		inputManager.brodcastNoInputAtEndOfTurn += () => finiteStateMachine.TransitionStateTo("IdleState");

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
   
 
}
