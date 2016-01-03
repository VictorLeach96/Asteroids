using UnityEngine;
using System.Collections;

public class WallMovement : MonoBehaviour {

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	Rigidbody2D m_rigidBody;
	WallManager m_obstacleManager;

	//Speed
	public float moveSpeed = 2f;
	public float tileOffset = 1.3f; //Gives leaway for different aspect ratios

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Awake()
	{
		m_rigidBody = (Rigidbody2D)GetComponent<Rigidbody2D>();
		m_obstacleManager = (WallManager)gameObject.GetComponentInParent<WallManager>();
	}
	
	void Update()
	{
		//Move wall left constantly at given speed
		m_rigidBody.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

		//Check whether the wall piece is completely off the screen and if so remove it
		if (transform.position.x < -(m_obstacleManager.wallWidth * tileOffset)){
			m_obstacleManager.RecycleWall(gameObject);
		}
	}
}