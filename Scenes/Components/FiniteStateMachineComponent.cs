using Godot;
using Godot.Collections;
using System;
using System.ComponentModel;

public partial class FiniteStateMachineComponent : Component
{
	// Called when the node enters the scene tree for the first time.
	[Export] public string initialStateName = "IdleState";

	State currentState;
	Dictionary <string, State> stateDict;

	public override void _Ready()
	{
		GetStates();
		TransitionStateTo(initialStateName);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	void GetStates()
	{
		stateDict = new Dictionary<string, State> ();
		foreach (Node child in GetChildren())
		{
			if (child is State)
			{
				State childState = (State)child;
				stateDict[child.Name] = childState;
				childState.finiteStateMachine = this;

				childState.ReadyState();
				childState.ExitState();
			}
		}
	}

	public override void _Process (double delta)
	{
		currentState.UpdateState(delta);
	}

    public override void _PhysicsProcess(double delta)
    {
        currentState.PhysicsUpdateState(delta);
    }

	public override void _Input(InputEvent @event)
	{
		currentState.HandelInputState(@event);
	}

	public void TransitionStateTo(string key)
	{
		if (!stateDict.ContainsKey(key) || currentState == stateDict[key])
		{
			GD.PushWarning ("State" + key + "not found in FSM");
			return;
		}
		if (currentState == stateDict[key])
		{
			GD.Print("All ready in state " + "key");
		}
		else
		{
			currentState = stateDict[key];
		}
	}
}
