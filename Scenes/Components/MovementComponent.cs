using Godot;


[Tool]
[GlobalClass]
public partial class MovementComponent : Node2D

{
	[Export] public RayCast2D rayCastUp;
	[Export] public RayCast2D rayCastRight;
	[Export] public RayCast2D rayCastDown;
	[Export] public RayCast2D rayCastLeft;

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
		RayCast2D ray = GetRay(direction);
		if (ray.IsColliding())
		{
			Node collision = (Node)ray.GetCollider();
			GD.Print("impact " + GetParent().Name + collision.GetParent().Name);
			return false;
		}
		
		//TODO use colliers and raycasts to figure this out (allways true for now)
		return true;
	}
	
	private RayCast2D GetRay (Vector2 direction)
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
