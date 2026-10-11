using Godot;
using System;

public partial class ButtonStateController : Node2D
{
	private GameObject parent;
	public bool pressed = false;

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
    }

    public override void _ExitTree()
    {
        base._ExitTree();
		collider.AreaEntered -= PressButton;
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void PressButton(Node body)
	{
		pressed = true;
		spriteManagerComponent.PlayAnimation("press");
		GD.Print("button pressed");

		//TODO when exiting the button transitions back to the unpressed state
		//TODO brodcast to swithc
	}
}
