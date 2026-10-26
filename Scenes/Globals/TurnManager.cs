using Godot;
using System;
using System.Collections.Generic;

public partial class TurnManager : Node

{
	public static bool isTurnHappening = false;
    float turnLength= 0.15f;
    Action brodcastTurnStarted;
    Action brodcastTurnEnded;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void StartTurn()
    {
        GD.Print("turn start");
        isTurnHappening = true;
        GetTree().CreateTween().TweenCallback(Callable.From(StopTurn)).SetDelay(turnLength);

        brodcastTurnStarted?.Invoke();
    }

    public void StopTurn()
    {
        GD.Print("turn end");
        isTurnHappening = false;
        brodcastTurnEnded?.Invoke();
    }
}