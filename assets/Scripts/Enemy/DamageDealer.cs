using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageDealer : MonoBehaviour
{
	public static EventHandler<OnDealDamageArgs> onDealDamage;

	public class OnDealDamageArgs : EventArgs
	{
		public int damage;
		
		public OnDealDamageArgs(int damage)
		{
			this.damage = damage;
		}
	}
}
