using System;
using System.ComponentModel;
using System.Linq;
using Godot;

[GlobalClass]
public partial class InputControllerComponent : Component
{
	public PlayerControllerComponent playerControllerComponent;

	[Export] double moveSpeedBufferMax = 0.2; 

	private Vector2 currentDirectionalInput = new Vector2 ();
	private double currentMoveBufferWait = 0.0;

	private GameObject parent;
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		parent = GetParent<GameObject>();
		Callable.From(_LateReady).CallDeferred();
	}
	public void _LateReady()
	{	
		playerControllerComponent = parent.GetComponent<PlayerControllerComponent>();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		GetInputs(@event);

    }

    public override void _Process(double delta)
    {
        base._Process(delta);
		UseBufferedInputs(delta);
    }

	public void GetInputs(InputEvent @event)
	{
		if (
		   (@event.IsActionPressed("up")
		|| @event.IsActionPressed("right")
		|| @event.IsActionPressed("down")
		|| @event.IsActionPressed("left"))
		)
		{
			if(@event.IsActionPressed("up"))
			{
				currentDirectionalInput = Vector2.Up;
			}
			else if(@event.IsActionPressed("right"))
			{
				currentDirectionalInput = Vector2.Right;
			}
			else if(@event.IsActionPressed("down"))
			{
				currentDirectionalInput = Vector2.Down;
			}
			else if(@event.IsActionPressed("left"))
			{
				currentDirectionalInput = Vector2.Left;
			}
			
		}
		else if (Global.directionList.Contains(Input.GetVector("left","right","up","down")))
		{
			currentDirectionalInput = Input.GetVector("left","right","up","down");
		}
		else if (Input.GetVector("left","right","up","down") == new Vector2())
		{
			currentDirectionalInput = new Vector2();
		}
		
	}

	public void UseBufferedInputs(double delta)
	{
		currentMoveBufferWait += delta ;
		if (currentDirectionalInput != new Vector2()
		&& moveSpeedBufferMax <= currentMoveBufferWait)
		{
			currentMoveBufferWait = 0.0;
			//TODO make this use a signal
			playerControllerComponent.UseDirectionalInput(currentDirectionalInput);
		}
	}

}
