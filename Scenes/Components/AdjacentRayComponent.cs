using Godot;
using System;
using System.ComponentModel;

[GlobalClass]
public partial class AdjacentRayComponent : Component
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

	public RayCast2D GetRay (Vector2 rayDirection)
	{
		//Note Raycast Up is at the bottom (defualt)
		if (rayDirection == Vector2.Right)
		{
			return rayCastRight;
		}
		else if (rayDirection == Vector2.Down)
		{
			return rayCastDown;
		}
		else if (rayDirection == Vector2.Left)
		{
			return rayCastLeft;
		}
		else
		{
			return rayCastUp;
		}
	}

	public Node2D GetAdjacentNode(Vector2 rayDirection)
	{
		RayCast2D ray = GetRay(rayDirection);
		if (ray.IsColliding())
		{
			Node2D collision = (Node2D)ray.GetCollider();
			return collision.GetParent<Node2D>();
		}
		else
		{
			return null;
		}
	}
}
