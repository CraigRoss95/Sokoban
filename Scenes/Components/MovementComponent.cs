using Godot;


[Tool]
[GlobalClass]
public partial class MovementComponent : Node2D
{
[Export] AdjacentRayComponent adjacentRayComponent;
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
		RayCast2D ray = adjacentRayComponent.GetRay(direction);
		if (ray.IsColliding())
		{
			Node collision = (Node)ray.GetCollider();
			GD.Print("impact " + GetParent().Name + collision.GetParent().Name);
			return false;
		}
		return true;
	}
}
