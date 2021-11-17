using Godot;
using System;

public class Death : Area2D
{
    public override void _Ready()
    {

    }

    void OnDeathBodyEntered(PhysicsBody2D body)
    {
        if (body.Name == "PlayerController")
        {
            var player = (PlayerController)body;
            player.Die();
        }

    }
}




