using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour 
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Firing
	public float fireRate = 0.2f;
	public bool canFire = false;
	float m_nextFire = 0.2f;

	//Bullets
	public ObjectPool m_bulletPool;
	List<Vector2> m_bulletSpawnPositions = new List<Vector2>
	{
		new Vector2(1f,-0.15f), //First bullet position
		new Vector2(1f,-0.05f) //Second bullet position
	};

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		GetComponent<PlayerControls>().spacePressed = Fire;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Fire()
	{
		//Make sure bullets are not being fired too quickly
		if (canFire)
		{
			if (Time.time > m_nextFire)
			{
				m_nextFire = Time.time + fireRate;

				//Spawn each of the bullets at their spawn positions
				foreach (Vector2 offset in m_bulletSpawnPositions)
				{
					//Recycle bullets from the pool, set their position and fire
					GameObject newBullet = m_bulletPool.InstantiateNextAvailableObject();
					newBullet.transform.position = gameObject.transform.position + new Vector3(offset.x, offset.y, 0f);
					newBullet.GetComponent<BulletController>().FireBullet();
				}
			}
		}
	}
}
