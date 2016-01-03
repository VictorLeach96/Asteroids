using UnityEngine;
using System.Collections;

///Asteroid is small, fast and rotates slowly
public class Asteroid1Fast : AsteroidType
{
	public override void Awake()
	{
		base.Awake();
		
		//Configure asteroid properties
		speedRange = new Vector2(180f, 220f);
		spinRange = new Vector2(5f, 10f);
		scaleRange = new Vector2(0.3f, 0.5f);
	}
}
