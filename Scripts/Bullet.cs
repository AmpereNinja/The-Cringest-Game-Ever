using Godot;

public class Bullet : RigidBody2D
{
	private float projectile_speed = 10000f;
	private Vector2 offset;
	private Vector2 offset2;
	private Timer timer;
	public override void _Ready()
	{
		//timer = GetNode<Timer>("timer");
		offset2.x = projectile_speed;
		offset2.y = 0;
		ApplyImpulse(offset2, offset2.Rotated(Rotation));
		//timer.Start();
		//SelfDestruct();

	}

	private void SelfDestruct()
	{
		QueueFree();
	}
	private void OnCollision(Object body)
	{
		Hide();
	}
}
