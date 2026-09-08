using Godot;
using System;
using System.Collections.Generic;

public partial class GameObject : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public List<Component> components = new List<Component>();
	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is Component)
			{
				Component childComponent = (Component)child;
				components.Add(childComponent);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
