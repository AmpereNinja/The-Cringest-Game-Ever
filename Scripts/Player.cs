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
	private Particles2D particles;
	private AudioStreamPlayer shooting;
	private PackedScene bullet;
	private bool can_fire = true;
	private float rateOfFire = 0.4f;
	private Timer timer;
	public override void _Ready() {
		bullet = (PackedScene)ResourceLoader.Load("res://Scenes/Bullet.tscn");
		pistol =  GetNode<Node2D>("Pistol");
		raycast = pistol.GetNode<RayCast2D>("RayCast2D");
		sprite = pistol.GetNode<Sprite>("Sprite");
		particles = pistol.GetNode<Particles2D>("Particles");
		shooting = pistol.GetNode<AudioStreamPlayer>("ShootingAudio");
		timer = GetNode<Timer>("timer");
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
			movement.y = 0;
			if (Input.IsActionPressed("jump"))
			{
				movement.y = jumpForce;
			}
		}




		//For Shooting

		if (Input.IsActionPressed("shoot") && can_fire == true)
		{
			can_fire = false;
			particles.ProcessMaterial.Set("angle", GetAngleTo(GetGlobalMousePosition()));   //Set the particles angle to the mouse's global angle


			// Turn on the particles effect
			particles.Emitting = true;
			shooting.Play();
			bulletForce();
			setTimer();
		}
		//For movement
		MoveAndSlide(movement, up_dir);
		//allowing you to rotate the gun to your pointer's angle
		pistol.Rotation = GetAngleTo(GetGlobalMousePosition());
		//for debugging
		//GD.Print(pistol.Rotation);
	}
	void bulletForce()
	{
		var bulletInstance = bullet.Instance();

		RigidBody2D bulletRigid = (RigidBody2D)bulletInstance;

		bulletRigid.Position = GlobalPosition;
		bulletRigid.Rotation = GetAngleTo(GetGlobalMousePosition());
		GetParent().AddChild(bulletRigid);

	}


	void setTimer()
	{
		timer.WaitTime = rateOfFire;
		timer.Start();
	}
	public void setCanFireValue()
	{
		can_fire = true;
	}
}
