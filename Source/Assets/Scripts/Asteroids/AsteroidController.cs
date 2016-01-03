using UnityEngine;
using System.Collections;

public class AsteroidController: MonoBehaviour
{
	///////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	///////////////////////////////////////////////////////////////////////////////////////////////////

	RandomObjectPool m_asteroidPool;
	PlayerController m_playerController;
	bool m_isVisible = false; //Specifies whether the asteroid has been on screen yet

	///////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	///////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		m_asteroidPool = (RandomObjectPool)GameObject.FindGameObjectWithTag ("AsteroidPool").GetComponent<RandomObjectPool> ();
		m_playerController = (PlayerController)GameObject.Find("Ship").GetComponent<PlayerController>();
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Event Listeners
	///////////////////////////////////////////////////////////////////////////////////////////////////

	void OnEnable()
	{
		m_isVisible = false;

		//Reset the asteroid type once it has been reenabled by the object pool
		RandomizeAsteroidType();
	}

	void OnBecameVisible()
	{
		m_isVisible = true;
	}
	void OnBecameInvisible()
	{
		//Remove the asteroid once it has moved out of the camera.
		//Make sure the asteroid has been on screen before removing it.
		if (m_isVisible)
		{
			m_asteroidPool.RecycleObjectInPool(gameObject);
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////
	// Collisions
	///////////////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Asteroid has collided with bullets.
		if (collision.gameObject.tag == "Bullet")
		{
			//Remove the asteroid and run the bullet exlosion effect
			m_asteroidPool.RecycleObjectInPool (gameObject);
			collision.gameObject.GetComponent<BulletController> ().ExplodeBullet ();

			//Add score to the player
			m_playerController.AddScore(10);
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	///////////////////////////////////////////////////////////////////////////////////////////////////

	//Clear all previous asteroid types and randomly apply a new one
	void RandomizeAsteroidType()
	{
		//Check each asteroid type and remove if they exist
		Destroy(GetComponent<Asteroid1Fast>());
		Destroy(GetComponent<Asteroid2Slow>());
		Destroy(GetComponent<Asteroid3Spin>());

		//Choose a random number for the asteroid type and add its component
		switch (Random.Range(0, 3))
		{
		case 0:
			gameObject.AddComponent<Asteroid1Fast>();
			break;
		case 1:
			gameObject.AddComponent<Asteroid2Slow>();
			break;
		case 2:
			gameObject.AddComponent<Asteroid3Spin>();
			break;
		}
	}
}
