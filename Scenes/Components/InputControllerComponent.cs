using Godot;

[Tool]
[GlobalClass]
public partial class InputControllerComponent : Node2D
{
	[Export] PlayerControllerComponent playerControllerComponent;

	[Export] double moveSpeedBufferMax = 0.3; 

	private Vector2 currentDirectionalInput = new Vector2 ();
	private double currentMoveBufferWait = 0.0;
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
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
		&& Global.directionList.Contains(Input.GetVector("left","right","up","down"))
		)
		{
			currentDirectionalInput = Input.GetVector("left","right","up","down");
		}

		if ((@event.IsActionReleased("up") && currentDirectionalInput == Vector2.Up)
		|| (@event.IsActionReleased("right")&& currentDirectionalInput == Vector2.Right)
		|| (@event.IsActionReleased("down") && currentDirectionalInput == Vector2.Down)
		|| (@event.IsActionReleased("left") && currentDirectionalInput == Vector2.Left))
		{
			currentDirectionalInput = new Vector2();
		}	

		if(currentDirectionalInput == new Vector2()
		&& Input.GetVector("left","right","up","down") != new Vector2()
		&& Global.directionList.Contains(Input.GetVector("left","right","up","down"))) 
		{
			currentDirectionalInput = Input.GetVector("left","right","up","down");
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
