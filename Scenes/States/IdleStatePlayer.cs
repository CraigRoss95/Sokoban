using Godot;
using System;

public partial class IdleStatePlayer : IdleState
{
	// Called when the node enters the scene tree for the first time.
	private InputControllerComponent inputControllerComponent;
	private PlayerControllerComponent playerControllerComponent;
	private MovementComponent movementComponent;
	public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

    public override void _ExitTree()
	{
		
	}


	private void _LateReady()
	{
		inputControllerComponent = gameObject.GetComponent<InputControllerComponent>();
		playerControllerComponent = gameObject.GetComponent<PlayerControllerComponent>();
		movementComponent = gameObject.GetComponent<MovementComponent>();

		playerControllerComponent.brodcastPush += TransitionStateToPush;
		movementComponent.brodcastMove += TransitionStateToMove;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void UpdateState(double delta)
    {
		//TODO IMPLEMENT CODE
		inputControllerComponent.UseBufferedInputs(delta);
    }

	private void TransitionStateToMove(Vector2 direction)
	{
		finiteStateMachine.TransitionStateTo("MoveState");
	}

	private void TransitionStateToPush(Vector2 direction)
	{
		finiteStateMachine.TransitionStateTo("PushState");
	}


}
