using Godot;
using System;

[Tool]
[GlobalClass]
public partial class AdjacentRayComponent : Node2D
{	[Export] public RayCast2D rayCastUp;
	[Export] public RayCast2D rayCastRight;
	[Export] public RayCast2D rayCastDown;
	[Export] public RayCast2D rayCastLeft;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public RayCast2D GetRay (Vector2 direction)
	{
		//Note Raycast Up is at the bottom (defualt)
		if (direction == Vector2.Right)
		{
			return rayCastRight;
		}
		else if (direction == Vector2.Down)
		{
			return rayCastDown;
		}
		else if (direction == Vector2.Left)
		{
			return rayCastLeft;
		}
		else
		{
			return rayCastUp;
		}
	}
}
