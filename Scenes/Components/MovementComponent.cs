using System.Linq;
using Godot;


[GlobalClass]
public partial class MovementComponent : Component
{
	GameObject parent;
	public AdjacentRayComponent adjacentRayComponent;
	[Export] bool movable = true;
	public SpriteManagerComponent spriteManagerComponent;
	private double moveTime = 0.1;
	public bool moving = false;
	Vector2 bufferedMoveDirection = new Vector2();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<GameObject>();
		Callable.From(_LateReady).CallDeferred();
	}
	public void _LateReady()
	{
		adjacentRayComponent = parent.components.OfType<AdjacentRayComponent>().FirstOrDefault();
		spriteManagerComponent = parent.components.OfType<SpriteManagerComponent>().FirstOrDefault();
		
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
		if (moving)
		{
			spriteManagerComponent.updateZIndex((int)GlobalPosition.Y);

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

	public void ForceMove(Vector2 direction)
	{
		bufferedMoveDirection = direction;
	}


	public bool CanMove(Vector2 direction)
	{
		return ( !moving &&
			adjacentRayComponent.GetAdjacentNode(direction) == null);
	}
}
