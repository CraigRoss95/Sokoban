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
		playerControllerComponent.brodcastPush -= _ => this.RequestTransitionToState("PushState");
		playerControllerComponent.brodcastFailedPush -= _ => this.RequestTransitionToState("FailedPushState");
		movementComponent.brodcastMove -= _ => this.RequestTransitionToState("MoveState");;
	}
	private void _LateReady()
	{
		inputManager =  GetNode<InputManager>("/root/InputManager");

		movementComponent = gameObject.GetComponent<MovementComponent>();
		playerControllerComponent = gameObject.GetComponent<PlayerControllerComponent>();

		playerControllerComponent.brodcastPush += _ => this.RequestTransitionToState("PushState");
		playerControllerComponent.brodcastFailedPush += _ => this.RequestTransitionToState("FailedPushState");
		movementComponent.brodcastMove += _ => this.RequestTransitionToState("MoveState");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
