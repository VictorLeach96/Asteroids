﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	Rigidbody2D m_rigidBody;

	//Thrusters
	public float thrustSpeed = 15f;
	bool m_canThrust = true;
	ParticleSystem m_thrusterParticles;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		m_rigidBody = (Rigidbody2D)GetComponent<Rigidbody2D>();
		m_thrusterParticles = (ParticleSystem)transform.FindChild("ThrustEmitter").GetComponent<ParticleSystem>();
		GetComponent<PlayerControls> ().upPressed = Thrust;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Thrust()
	{
		//Thrust player upwards and play particle effect underneth
		if (m_canThrust)
		{
			m_rigidBody.AddForce (Vector2.up * thrustSpeed);
			m_thrusterParticles.Play();
		}
	}
}