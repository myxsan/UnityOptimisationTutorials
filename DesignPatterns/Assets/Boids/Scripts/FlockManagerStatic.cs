using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlockManagerStatic {
	public static GameObject[] allBoids;

	public static Vector3 limits = new Vector3(5, 5, 5);

	public static float minSpeed = 2;
	public static float maxSpeed = 4;
	public static float neighbourDistance = 3;
	public static float rotationSpeed = 2;

	// Use this for initialization
	public static void Init (GameObject boidPrefab, int numBoids) {
		allBoids = new GameObject[numBoids];
		for(int i = 0; i < numBoids; i++)
		{
			Vector3 pos = Random.insideUnitSphere * 10;
			allBoids[i] = GameObject.Instantiate(boidPrefab, pos, Quaternion.identity);
		}
	}

}
