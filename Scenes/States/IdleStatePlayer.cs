using Godot;
using System;

public partial class IdleStatePlayer : IdleState
{
	// Called when the node enters the scene tree for the first time.
	private InputManager inputManager;
	private PlayerControllerComponent playerControllerComponent;
	private MovementComponent movementComponent;
	public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

    public override void _ExitTree()
	{	
		playerControllerComponent.brodcastPush -= _ => finiteStateMachine.TransitionStateTo("PushState");
		playerControllerComponent.brodcastFailedPush -= _ => finiteStateMachine.TransitionStateTo("FailedPushState");
		movementComponent.brodcastMove -= _ => finiteStateMachine.TransitionStateTo("MoveState");;
	}
	private void _LateReady()
	{
		inputManager =  GetNode<InputManager>("/root/InputManager");

		movementComponent = gameObject.GetComponent<MovementComponent>();
		playerControllerComponent = gameObject.GetComponent<PlayerControllerComponent>();

		playerControllerComponent.brodcastPush += _ => finiteStateMachine.TransitionStateTo("PushState");
		playerControllerComponent.brodcastFailedPush += _ => finiteStateMachine.TransitionStateTo("FailedPushState");
		movementComponent.brodcastMove += _ => finiteStateMachine.TransitionStateTo("MoveState");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void UpdateState(double delta)
    {
		//TODO IMPLEMENT CODE
		inputManager.UseBufferedInputs(delta);
    }
}
