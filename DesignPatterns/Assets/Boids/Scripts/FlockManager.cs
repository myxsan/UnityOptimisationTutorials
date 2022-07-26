using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

	public GameObject boidPrefab;
	public int numBoids = 20;
	public GameObject[] allBoids;

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

	// Use this for initialization
	void Start () {
		allBoids = new GameObject[numBoids];
		for(int i = 0; i < numBoids; i++)
		{
			Vector3 pos = this.transform.position + Random.insideUnitSphere * 10;
			allBoids[i] = (GameObject) Instantiate(boidPrefab, pos, Quaternion.identity);
			allBoids[i].GetComponentInChildren<Flock>().myManager = this;
		}
	}

}
