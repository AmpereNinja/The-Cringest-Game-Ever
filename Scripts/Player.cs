using Godot;
using System;

public class Player : KinematicBody2D
{
	private float speed = 1000f;
	private float gravity = 20f;
	private Vector2 movement = Vector2.Zero;
	private Vector2 up_dir = Vector2.Up;
	private float jumpForce = -1000f;
	private RayCast2D raycast;
	private Node2D pistol;
	private Sprite sprite;
	public override void _Ready() {
		pistol =  GetNode<Node2D>("Pistol");
		raycast = pistol.GetNode<RayCast2D>("RayCast2D");
		sprite = pistol.GetNode<Sprite>("Sprite");
	}
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
			ChangeRayCast(true, true);
		} else if (Input.IsActionPressed("move_left"))
		{
			movement.x = -speed;
			ChangeRayCast(true, false);
		} 
		else
		{
			movement.x = 0f;
		}
		if (IsOnFloor())
		{
			movement.y = 0;
			if (Input.IsActionPressed("jump"))
			{
				movement.y = jumpForce;
			}
		}
		MoveAndSlide(movement, up_dir);
	}
	void ChangeRayCast(bool IsMoving, bool moveright) {
		if (IsMoving) {
			var rayposition = raycast.CastTo;
			if (moveright) {
				if (raycast.CastTo.y < 0) {
					rayposition.y *= -1;
				}
				raycast.CastTo = rayposition;
				sprite.FlipH = false;
			} else {
				if (raycast.CastTo.y > 0) {
					rayposition.y *= -1;
				}
				raycast.CastTo = rayposition;
				sprite.FlipH = true;
			}
		} 
		GD.Print(raycast.IsColliding());
	}
}
