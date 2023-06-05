using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		PlayerHealth playerHealth = GetComponent<PlayerHealth>();
		playerHealth.damageSuccess += PlayHurtSound;
	}

	private void PlayHurtSound(object sender, EventArgs e)
	{
		source.clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
		source.Play();
	}
}
