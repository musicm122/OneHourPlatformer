using Godot;
using System;

public class Win : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    void OnWinBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "PlayerController")
        {
            var player = (PlayerController)body;
            player.Win();
            
        }

    }

}


