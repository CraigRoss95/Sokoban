using Godot;


[Tool]
[GlobalClass]
public partial class MovementComponent : Node2D
{
	[Export] AdjacentRayComponent adjacentRayComponent;
	[Export] bool movable = true;
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
		if (movable && CanMove(direction))
		GetParent<Node2D>().GlobalPosition = GlobalPosition + (direction * Global.pixelGridSize);
	}

	public bool CanMove(Vector2 direction)
	{
		return adjacentRayComponent.GetAdjacentNode(direction) == null;
	}
}
