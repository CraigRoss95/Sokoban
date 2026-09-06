using Godot;


[Tool]
[GlobalClass]
public partial class MovementComponent : Node2D
{
	[Export] AdjacentRayComponent adjacentRayComponent;
	[Export] bool movable = true;
	[Export] double moveTime = 0.1;

	private bool moving = false;
	Vector2 bufferedMoveDirection = new Vector2();

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
				
		if (!moving && bufferedMoveDirection != new Vector2())
		{	Tween tween = CreateTween();
			tween.TweenProperty (GetParent<Node2D>(), "position", (GlobalPosition + bufferedMoveDirection * Global.pixelGridSize), moveTime);
			
			bufferedMoveDirection = new Vector2();
			moving = true;
			GetTree().CreateTween().TweenCallback(Callable.From(DoneMoving)).SetDelay(moveTime);
		}	

	}

	private async void DoneMoving()
	{
		moving = false;
	}

	public void Move(Vector2 direction)
	{
		if (movable && CanMove(direction))
		{
			bufferedMoveDirection = direction;
		}
	}


	public bool CanMove(Vector2 direction)
	{
		return ( !moving &&
			adjacentRayComponent.GetAdjacentNode(direction) == null);
	}
}
