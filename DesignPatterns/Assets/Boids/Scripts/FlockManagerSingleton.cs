using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManagerSingleton : MonoBehaviour {

	public GameObject boidPrefab;
	public int numBoids = 20;
	public GameObject[] allBoids;
	public 	Dictionary<GameObject, FlockSingleton> fs = new Dictionary<GameObject, FlockSingleton>();

	public Vector3 limits = new Vector3(5, 5, 5);

	[Header("Boid Settings")]
	[Range(0.0f, 5.0f)]
	public float minSpeed = 2;
	[Range(0.0f, 5.0f)]
	public float maxSpeed = 4;
	[Range(1.0f, 10.0f)]
	public float neighbourDistance = 3;
	[Range(0.0f, 5.0f)]
	public float rotationSpeed = 2;

	static FlockManagerSingleton instance;
	public static FlockManagerSingleton Instance
	{
		get
		{
			if (!instance)
			{
				FlockManagerSingleton[] managers = GameObject.FindObjectsOfType<FlockManagerSingleton>();
				if(managers != null)
					if(managers.Length == 1)
					{
						instance = managers[0];
						return managers[0];
					}
				GameObject go = new GameObject("FlockManagerSingleton", typeof(FlockManagerSingleton));
				instance = go.GetComponent<FlockManagerSingleton>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
		set
		{
			instance = value as FlockManagerSingleton;
		}
	}

	// Use this for initialization
	void Start () {
		allBoids = new GameObject[numBoids];
		for(int i = 0; i < numBoids; i++)
		{
			Vector3 pos = this.transform.position + Random.insideUnitSphere * 10;
			allBoids[i] = (GameObject) Instantiate(boidPrefab, pos, Quaternion.identity);
			fs.Add(allBoids[i], allBoids[i].GetComponent<FlockSingleton>());
		}
	}

}
