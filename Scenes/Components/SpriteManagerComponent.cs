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

	[Export] public bool isBackgroundObject = false;
	
	MovementComponent movementComponent;
	FiniteStateMachineComponent fsmComponent;
	PlayerControllerComponent playerControllerComponent;
	DirectionComponent directionComponent;

	public override void _Ready()
	{
		parent = GetParent<GameObject>();
		GetSiblingSprite();
		Callable.From(_LateReady).CallDeferred();
		if (isBackgroundObject)
		{
			sprite.ZIndex = -100;
		}

	}
	public void _LateReady()
	{

		fsmComponent = parent.GetComponent<FiniteStateMachineComponent>();
		if (fsmComponent != null)
		{
			fsmComponent.brodcastStateChange += ListenForStateChange;
		}

		directionComponent = parent.GetComponent<DirectionComponent>();
		if (directionComponent != null)
		{
			directionComponent.BrodcastDirectionChanged += ListenForDirectionChange;
		}
	}

    public override void _ExitTree()
    {
        base._ExitTree();
		fsmComponent = parent.GetComponent<FiniteStateMachineComponent>();
		if (fsmComponent == null)
		{
			fsmComponent.brodcastStateChange -= ListenForStateChange;
		}
    }


	public void updateZIndex(int index)
	{
		if (sprite != null
		&& isBackgroundObject == false)
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



	private void PlayAnimationStateAndDirection(string stateString)
	{
		string animationName;
		if (directionComponent != null)
		{
			animationName = stateString + directionComponent.GetDirectionAsString();
		}
		else {
			animationName = stateString;
		}

		PlayAnimation(animationName);
	}

	public void PlayAnimation (string animationName)
	{
		if (sprite.SpriteFrames.GetAnimationNames().Contains(animationName))
		{
			GD.Print("playing Animation " + animationName);
			sprite.Play(animationName);
		}
		else
		{
			//GD.Print("failed to find " + animationName  + " (" + this.GetParent<Node>().Name + ")");
		}
	}

	void ListenForDirectionChange(Vector2 direction)
	{
		PlayAnimationStateAndDirection(fsmComponent.currentState.stateName);
	}

		void ListenForStateChange(string stateName)
	{
		PlayAnimationStateAndDirection (stateName);
	}
}
