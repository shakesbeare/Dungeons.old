using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : DamageDealer
{
	private Vector2 targetPoint;
	private float maxLifespan;
	private float movementSpeed;
	private Vector2 targetDir;
	private float lifeTimer;
	private int damage;

	public void Setup(Vector2 targetPoint, float maxLifespan, float movementSpeed, int damage)
	{
		this.targetPoint = targetPoint;
		this.maxLifespan = maxLifespan;
		this.movementSpeed = movementSpeed;
		this.damage = damage;
		targetDir = (targetPoint - new Vector2(transform.position.x, transform.position.y)).normalized;
		lifeTimer = maxLifespan;
	}

	private void Update()
	{
		transform.Translate(targetDir * movementSpeed * Time.deltaTime);
		lifeTimer -= Time.deltaTime;

		if (lifeTimer <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			onDealDamage?.Invoke(this, new OnDealDamageArgs(damage));
			Destroy(gameObject);
		}
		else if (collision.collider.tag == "Ground")
		{
			Destroy(gameObject);
		}
	}
}
