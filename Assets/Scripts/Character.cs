using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
	
	float dirX, dirY;
	public int maxHealth;
	public int currentHealth;
	public int gold;
	public bool Walking;
	public bool Attacking;
	private bool isDodge;
	private static bool playerExists;
	float nextDodgeTime = 0f;
	public float dodgeRate = 2f;
	public GameObject hitEffect;
	public HealthBar healthBar;
	public Animator anim;
	public AudioSource walking;
	public AudioSource healthSound;
	
    
    public AudioSource coinsound;
	private Rigidbody2D rb;

	[SerializeField]
	float moveSpeed;

	void Start () {
		
		/*if (!playerExists)
		{
			playerExists = true;
			DontDestroyOnLoad(transform.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}*/
		
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		anim = GetComponent<Animator>();
		anim.speed = 1;
		rb = GetComponent<Rigidbody2D>();
	}	
	void Update () {
		Move ();	
		float lastInputX = CrossPlatformInputManager.GetAxis("Horizontal");
		float lastInputY = CrossPlatformInputManager.GetAxis("Vertical");

		if (lastInputX != 0 || lastInputY != 0)
		{
			anim.SetBool("Walking", true);
			anim.SetFloat("LastMoveX", lastInputX);
			anim.SetFloat("LastMoveY", lastInputY);
			anim.Play("Walk");
			walking.enabled = true;
			walking.loop = true;
		}
		else if (CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			anim.SetBool("Attacking", true);
			StartCoroutine(Attack());
			anim.Play("Attack");
		}
		else 
		{
			anim.SetBool("Walking", false);
			walking.enabled = false;
			walking.loop = false;
		}

		float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
		float inputY = CrossPlatformInputManager.GetAxis("Vertical");

		anim.SetFloat("SpeedX", inputX);
		anim.SetFloat("SpeedY", inputY);

		//if (Input.GetKeyDown(KeyCode.Space)) //only for playtesting
		if (CrossPlatformInputManager.GetButtonDown("Fire3"))
		{
			isDodge = true;
			nextDodgeTime = Time.time + 1f / dodgeRate;
			anim.Play("Dash");
		}
		/*if (Time.time > nextDodgeTime)
		{
		
			//if (CrossPlatformInputManager.GetButtonDown("Fire2"))
			if (Input.GetKeyDown(KeyCode.Space))
		{
			isDodge = true;
				nextDodgeTime = Time.time + 1f / dodgeRate;
		}		

		}*/
	}
	void FixedUpdate()
	{
		//WalkingSound();
		if (isDodge)
		{
			moveSpeed *= 10;
			isDodge = false;
		}
		else
		{
			moveSpeed = 0.7f;
		}
	}

	void Move()
	{
		dirX = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis ("Horizontal"));
		dirY = Mathf.RoundToInt(CrossPlatformInputManager.GetAxis ("Vertical"));

		rb.MovePosition(new Vector2((transform.position.x + dirX * moveSpeed * Time.deltaTime),
			transform.position.y + dirY * moveSpeed * Time.deltaTime));
		//transform.position = new Vector2 (dirX * moveSpeed * Time.deltaTime + transform.position.x,
			//dirY * moveSpeed * Time.deltaTime + transform.position.y);
	}
	public void TakeDamage(int damage)
	{
		Instantiate(hitEffect, transform.position, Quaternion.identity);
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
			Debug.Log("Dead");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			Debug.Log("Hit coin");
			ScoreTextScript.coinAmount += 1;
			Destroy(other.gameObject);
            coinsound.Play();
		}
		if (other.gameObject.CompareTag("HealthPickup"))
		{
			currentHealth = maxHealth;
			healthBar.SetMaxHealth(maxHealth);
			Destroy(other.gameObject);
			healthSound.Play();
		}


	}
	public IEnumerator Attack()
	{
		yield return new WaitForSeconds(0.5f);
		Debug.Log("done attacking");
		anim.SetBool("Attacking", false);
        
	}
	/*public void WalkingSound()
    {
		if (lastInputX != 0 || lastInputY != 0)
        {
			walking.enabled = true;
			walking.loop = true;
        }
		if (lastInputX = 0 || lastInputY = 0)
        {
			walking.enabled = false;
			walking.loop = false;
		}



	}*/
}
