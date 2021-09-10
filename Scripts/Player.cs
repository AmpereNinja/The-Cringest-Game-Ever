using Godot;
using System;

public class Player : KinematicBody2D
{
	private float speed = 2000f;
	private float gravity = 20f;
	private Vector2 movement;
	private Vector2 up_dir = Vector2.Up;
	private float jumpForce = -2000f;
	public override void _PhysicsProcess(float delta)
	{
		PlayerMovement();
	}
	void PlayerMovement()
	{
		movement.y += gravity;
		if (Input.IsActionPressed("move_right"))
		{
			movement.x = speed;
		} else if (Input.IsActionPressed("move_left"))
		{
			movement.x = -speed;
		} 
		else
		{
			movement.x = 0f;
		}
		if (IsOnFloor())
		{
			if (Input.IsActionPressed("jump"))
			{
				movement.y = jumpForce;
			}
		}
		MoveAndSlide(movement, up_dir);
	}
}
