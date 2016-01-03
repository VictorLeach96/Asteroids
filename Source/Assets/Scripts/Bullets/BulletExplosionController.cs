using UnityEngine;
using System.Collections;

public class BulletExplosionController : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	ObjectPool m_bulletExplosionPool;
	ParticleSystem m_particlesEmitter;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		m_particlesEmitter = (ParticleSystem)GetComponentInChildren<ParticleSystem>();
		m_bulletExplosionPool = (ObjectPool)GameObject.FindGameObjectWithTag("BulletExplosionPool").GetComponent<ObjectPool>();
	}

	void Update()
	{
		//Remove the bullet explosion effect once it has finished playing
		if (!m_particlesEmitter.IsAlive())
		{
			m_bulletExplosionPool.RecycleObjectInPool(gameObject);
		}
	}
}
