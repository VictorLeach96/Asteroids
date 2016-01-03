using UnityEngine;
using System.Collections;
using System;

public class PlayerControls : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public Action upPressed;
	public Action spacePressed;
	public Action enterPressed;
	Vector2 m_startSwipePosition;
	int m_swipeTouchId;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Update()
	{
		//Up arrow key for thrusting
		if ((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) &&
			upPressed != null)
		{
			upPressed();
		}

		//Space bar for firing
		if (Input.GetKey(KeyCode.Space) && spacePressed != null)
		{
			spacePressed();
		}

		//Enter pressed for bomb (Key down to make sure it doesn't keep triggering)
		if (Input.GetKeyDown (KeyCode.Return) && enterPressed != null)
		{
			enterPressed ();
		}

		//Check each touch for gestures or actions
		foreach (Touch touch in Input.touches)
		{
			//When a touch starts save its starting y position
			if (touch.phase == TouchPhase.Began)
			{
				m_startSwipePosition = touch.position;
				m_swipeTouchId = touch.fingerId;
			}

			//Check if player has pressed and held on left side of the screen
			if (touch.phase == TouchPhase.Stationary &&
			    Camera.main.ScreenToWorldPoint(touch.position).x < 0 &&
			    upPressed != null)
			{
				upPressed();
			}

			//Check if player has pressed and held on right side of the screen
			if (touch.phase == TouchPhase.Stationary &&
			    Camera.main.ScreenToWorldPoint(touch.position).x > 0 &&
			    spacePressed != null)
			{
				spacePressed();
			}

			//When the touch moves check that it has moved right more than 150
			if (touch.phase == TouchPhase.Moved &&
			    touch.position.x - m_startSwipePosition.x > 150 &&
			    m_swipeTouchId == touch.fingerId && //Track finger id to make sure that it doesn't confuse fingers which are being used to thrust/shoot
			    enterPressed != null)
			{
				//Check that the touch is moving in the same direction (right)
				if (Vector2.Dot(m_startSwipePosition, touch.position) > 0.7)
				{
					enterPressed();
					m_startSwipePosition = touch.position;
				}
			}
		}
	}
}
