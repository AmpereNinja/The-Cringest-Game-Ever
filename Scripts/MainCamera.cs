using Godot;
using System;

public class MainCamera : Camera2D
{

    private KinematicBody2D Player;

    public override void _Ready()
    {
        Player = GetParent().GetNode<KinematicBody2D>("Player");
    }

    public override void _Process(float delta)
    {
        Position = Player.Position;
    }
}
