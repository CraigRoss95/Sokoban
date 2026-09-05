using Godot;
using System;

[Tool]
[GlobalClass]
public partial class InputControllerComponent : Node2D
{
	[Export] PlayerControllerComponent playerControllerComponent;
	[Export] double moveSpeedBufferMax = 0; 
	private Vector2 directionalMovementBuffer = new Vector2();
	private double currentMoveBufferWait = 0.0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void BufferInputs()
	{
		//Move and buffer for input
		Vector2 inputDirection = Input.GetVector("left","right","up","down");

		if (
			Global.directionList.Contains(inputDirection))
		{
			directionalMovementBuffer = inputDirection;
		}	

		//Put Use Input here	
	}

	public void UseBufferedInputs(double delta)
	{
		currentMoveBufferWait += delta ;
		if (directionalMovementBuffer != new Vector2()
		&& moveSpeedBufferMax <= currentMoveBufferWait)
		{
			currentMoveBufferWait = 0.0;
			playerControllerComponent.UseDirectionalInput(directionalMovementBuffer);
			directionalMovementBuffer = new Vector2();
		}
	}

}
