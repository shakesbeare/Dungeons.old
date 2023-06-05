using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public float invincibleTime;
    private int currentHealth;
    private Queue<int> damageInstanceQueue = new Queue<int>();
    private float lastHit;

    public EventHandler onPlayerDeath;
    public EventHandler<OnUpdateHealthArgs> onUpdateHealth;
    public EventHandler damageSuccess;
    public bool isDead;

    public class OnUpdateHealthArgs : EventArgs
    {
        public int newHealth;
        public float healthPercent;

        public OnUpdateHealthArgs(int health, float healthPercent)
        {
            this.newHealth = health;
            this.healthPercent = healthPercent;
        }

    }

    void Awake()
    {
        currentHealth = maxHealth;
        onUpdateHealth?.Invoke(this, new OnUpdateHealthArgs(currentHealth, 1.0f));
        isDead = false;
    }

    void Start()
    {
        lastHit = Time.time;
        DamageDealer.onDealDamage += OnDamageTaken;
    }

	private void Update()
	{
        if (damageInstanceQueue.Count != 0)
		{
            ApplyHealthChange(damageInstanceQueue.Dequeue());
        }
    }

    void ApplyHealthChange(int amount)
	{
        // Update player health 
        currentHealth -= amount;

        if (currentHealth > maxHealth)
		{
            currentHealth = maxHealth;
            damageSuccess?.Invoke(this, EventArgs.Empty);
        }

        float healthPercent = (float)currentHealth / (float)maxHealth;
        onUpdateHealth?.Invoke(this, new OnUpdateHealthArgs(currentHealth, healthPercent));

        // Check for player death
        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke(this, EventArgs.Empty); // Broadcast a death message
            Destroy(gameObject);
        }
    }

    void OnDamageTaken(object sender, DamageDealer.OnDealDamageArgs args)
    {
        if (Time.time >= lastHit + invincibleTime)
		{
            lastHit = Time.time;
            damageInstanceQueue.Enqueue(args.damage);
		}
    }
}
