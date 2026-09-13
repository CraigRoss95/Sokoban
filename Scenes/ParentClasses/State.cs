using Godot;
using System;

public partial class State : Node
{
	public FiniteStateMachineComponent finiteStateMachine;
	public GameObject gameObject;
	// Called when the node enters the scene tree for the first time.
public virtual void EnterState() {}
public virtual void ExitState() {}

public virtual void ReadyState() {}
public virtual void UpdateState(double delta) {} 
public virtual void PhysicsUpdateState(double delta) {} 
public virtual void HandelInputState(InputEvent @event) {}
}
