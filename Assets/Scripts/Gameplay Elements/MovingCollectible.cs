using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCollectible : Collectible
{
    public List<Vector3> waypoints;
    public SpawnArea spawnArea;
    public int currentWaypoint = 0;
    public float speed = 5;
    public const string SpawnAreaTag = "SpawnArea";
    // Start is called before the first frame update
    void Start()
    {
        spawnArea.minTransform = GameObject.FindGameObjectWithTag(SpawnAreaTag).transform.GetChild(0).transform;
        spawnArea.maxTransform = GameObject.FindGameObjectWithTag(SpawnAreaTag).transform.GetChild(1).transform;

        GenerateRandomWaypoints();

        StartCoroutine(MoveThroughWaypoints());
    }

    IEnumerator MoveThroughWaypoints()
    {
        while(transform.position != waypoints[currentWaypoint])
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint], speed * Time.deltaTime);

            yield return null;
        }

        if (currentWaypoint < waypoints.Count - 1)
            currentWaypoint++;
        else
            currentWaypoint = 0;

        StartCoroutine(MoveThroughWaypoints());
    }

    void GenerateRandomWaypoints()
    {
        int numberOfWaypoints = Random.Range(5, 10);
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            waypoints.Add(GenerateRandomPosition());
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(spawnArea.minTransform.position.x, spawnArea.maxTransform.position.x);
        float randomZ = Random.Range(spawnArea.minTransform.position.z, spawnArea.maxTransform.position.z);

        return new Vector3(randomX, transform.position.y, randomZ) ;
    }
}
