using Godot;
using System;


[Tool]
[GlobalClass]
public partial class MovementComponent : Node2D

{
	[Export] public RayCast2D checkDirectionRayCast;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		
	}

	public void Move(Vector2 direction)
	{
		if (CanMove(direction))
		GetParent<Node2D>().GlobalPosition = GlobalPosition + (direction * Global.pixelGridSize);
	}

	public bool CanMove(Vector2 direction)
	{
		//TODO IMPORTANT This line is running on the same frame as input so the player isn't rotating
		checkDirectionRayCast.Rotation = direction.Angle();
		if (checkDirectionRayCast.IsColliding())
		{
			Node collision = (Node)checkDirectionRayCast.GetCollider();
			GD.Print("impact " + GetParent().Name + collision.GetParent().Name);
			return false;
		}
		
		//TODO use colliers and raycasts to figure this out (allways true for now)
		return true;
	}
	
}
