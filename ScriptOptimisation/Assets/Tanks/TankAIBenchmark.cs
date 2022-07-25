using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIBenchmark : MonoBehaviour
{
    GameObject[] tanks;
    public int numberOfTanks;
    public GameObject tankPrefab;
    [SerializeField] float tankSpeed = 0.5f;
    [SerializeField] Transform player;

    void Start()
    {
        tanks = new GameObject[numberOfTanks];
        for (int i = 0; i < numberOfTanks; i++)
        {
            tanks[i] = Instantiate(tankPrefab);
            tanks[i].transform.position = new Vector3(Random.Range(-50,50), 0, Random.Range(-50,50));
        }
    }

    void Update()
    {
        foreach (GameObject t in tanks)
        {
            if (!ReferenceEquals(player, null))
            {
                t.transform.LookAt(player.position);
                t.transform.Translate(0, 0, tankSpeed * Time.deltaTime);
            }
        } 
    }
}
