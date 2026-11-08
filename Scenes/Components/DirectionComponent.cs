using Godot;
using System;

public partial class DirectionComponent : Component
{
	public Action<Vector2>BrodcastDirectionChanged;
	private Vector2 currentDirection = Vector2.Down; 

	private InputManager inputManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Callable.From(_LateReady).CallDeferred();
	}

	void _LateReady (){
		inputManager = GetNode<InputManager>("/root/InputManager");

		inputManager.brodcastInputs += SetDirection;
	}

    public override void _ExitTree()
    {
		inputManager.brodcastInputs -= SetDirection;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetDirection(Vector2 newDirection)
	{
		if (newDirection != Vector2.Zero &&
			newDirection != currentDirection)
		{
			currentDirection = newDirection;
			BrodcastDirectionChanged?.Invoke(newDirection);

			GD.Print("directionComponent Direction: " + DirectionToString(currentDirection));
		}
	}

	public Vector2 GetDirection()
	{
		return currentDirection;
	}

	public string DirectionToString(Vector2 direction)
	{
		if (direction == Vector2.Up)
		{
			return "Up";
		}
		else if (direction == Vector2.Right)
		{
			return "Right";
		}
		else if (direction == Vector2.Down)
		{
			return "Down";
		}
		else if (direction == Vector2.Left)
		{
			return "Left";
		}
		else
		{
			return "";
		}
	}
}
