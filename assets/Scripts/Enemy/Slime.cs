using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : DamageDealer
{
    [SerializeField] public float health;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float goldDrop;
    [SerializeField] private float jumpDelay = 2f;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform selfTransform;
    [SerializeField] private Animator selfAnimator;
    [SerializeField] private Rigidbody2D selfBody;

    private float lastJump;
    private Vector2 m_Velocity = Vector2.zero;
    void Start()
    {
        lastJump = Time.time;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Time.time - lastJump >= jumpDelay && playerTransform)
        {
            selfAnimator.SetTrigger("Jump");
            Vector2 moveDirection = playerTransform.position - selfTransform.position;
            moveDirection.Normalize();

            selfBody.AddForce(moveDirection * jumpForce, ForceMode2D.Impulse);
            lastJump = Time.time;
        }

        selfBody.velocity = Vector2.SmoothDamp(selfBody.velocity, Vector2.zero, ref m_Velocity, 0.6f);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
            onDealDamage?.Invoke(this, new OnDealDamageArgs(1));
		}
	}
}
