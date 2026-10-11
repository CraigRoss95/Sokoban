using Godot;
using System;
using System.Linq;

public partial class ButtonStateController : Node2D
{
	private GameObject parent;
	public bool pressed = false;

	public Action brodcastButtonToggled;

	[Export] public Area2D collider;
	private SpriteManagerComponent spriteManagerComponent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<GameObject>();
		Callable.From(_LateReady).CallDeferred();
	}
	public void _LateReady()
	{	
		spriteManagerComponent = parent.GetComponent<SpriteManagerComponent>();
		collider.AreaEntered += PressButton;
		collider.AreaExited += UnpressButtonCoyoteTime;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
		collider.AreaEntered -= PressButton;
		collider.AreaExited -= UnpressButtonCoyoteTime;

    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void UnpressButtonCoyoteTime(Node body)
	{
		await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);
		UnpressButtonCheck();
	}

	private void UnpressButtonCheck()
	{
		if (collider.GetOverlappingAreas().Count == 0)
		{
			UnPressButton();
		}
	}
	private void PressButton(Node body)
	{
		if (pressed == false)
		{
			pressed = true;
			spriteManagerComponent.PlayAnimation("press");
		    brodcastButtonToggled?.Invoke();
		}
	}

	private void UnPressButton()
	{
		if (pressed == true)
		{
			pressed = false;
		spriteManagerComponent.PlayAnimation("unpress");
		brodcastButtonToggled?.Invoke();
		}

	}
}
