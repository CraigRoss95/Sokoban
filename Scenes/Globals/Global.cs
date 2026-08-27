using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node

{
	// Allows for isometirc camara angel (not reall isometric angel but can't find the right word for "at an angel and not top down")
	public static Vector2 pixelGridSize = new Vector2 (16,16);
	public static List<Vector2> directionList = new List<Vector2> {Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
