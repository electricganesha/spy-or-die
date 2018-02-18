using System;
using UnityEngine;

public class player : MonoBehaviour
{

	public Rigidbody rb;
	public float initialAccel;
	private float accel;
	public float slowDown;
	private Vector3 dirV;
	private Vector3 lastDirV;
	public Transform directionVector;

	public GameObject disco;
	public bool temDisco;
	public int playerNumber;
	public String team;

	private float timer;
	public float cooldown;
	private bool colidiu;
	private bool isSlow;

	public GameObject sprite;
	private Animator spriteAnimator;

	private bool hasStarted = false;

	private KeyCode up, down, left, right, fire;
	private String upJoy, downJoy, leftJoy, rightJoy, fireJoy;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		accel = initialAccel;

		spriteAnimator = sprite.GetComponent<Animator> ();
		
		//disco.Owned(gameObject);
		
		switch(playerNumber)
		{
			case(1):
				up = KeyCode.W;
				down = KeyCode.S;
				left = KeyCode.A;
				right = KeyCode.D;
				fire = KeyCode.Space;
			break;
			case(2):
				up = KeyCode.UpArrow;
				down = KeyCode.DownArrow;
				left = KeyCode.LeftArrow;
				right = KeyCode.RightArrow;
				fire = KeyCode.RightControl;
			break;
			case(3):
				upJoy = "Vertical_1";
				downJoy = "Vertical_1";
				leftJoy = "Horizontal_1";
				rightJoy = "Horizontal_1";
				fireJoy = "Fire_1";
				break;
			case(4):
				upJoy = "Vertical_2";
				downJoy = "Vertical_2";
				leftJoy = "Horizontal_2";
				rightJoy = "Horizontal_2";
				fireJoy = "Fire_2";
				break;
		}

		hasStarted = true;
	}


// Update is called once per frame
	void Update()
	{


		if(colidiu)
		{
			timer += Time.deltaTime;
			
			if(timer > cooldown)
			{
				colidiu = false;
				accel = initialAccel;
				timer = 0.0f;
			}
				
		}
		
		dirV = rb.velocity;

		if (dirV.magnitude * 100 < 1)
			dirV = lastDirV;
		
		if(playerNumber == 1 || playerNumber == 2)
		{
			if (Input.GetKey(up))
			{
				rb.AddForce(transform.forward * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);

				if(!temDisco)
					spriteAnimator.SetTrigger ("walkingUp");
				else
					spriteAnimator.SetTrigger ("walkingUpBC");
			}
	
			if (Input.GetKey(left))
			{
				rb.AddForce(-transform.right * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);

				if(!temDisco)
					spriteAnimator.SetTrigger ("walkingLeft");
				else
					spriteAnimator.SetTrigger ("walkingLeftBC");
			}
	
			if (Input.GetKey(down))
			{
				rb.AddForce(-transform.forward * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);

				if(!temDisco)
					spriteAnimator.SetTrigger ("walkingDown");
				else
					spriteAnimator.SetTrigger ("walkingDownBC");
				
			}
	
			if (Input.GetKey(right))
			{
				rb.AddForce(transform.right * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);

				if(!temDisco)
					spriteAnimator.SetTrigger ("walkingRight");
				else
					spriteAnimator.SetTrigger ("walkingRightBC");
			}
			
			if (!Input.GetKey(right) || !Input.GetKey(down) || !Input.GetKey(left) || !Input.GetKey(up)) {
				directionVector.transform.LookAt(lastDirV* 100);

				if (!temDisco && hasStarted) 
					spriteAnimator.SetTrigger ("goIdle");
				else if (temDisco && hasStarted)
					spriteAnimator.SetTrigger ("goIdleBC");
			}
	
			if (Input.GetKeyDown(fire))
			{
				if(temDisco)
				{
					disc novoDisco = Instantiate(disco).GetComponent<disc>();
					novoDisco.transform.position = transform.position + dirV.normalized;
					
					if (dirV.magnitude != 0)
					{
						novoDisco.Shoot(dirV.normalized);
					} else 
					{
						novoDisco.Shoot(lastDirV.normalized);	
					}	
					temDisco = false;
				}	
			}
		}
		else if(playerNumber == 3 || playerNumber == 4)
		{			
			if (Input.GetAxis(upJoy) > 0.5f)
			{
				rb.AddForce(transform.forward * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);
	
			}
	
			if (Input.GetAxis(leftJoy) <  -0.5f)
			{
				rb.AddForce(-transform.right * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);
			}
	
			if (Input.GetAxis(downJoy) < -0.5f)
			{
				rb.AddForce(-transform.forward * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);
			}
	
			if (Input.GetAxis(rightJoy) > 0.5f)
			{
				rb.AddForce(transform.right * accel);
				lastDirV = dirV;
				directionVector.transform.LookAt(dirV* 100);
			}
			
			if (Input.GetAxis(rightJoy) == 0 && Input.GetAxis(downJoy) == 0 && Input.GetAxis(leftJoy) == 0 && Input.GetAxis(upJoy) == 0) {
				directionVector.transform.LookAt(lastDirV* 100);
			}
	
			if (Input.GetButton(fireJoy))
			{
				if(temDisco)
				{
					disc novoDisco = Instantiate(disco).GetComponent<disc>();
					novoDisco.transform.position = transform.position + dirV.normalized;
					
					if (dirV.magnitude != 0)
					{
						novoDisco.Shoot(dirV.normalized);
					} else 
					{
						novoDisco.Shoot(lastDirV.normalized);	
					}	
					temDisco = false;
				}	
			}
		}

		if (temDisco && hasStarted)
			spriteAnimator.SetBool ("hasBC",true);
		else
			spriteAnimator.SetBool ("hasBC", false);

		if(temDisco && !isSlow)
		{
			accel = accel / slowDown;
			isSlow = true;
		}
		if(!temDisco && !colidiu)
		{
			isSlow = false;
			accel = initialAccel;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Disc")
		{
			temDisco = true;
			Destroy(other.gameObject);
		}
		
		if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<player>().team != team && other.gameObject.GetComponent<player>().TemDisco() && !colidiu)
		{
			other.gameObject.GetComponent<player>().Steal();
			temDisco = true;
		}
	}

	private void OnTriggerStay(Collider target)
	{
		if (temDisco && target.gameObject.GetComponent<PointArea>().team == team)
		{
			target.gameObject.GetComponent<PointArea>().transmited = true;
		}
	}


	public bool TemDisco()
	{
		return temDisco;
	}
	
	public void Steal()
	{
		colidiu = true;
		accel = accel / 10;
		temDisco = false;
	}
}