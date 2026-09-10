using Godot;
using Godot.Collections;
using System;
using System.Collections.ObjectModel;
using System.Linq;


[GlobalClass]
public partial class SpriteManagerComponent : Component
{
	Sprite2D sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetSiblingSprite();
	}

	public void updateZIndex(int index)
	{
		sprite.ZIndex = index;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GetSiblingSprite()
	{
		Array<Node> children = GetParent<Node2D>().GetChildren();

		if(children.OfType<Sprite2D>().FirstOrDefault() == null)
		{
			GD.PushError("Failed to find sprite for parent: " + GetParent().Name);
			return;
		}
		sprite = children.OfType<Sprite2D>().FirstOrDefault();
		sprite.ZIndex = (int)GetParent<Node2D>().GlobalPosition.Y;
	}
}
