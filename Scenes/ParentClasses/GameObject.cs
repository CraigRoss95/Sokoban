using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
	// TODO Make this Genaric
	public T GetComponent<T>()  where T : Component
	{
		foreach (Component component in components) 
		{
			if (component.GetType() == typeof(T))
			{
				return (T)component;
			}
		}
		GD.PushError("Failed to get Component type: " + typeof(T));
		return null;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
