using Godot;
using System;

[Tool]
[GlobalClass]
public partial class GridObjectComponent : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SnapToGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SnapToGrid()
	{
		//TODO Implement
	}
}
