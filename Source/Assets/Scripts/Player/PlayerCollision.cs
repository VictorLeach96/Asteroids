using UnityEngine;
using System.Collections;
using System;

public class PlayerCollision : MonoBehaviour 
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public GameObject explosionEffect;
	public Action playerExploded = null;
	public Action bombCollected = null;
	public ObjectPool asteroidPool;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Event Listeners
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Run the explosion effect on the player if it collides with walls or asteroids
		if (collision.gameObject.tag == "Walls")
		{
			Explode ();
		}
		else if (collision.gameObject.tag == "Asteroids")
		{
			Explode();
			asteroidPool.RecycleObjectInPool(collision.gameObject);
		}
		else if (collision.gameObject.tag == "Bomb")
		{
			//Trigger bomb collected action
			if (bombCollected != null)
			{
				bombCollected();
			}

			//Destroy the bomb
			Destroy(collision.gameObject);
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Explode()
	{
		//Create the explode particle at this position
		GameObject particleEffect = Instantiate(explosionEffect);
		particleEffect.transform.position = gameObject.transform.position;

		//Trigger the player exploded action
		if (playerExploded != null)
		{
			playerExploded();
		}
	}
}
