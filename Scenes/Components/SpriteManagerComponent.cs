using Godot;
using Godot.Collections;
using System;
using System.Collections.ObjectModel;
using System.Linq;


[GlobalClass]
public partial class SpriteManagerComponent : Component
{
	AnimatedSprite2D sprite;
	GameObject parent;
	// Called when the node enters the scene tree for the first time.

	
	MovementComponent movementComponent;
	FiniteStateMachineComponent fsmComponent;
	PlayerControllerComponent playerControllerComponent;

	string directionString = "";
	public override void _Ready()
	{
		parent = GetParent<GameObject>();
		GetSiblingSprite();
		Callable.From(_LateReady).CallDeferred();

	}
	public void _LateReady()
	{
		movementComponent = parent.GetComponent<MovementComponent>();
		if (movementComponent != null)
		{
			movementComponent.brodcastMove += ListenForDirectionAsString;
		}

		fsmComponent = parent.GetComponent<FiniteStateMachineComponent>();
		if (fsmComponent != null)
		{
			fsmComponent.brodcastStateChange += ListenForStateChange;
		}
		playerControllerComponent = parent.GetComponent<PlayerControllerComponent>();
		if (playerControllerComponent != null)
		{
			playerControllerComponent.brodcastPush += ListenForDirectionAsString;
		}

	}

    public override void _ExitTree()
    {
        base._ExitTree();
		if (fsmComponent != null)
		{
			fsmComponent.brodcastStateChange -= ListenForStateChange;
		}

    }


	public void updateZIndex(int index)
	{
		if (sprite != null)
		{
			sprite.ZIndex = index;

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GetSiblingSprite()
	{
		Array<Node> children = GetParent<Node2D>().GetChildren();

		if(children.OfType<AnimatedSprite2D>().FirstOrDefault() == null)
		{
			GD.PushError("Failed to find sprite for parent: " + GetParent().Name);
			return;
		}
		sprite = children.OfType<AnimatedSprite2D>().FirstOrDefault();
		sprite.ZIndex = (int)GetParent<Node2D>().GlobalPosition.Y;
	}

	void ListenForStateChange(string stateName)
	{
		string animationName = stateName + directionString;

		if (sprite.SpriteFrames.GetAnimationNames().Contains(animationName))
		{
			GD.Print("playing Animation " + animationName);
			sprite.Play(animationName);
		}
	}

	void ListenForDirectionAsString(Vector2 direction)
	{
		if (direction == Vector2.Up)
		{
			directionString = "Up";
		}
		else if (direction == Vector2.Right)
		{
			directionString = "Right";
		}
		else if (direction == Vector2.Down)
		{
			directionString = "Down";
		}
		else if (direction == Vector2.Left)
		{
			directionString = "Left";
		}
		else
		{
			directionString = "";
		}
	}
}
