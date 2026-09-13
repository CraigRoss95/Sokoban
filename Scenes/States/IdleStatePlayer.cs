using Godot;
using System;

public partial class IdleStatePlayer : IdleState
{
	// Called when the node enters the scene tree for the first time.
	private InputControllerComponent inputControllerComponent;
	public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

	private void _LateReady()
	{
		inputControllerComponent = gameObject.GetComponent<InputControllerComponent>();
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

}
