using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Properties
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public GameObject objectPrefab; //Prefab to create in the pool
	public int startSize; //Number of starting instances
	List<GameObject> m_objectPool = new List<GameObject>();

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Setup
	////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start(){

		//Populate the pool with disabled items to begin with
		for (int i=0; i<startSize; i++)
		{
			InstantiateNewObject().SetActive(false);
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// Methods
	////////////////////////////////////////////////////////////////////////////////////////////////////

	//Create new object from prefab and add it to the pool
	public GameObject InstantiateNewObject()
	{
		GameObject newObject = Instantiate(objectPrefab);
		newObject.transform.parent = gameObject.transform;
		m_objectPool.Add(newObject);
		return newObject;
	}

	//Reuse the next available object from the pool or create one if empty
	public virtual GameObject InstantiateNextAvailableObject()
	{
		//Loop through each object in the pool and find an inative one
		foreach (GameObject nextObject in m_objectPool)
		{
			if (!nextObject.activeSelf)
			{
				//If a valid item is found then reset and return it
				nextObject.SetActive(true);
				return nextObject;
			}
		}

		//If none where found in the pool create a new one and return it
		return InstantiateNewObject();
	}

	//Once an object has been finished with disable it and wait for re use
	public void RecycleObjectInPool(GameObject _oldObject)
	{
		//Find object in the pool and disable it
		if (m_objectPool.Contains(_oldObject))
		{
			_oldObject.SetActive(false);
		}
		else
		{
			Debug.LogError("Object could not be found in pool and has not been recycled");
		}
	}

	//Recycle all objects in the pool
	public void RecycleAllObjectsInPool()
	{
		foreach (GameObject nextObject in m_objectPool)
		{
			RecycleObjectInPool(nextObject);
		}
	}
}
