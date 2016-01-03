using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallManager : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public float wallWidth = 0f;
	public List<GameObject> obstaclePrefabs;
	public List<GameObject> obstaclePool;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start()
	{
		//Calculate the width of a single wall piece
		wallWidth = obstaclePool[1].transform.position.x - obstaclePool[0].transform.position.x;

		//Randomize each wall type when the game starts
		foreach (GameObject obstacle in obstaclePool)
		{
			if (obstaclePrefabs.Contains(obstacle) || obstacle == null)
			{
				RandomizeToPrefab(obstacle);
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Once a wall piece has been recycled randomize its type again
	public void RecycleWall(GameObject _oldObject)
	{
		RandomizeToPrefab(_oldObject);

		//Move wall to other side of screen
		_oldObject.transform.Translate(new Vector3(wallWidth * obstaclePool.Count, 0f, 0f));
	}

	//Match the object to a random prefab
	void RandomizeToPrefab(GameObject _gameObject)
	{
		//Choose a random prefab
		GameObject prefab = obstaclePrefabs[Random.Range (0, obstaclePrefabs.Count)];
		
		//Copy sprite from the chosen prefab
		_gameObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

		//Copy the collider from the chosen prefab
		PolygonCollider2D polygonColider = prefab.GetComponent<PolygonCollider2D>();
		for (int i=0; i<polygonColider.pathCount; i++)
		{
			_gameObject.GetComponent<PolygonCollider2D>().SetPath(i, polygonColider.GetPath(i));
		}
	}
}
