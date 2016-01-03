using UnityEngine;
using System.Collections;

///Asteroid is medium in scale and speed and rotates quickly
public class Asteroid3Spin : AsteroidType
{
	public override void Awake()
	{
		base.Awake();

		//Configure asteroid properties
		speedRange = new Vector2(140f, 160f);
		spinRange = new Vector2(30f, 50f);
		scaleRange = new Vector2(0.5f, 0.7f);
	}
}
