using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour 
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	Rigidbody2D m_rigidBody;
	SpriteRenderer m_spriteRenderer;
	public int startLives = 3;
	public int startBombs = 3;
	int m_score = 0;
	int m_lives = 3;
	int m_bombs = 3;
	bool m_canBomb = false;
	public GameObject bombEffect;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		m_rigidBody = (Rigidbody2D)GetComponent<Rigidbody2D> ();
		m_spriteRenderer = (SpriteRenderer)GetComponent<SpriteRenderer> ();

		m_lives = startLives;
		m_bombs = startBombs;

		//Freeze player at start of game
		m_rigidBody.isKinematic = true;

		//Register events
		GetComponent<PlayerCollision>().playerExploded += RemoveLife;
		GetComponent<PlayerCollision>().bombCollected += AddBomb;
		GetComponent<PlayerControls>().enterPressed += DropBomb;
	}

	void Start()
	{
		//Register for HUD state event
		HUDState.instance.countdownEnded += EnablePlayer;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public void AddScore(int _score)
	{
		m_score += _score;
		HUDState.instance.SetScore (m_score);
	}

	void DisablePlayer()
	{
		//Stop player from moving or colliding
		m_spriteRenderer.enabled = false;
		m_rigidBody.isKinematic = true;
		m_canBomb = false;
		GetComponent<PlayerShooting>().canFire = false;
		GetComponent<PolygonCollider2D> ().enabled = false;

		//Disable all particle systems
		foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
		{
			particle.enableEmission = false;
		}
	}

	void EnablePlayer()
	{
		//Start player from moving or colliding
		m_spriteRenderer.enabled = true;
		m_rigidBody.isKinematic = false;
		m_canBomb = true;
		GetComponent<PlayerShooting>().canFire = true;
		GetComponent<PolygonCollider2D> ().enabled = true;
		
		//Enable all particle systems
		foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
		{
			particle.enableEmission = true;
		}
	}

	void RemoveLife()
	{
		if (m_lives <= 1)
		{
			//Show the game over screen and destory the player
			HUDState.instance.ShowGameOver();
			Destroy(gameObject);
		}
		else
		{
			//Remove a life and reset the player position to center
			m_lives--;
			HUDState.instance.SetLives(m_lives);
			DisablePlayer();

			StartCoroutine(RestartGame());
		}
	}

	IEnumerator RestartGame()
	{
		//Slow down game fps for effect
		while (Time.timeScale > 0.3f)
		{
			Time.timeScale -= 0.1f;
			yield return new WaitForSeconds(0.1f);
		}

		//Wait half a second before restarting
		yield return new WaitForSeconds(0.5f);

		//Reset game speed
		Time.timeScale = 1f;

		//Remove all asteroids onscreen
		GameObject.Find("AsteroidPool").GetComponent<ObjectPool>().RecycleAllObjectsInPool();
		GameObject.Find("BulletPool").GetComponent<ObjectPool>().RecycleAllObjectsInPool();
		GameObject.Find("BulletExplosionPool").GetComponent<ObjectPool>().RecycleAllObjectsInPool();
		
		//Remove all bombs onscreen
		foreach (GameObject bomb in GameObject.FindGameObjectsWithTag("Bomb"))
		{
			Destroy (bomb);
		}

		//Reset position of player to center and activate
		m_spriteRenderer.enabled = true;
		transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
		HUDState.instance.ShowCountdown ();
	}

	void AddBomb()
	{
		//Add to the bombs (clamp at 3) and update hud
		if (m_bombs < 3)
		{
			m_bombs++;
			HUDState.instance.SetBombs(m_bombs);
		}
	}

	void DropBomb()
	{
		if (m_canBomb && m_bombs > 0) {
			m_bombs--;

			//Create the bomb explosion particle effect
			GameObject bombExposion = Instantiate(bombEffect);
			bombExposion.transform.position = gameObject.transform.position;

			//Remove all asteroids after a second
			StartCoroutine(RemoveAllAsteroids());

			//Update HUD with bomb
			HUDState.instance.SetBombs(m_bombs);
		}
	}

	IEnumerator RemoveAllAsteroids()
	{
		yield return new WaitForSeconds(0.7f);
		GameObject.Find ("AsteroidPool").GetComponent<ObjectPool>().RecycleAllObjectsInPool();
	}
}
