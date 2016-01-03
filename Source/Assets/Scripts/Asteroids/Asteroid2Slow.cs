using UnityEngine;
using System.Collections;

///Asteroid is large, slow and rotates slowly
public class Asteroid2Slow : AsteroidType
{
	public override void Awake()
	{
		base.Awake();
		
		//Configure asteroid properties
		speedRange = new Vector2(100f, 120f);
		spinRange = new Vector2(5f, 10f);
		scaleRange = new Vector2(0.7f, 1f);
	}
}

