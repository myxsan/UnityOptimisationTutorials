using System.Collections.Generic;
using UnityEngine;

public class FlockSingleton : MonoBehaviour {

	Vector3 pos;
	public float speed;
	Vector3 direction;
	int groupSize;

	public Transform target;

	// Use this for initialization
	void Start () {
		speed = Random.Range(FlockManagerSingleton.Instance.minSpeed,
								FlockManagerSingleton.Instance.maxSpeed);
		Color color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		GetComponent<Renderer>().material.color = color;
		direction = Vector3.forward;

	}

	// Update is called once per frame
	void Update ()
	{
		pos = transform.position;
		//if out of bounds
		if (pos.x < -FlockManagerSingleton.Instance.limits.x || pos.x > FlockManagerSingleton.Instance.limits.x ||
			pos.y < -FlockManagerSingleton.Instance.limits.y || pos.y > FlockManagerSingleton.Instance.limits.y ||
			pos.z < -FlockManagerSingleton.Instance.limits.z || pos.z > FlockManagerSingleton.Instance.limits.z)
		{
			//turn back to the managers centre
			direction = FlockManagerSingleton.Instance.transform.position - transform.position;
		}
		else if (Random.Range(0, 1000) < 20)
		{
			speed = Random.Range(FlockManagerSingleton.Instance.minSpeed, FlockManagerSingleton.Instance.maxSpeed);
		}
		else if (Random.Range(0, 1000) < 10) //go somewhere random
		{
			direction = Random.insideUnitSphere * 100;
		}
		else if (Random.Range(0, 100) < 20)
		{
			ApplyRules();
		}

		transform.Translate(0, 0, Time.deltaTime * speed);
		if (!direction.Equals(Vector3.zero))
			transform.rotation = Quaternion.Slerp(transform.rotation,
										  Quaternion.LookRotation(direction),
										  FlockManagerSingleton.Instance.rotationSpeed * Time.deltaTime);
	}

	void ApplyRules()
	{
		GameObject[] gos;
		gos = FlockManagerSingleton.Instance.allBoids;
		
		Vector3 vcentre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.00f;
		float nDistance;
		groupSize = 0;

		foreach (GameObject go in gos) 
		{
			if(go != this.gameObject)
			{
				nDistance = Vector3.Distance(go.transform.position,this.transform.position);
				if(nDistance <= FlockManagerSingleton.Instance.neighbourDistance)
				{
					vcentre += go.transform.position;	
					groupSize++;	
					
					if(nDistance < 1.0f)		
					{
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}
					
					FlockSingleton anotherFlock = FlockManagerSingleton.Instance.fs[go];
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		} 
		
		if(groupSize > 0)
		{
			vcentre = vcentre / groupSize;
			speed = gSpeed/groupSize;
			
			direction = (vcentre + vavoid) - transform.position;
		}
	}
}
