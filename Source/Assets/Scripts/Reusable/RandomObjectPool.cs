using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectPool : ObjectPool
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public List<GameObject> objectPrefabs; //List of prefabs to choose randomly from

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Setup the next available object from the pool and setup to match random prefab
	public override GameObject InstantiateNextAvailableObject()
	{
		//Get prefab recycled from the superclass object pool
		GameObject newObject = base.InstantiateNextAvailableObject();

		//Choose a random prefab from the list
		GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];

		//Copy the sprite from the prefab to the new object
		newObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

		//Copy the collider from the prefab to the new object
		PolygonCollider2D polygonColider = prefab.GetComponent<PolygonCollider2D>();
		for (int i=0; i<polygonColider.pathCount; i++)
		{
			newObject.GetComponent<PolygonCollider2D>().SetPath(i, polygonColider.GetPath(i));
		}

		return newObject;
	}
}
