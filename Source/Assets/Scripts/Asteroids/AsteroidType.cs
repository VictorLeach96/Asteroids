using UnityEngine;
using System.Collections;

public class AsteroidType : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	Rigidbody2D m_rigidBody;
	public Vector2 speedRange;
	public Vector2 spinRange;
	public Vector2 scaleRange;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public virtual void Awake()
	{
		m_rigidBody = (Rigidbody2D)GetComponent<Rigidbody2D> ();
	}

	void Start()
	{	
		//Start moving asteroid with random speed and rotation
		m_rigidBody.AddForce(Vector3.left * Random.Range(speedRange.x, speedRange.y));
		m_rigidBody.AddTorque(Random.Range (spinRange.x, spinRange.y));

		//Set the asteroid scale to a random value in range
		float newScale = Random.Range (scaleRange.x, scaleRange.y);
		transform.localScale = new Vector3 (newScale, newScale, 1f);
	}
}
