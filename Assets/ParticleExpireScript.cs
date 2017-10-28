using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExpireScript : MonoBehaviour
{
	private ParticleSystem ps;
//	private AudioSource source;
//	public AudioClip explosionSFX;

	void Start() 
	{
		ps = GetComponent<ParticleSystem>();
//		source = GetComponent<AudioSource>();
//		source.PlayOneShot(explosionSFX);
	}

	void Update() 
	{
		if(ps)
		{
			if(!ps.IsAlive())
			{
				Destroy(gameObject);
			}
		}
	}
}
