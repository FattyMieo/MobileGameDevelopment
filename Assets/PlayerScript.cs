using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : SoldierScript
{
	[Header("Reload Mechanics")]
	public float reloadDuration;
	protected float reloadTimer;
	public bool isReloading;

	[Header("Blood FX")]
	public GameObject bloodSprayPrefab;

	[Header("Helix Rocket")]
	public GameObject rocketPrefab;
	public AudioClip rocketFireSFX;
	public float rocketDuration;
	protected float rocketTimer;
	public bool isRocketReady;

	[Header("Biotic Field")]
	public GameObject bioticPrefab;
	public AudioClip bioticDeploySFX;
	public float bioticDuration;
	protected float bioticTimer;
	public bool isBioticReady;

	[Header("Camera")]
	public bool isFPS;
	public GameObject camFPS;
	public GameObject camTPS;
	public float hTurnSpeed;
	public float vTurnSpeed;
	public float curVAngle = 0.0f;
	public float minVAngle;
	public float maxVAngle;
	public float camSnapSensitivity;

	//Buttons
	private bool btnAttack;
	private bool btnJump;
	private bool btnHelix;
	private bool btnBiotic;
	private bool btnReload;

	//Values
	[Header("UI Elements")]
	public Slider healthBar;
	public Text loadedAmmoText;
	public Text maxLoadedAmmoText;

	public override void Start()
	{
		base.Start();
		isRocketReady = true;
		isBioticReady = true;
	}

	public void FixedUpdate()
	{
		isRunning = Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f;

		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * Time.deltaTime * speed;
		transform.Translate(dir);

		if(Input.GetKeyDown(KeyCode.Space) || btnJump)
		{
			if(Physics.Raycast(transform.position, Vector3.down, 0.1f))
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		btnJump = false;
	}

	// Update is called once per frame
	public override void Update ()
	{
		if(Input.GetKeyDown(KeyCode.F)) isFPS = !isFPS;
		camFPS.SetActive(isFPS);
		camTPS.SetActive(!isFPS);

		isShooting = (/*Input.GetMouseButton(0) || */btnAttack) && loadedAmmo > 0;

		if(canReload && ((Input.GetKeyDown(KeyCode.R) || btnReload) || loadedAmmo <= 0))
		{
			isReloading = true;
			reloadTimer = 0.0f;
		}

		if(isReloading)
		{
			reloadTimer += Time.deltaTime;

			if(reloadTimer >= reloadDuration)
			{
				isReloading = false;
				reloadTimer = 0.0f;
				ReloadAmmo();
			}
		}

		if(Input.GetKey(KeyCode.Keypad4))
		{
			transform.Rotate(0, -Time.deltaTime * hTurnSpeed, 0);
		}
		if(Input.GetKey(KeyCode.Keypad6))
		{
			transform.Rotate(0, Time.deltaTime * hTurnSpeed, 0);
		}

		if(Input.GetKey(KeyCode.Keypad8))
		{
			if(curVAngle - Time.deltaTime * vTurnSpeed >= minVAngle)
			{
				curVAngle -= Time.deltaTime * vTurnSpeed;
				camFPS.transform.Rotate(-Time.deltaTime * vTurnSpeed, 0, 0);
				camTPS.transform.Rotate(-Time.deltaTime * vTurnSpeed, 0, 0);
			}
		}
		if(Input.GetKey(KeyCode.Keypad2))
		{
			if(curVAngle + Time.deltaTime * vTurnSpeed <= maxVAngle)
			{
				curVAngle += Time.deltaTime * vTurnSpeed;
				camFPS.transform.Rotate(Time.deltaTime * vTurnSpeed, 0, 0);
				camTPS.transform.Rotate(Time.deltaTime * vTurnSpeed, 0, 0);
			}
		}

		Ray ray = camFPS.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 500))
		{
//			camTPS.transform.LookAt(hit.point);
			camTPS.transform.rotation = Quaternion.Lerp(camTPS.transform.rotation, Quaternion.LookRotation(hit.point - camTPS.transform.position), Time.deltaTime * camSnapSensitivity);
			Debug.DrawLine(camFPS.transform.position, hit.point);
		}

		UpdateAnimator();
		UpdateWeapon();

		if(isRocketReady)
		{
			if(Input.GetMouseButtonDown(1) || btnHelix)
			{
				gunShotSource.PlayOneShot(rocketFireSFX);
				ShootHelixRocket();
				rocketTimer = rocketDuration;
				isRocketReady = false;
			}
		}
		else
		{
			rocketTimer -= Time.deltaTime;

			if(rocketTimer <= 0.0f)
			{
				rocketTimer = 0.0f;
				isRocketReady = true;
			}
		}

		if(isBioticReady)
		{
			if(Input.GetKeyDown(KeyCode.E) || btnBiotic)
			{
				gunShotSource.PlayOneShot(bioticDeploySFX);
				DeployBioticField();
				bioticTimer = bioticDuration;
				isBioticReady = false;
			}
		}
		else
		{
			bioticTimer -= Time.deltaTime;

			if(bioticTimer <= 0.0f)
			{
				bioticTimer = 0.0f;
				isBioticReady = true;
			}
		}

		//UI Button Update
		btnAttack = false;
		btnHelix = false;
		btnBiotic = false;
		btnReload = false;

		//UI Value Update
		healthBar.value = (float)health / (float)maxHealth;
		loadedAmmoText.text = loadedAmmo.ToString();
		maxLoadedAmmoText.text = maxLoadedAmmo.ToString();
	}

	public bool canReload
	{
		get
		{
			if(isReloading) return false;
			if(totalAmmo <= 0) return false;
			if(loadedAmmo >= maxLoadedAmmo) return false;
			return true;
		}
	}

	public override void ShootBullet()
	{
		Ray ray = camFPS.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 500))
		{
			if(hit.collider.tag == "Hitbox" && hit.collider.GetComponent<HitboxScript>())
			{
				HitboxScript enemy = hit.collider.GetComponent<HitboxScript>();
				enemy.TakeDamage(damage);
				Instantiate(bloodSprayPrefab, hit.point, Quaternion.LookRotation(hit.normal));
			}
		}
	}

	public void ShootHelixRocket()
	{
		Ray ray = camFPS.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 500))
		{
			Instantiate(rocketPrefab, muzzleFlash.transform.position, Quaternion.LookRotation(hit.point - muzzleFlash.transform.position));
		}
	}

	public void DeployBioticField()
	{
		Instantiate(bioticPrefab, transform.position, Quaternion.identity);
	}

	public void DoAttack()
	{
		btnAttack = true;
	}

	public void DoJump()
	{
		btnJump = true;
	}

	public void DoReload()
	{
		btnReload = true;
	}

	public void DoHelix()
	{
		btnHelix = true;
	}

	public void DoBiotic()
	{
		btnBiotic = true;
	}

	public void DoSwitchCam()
	{
		isFPS = !isFPS;
	}
}
