using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zchoop : DamageDealer
{
	[SerializeField] private Transform projectilePrefab;
	[SerializeField] private float shootDelay;
	[SerializeField] private int damage;
	[SerializeField] private float projectileSpeed;
	[SerializeField] private float projectileLifetime;

	private Rigidbody2D selfBody;
	private Transform player;
	private Vector2 m_Velocity = Vector2.zero;
	private float shootTimer;

	private void Start()
	{
		selfBody = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	void Update()
	{
		ResistMotion();

		shootTimer -= Time.deltaTime;
		if (shootTimer <= 0)
		{
			FireProjectile();
		}
	}

	private void ResistMotion()
	{
		selfBody.velocity = Vector2.SmoothDamp(selfBody.velocity, Vector2.zero, ref m_Velocity, 0.2f);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			onDealDamage?.Invoke(this, new OnDealDamageArgs(2));
		}
	}

	private void FireProjectile()
	{
		shootTimer = shootDelay;
		Transform projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		BaseProjectile projectile = projectileObject.GetComponent<BaseProjectile>();
		projectile.Setup(new Vector2(player.position.x, player.position.y), projectileLifetime, projectileSpeed, damage);
	}
}
