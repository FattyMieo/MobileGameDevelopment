using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SoldierScript : MonoBehaviour
{
	// Developer
	protected CapsuleCollider col;
	protected Rigidbody rb;
	protected Animator anim;

	[Header("Stats")]
	public int health;
	public int maxHealth;
	public float speed;
	public float jumpForce;
	public bool isRunning;
	public bool isShooting;

	[Header("Weapon Setup")]
	public AudioSource gunShotSource;
	public AudioClip gunShotSFX;
	public float gunShotDuration;
	protected float gunShotTimer;
	public ParticleSystem muzzleFlash;
	public ParticleSystem cartridgeEject;
	public ParticleSystem cartridgePuff;

	[Header("Weapon Stats")]
	public int damage;
	public int loadedAmmo;
	public int maxLoadedAmmo;
	public int totalAmmo;
	public bool infiniteAmmo;

	// Use this for initialization
	public virtual void Start ()
	{
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		health = maxHealth;
		ReloadAmmo();
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		UpdateAnimator();
		UpdateWeapon();
	}

	public virtual void UpdateAnimator()
	{
		anim.SetBool("IsRunning", isRunning);
		anim.SetBool("IsShooting", isShooting);

		if(isRunning)
			anim.speed = Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Horizontal"), 2) + Mathf.Pow(Input.GetAxis("Vertical"), 2)), 0.1f, 1.0f);
		else
			anim.speed = 1.0f;
	}

	public virtual void UpdateWeapon()
	{
		if(isShooting)
		{
			if(!muzzleFlash.isEmitting) muzzleFlash.Play(true);

			if(gunShotTimer >= gunShotDuration)
			{
				gunShotTimer = 0.0f;
			}

			if(gunShotTimer == 0.0f)
			{
				cartridgeEject.Emit(1);
				cartridgePuff.Emit(1);
				gunShotSource.PlayOneShot(gunShotSFX);
				ReduceAmmo(1);
				ShootBullet();
			}

			gunShotTimer += Time.deltaTime;
		}
		else
		{
			if(muzzleFlash.isPlaying) muzzleFlash.Stop(true);

			gunShotTimer = 0.0f;
		}
	}

	abstract public void ShootBullet();

	public virtual void ReduceAmmo(int count)
	{
		while(count > 0)
		{
			if(loadedAmmo <= 0)
			{
				break;
			}
			loadedAmmo--;
			count--;
		}
	}

	public virtual void ReloadAmmo()
	{
		if(infiniteAmmo)
		{
			loadedAmmo = maxLoadedAmmo;
			return;
		}

		if(totalAmmo < (maxLoadedAmmo - loadedAmmo))
		{
			int givenAmmo = totalAmmo;
			totalAmmo = 0;
			loadedAmmo += givenAmmo;
		}
		else
		{
			totalAmmo -= (maxLoadedAmmo - loadedAmmo);
			loadedAmmo = maxLoadedAmmo;
		}
	}

	public bool isDead
	{
		get {return health <= 0;}
	}

	public virtual void TakeDamage(int damage)
	{
		if(!isDead)
		{
			health -= damage;
			if(isDead) health = 0;
			Debug.Log(gameObject.name + " is hit by " + damage + " Damage, remaining HP: " + health);
		}
		else
		{
			Debug.Log(gameObject.name + " is dead");
		}
	}
}
