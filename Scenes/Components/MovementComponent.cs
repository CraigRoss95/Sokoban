using System;
using System.Linq;
using Godot;

[GlobalClass]
public partial class MovementComponent : Component
{
	GameObject parent;
	public AdjacentRayComponent adjacentRayComponent;
	public SpriteManagerComponent spriteManagerComponent;
	public bool moving = false;
	Vector2 bufferedMoveDirection = new Vector2();
	public Action <Vector2> brodcastMove;
	public Action brodcastDoneMoving;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<GameObject>();
		Callable.From(_LateReady).CallDeferred();
	}
	public void _LateReady()
	{
		adjacentRayComponent = parent.GetComponent<AdjacentRayComponent>();
		spriteManagerComponent = parent.GetComponent<SpriteManagerComponent>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		float moveTime = Global.stepTime;
		if (bufferedMoveDirection != new Vector2())
		{	Tween tween = CreateTween();
            // this is a different tween from the one running in the turn manager, this might cause problems...
			tween.TweenProperty (GetParent<Node2D>(), "position", (GlobalPosition + bufferedMoveDirection * Global.pixelGridSize), moveTime);
			bufferedMoveDirection = new Vector2();
			GetTree().CreateTween().TweenCallback(Callable.From(DoneMoving)).SetDelay(moveTime);
		}	

		spriteManagerComponent.updateZIndex((int)GlobalPosition.Y);		
	}

	private async void DoneMoving()
	{
		brodcastDoneMoving?.Invoke();
	}

	public void Move(Vector2 direction)
	{
		if (CanMove(direction))
		{
			bufferedMoveDirection = direction;
			brodcastMove?.Invoke(direction);
		}
	}

	public void ForceMove(Vector2 direction)
	{
		bufferedMoveDirection = direction;
	}

	public bool CanMove(Vector2 direction)
	{
		return (adjacentRayComponent.GetAdjacentNode(direction) == null);
	}
}