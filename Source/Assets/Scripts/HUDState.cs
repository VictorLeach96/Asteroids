using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class HUDState : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Singleton
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public static HUDState instance = null;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public Action countdownEnded = null;

	//HUD Images
	bool m_paused = false;
	bool m_countingDown = false;
	bool m_gameOver = false;
	public Text hudText;
	public Text hudScore;
	public Button hudPause;
	public Button hudMenu;
	public List<Image> hudLives;
	public List<Image> hudBombs;
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////
	
	void Awake()
	{
		//Assign this as singleton class if haven't already done so
		if (instance != null)
		{
			Debug.LogError("Singleton HUDState already exists");
		}
		instance = this;
	}

	void Start()
	{
		UpdatePause();
		ShowCountdown();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Countdown
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public void ShowCountdown()
	{
		StartCoroutine(_ShowCountdown());
	}

	IEnumerator _ShowCountdown()
	{
		//Run countdown routine updating hud
		m_countingDown = true;
		hudText.enabled = true;
		hudText.text = "3";
		yield return new WaitForSeconds(0.5f);
		hudText.text = "2";
		yield return new WaitForSeconds(0.5f);
		hudText.text = "1";
		yield return new WaitForSeconds(0.5f);
		hudText.enabled = false;
		m_countingDown = false;

		//Run the finished action to notify other classes that it has countdown
		if (countdownEnded != null)
		{
			countdownEnded();
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Lives / Bombs / Score
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public void SetScore(int score)
	{
		hudScore.text = "Score " + score;
	}

	public void SetLives(int lives)
	{
		//Loop through each of the hud live images
		for (int i=0; i < hudLives.Count; i++)
		{
			//If the current life is larger than the number of lives player should have then enable/disable the image
			if (i > lives - 1)
			{
				hudLives[i].enabled = false;
			}
			else
			{
				hudLives[i].enabled = true;
			}
		}
	}

	public void SetBombs(int bombs)
	{
		//Loop through each of the hud bomb images
		for (int i=0; i < hudBombs.Count; i++)
		{
			//If the current bomb is larger than the number of lives player should have then enable/disable the image
			if (i > bombs - 1)
			{
				hudBombs[i].enabled = false;
			}
			else
			{
				hudBombs[i].enabled = true;
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Pausing
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public void TogglePause()
	{
		if (!m_countingDown && !m_gameOver) 
		{
			m_paused = !m_paused;
			UpdatePause ();
		}
	}

	public void UpdatePause()
	{
		if (m_paused)
		{
			//Pause fps
			Time.timeScale = 0f;
			
			//Show buttons and set correct text
			hudMenu.enabled = true;
			hudMenu.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
			hudMenu.GetComponentInChildren<Text>().color = Color.black;
			hudPause.GetComponentInChildren<Text> ().text = "Resume";
			
			//Show paused message
			hudText.enabled = true;
			hudText.text = "Paused";
		}
		else
		{
			//Hide buttons and set correct text
			hudMenu.enabled = false;
			hudMenu.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
			hudMenu.GetComponentInChildren<Text>().color = Color.clear;
			hudPause.GetComponentInChildren<Text> ().text = "Pause";
			
			//Hide paused message
			hudText.enabled = true;
			hudText.text = "";
			
			//Resume fps
			Time.timeScale = 1f;
		}
	}

	public void MenuScreen()
	{
		Time.timeScale = 1f; //Resume game fps if paused
		Application.LoadLevel("Menu");
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Game Over
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Once the game is over show the game over text onscreen
	public void ShowGameOver()
	{
		m_gameOver = true;
		hudText.text = "Game Over!";
		hudText.enabled = true;
		SetLives(0);
		StartCoroutine (FinishGameOver());
	}

	//Wait for a couple of seconds before changing back to the menu scene
	IEnumerator FinishGameOver()
	{
		yield return new WaitForSeconds(5f);
		MenuScreen();
	}
}
