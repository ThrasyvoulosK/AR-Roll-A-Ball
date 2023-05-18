using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnArea spawnArea;

    public GameObject collectible;

    private int numberSpawned;
    public static Action onCollectiblesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        numberSpawned = UnityEngine.Random.Range(spawnArea.minNumberOfCollectibles, spawnArea.maxNumberOfCollectibles);
        Debug.Log("Number of Collectibles: " + numberSpawned);

        SpawnCollectibles();
    }

    void SpawnCollectibles()
    {
        for (int i = 0; i < numberSpawned; i++)
        {
            int tries = 0;

            while (tries < 3)
            {
                float randomX = UnityEngine.Random.Range(spawnArea.minTransform.position.x, spawnArea.maxTransform.position.x);
                float randomZ = UnityEngine.Random.Range(spawnArea.minTransform.position.z, spawnArea.maxTransform.position.z);
                float y = spawnArea.minTransform.position.y;

                Vector3 randomPosition = new Vector3(randomX, y, randomZ);
                GameObject collectibleClone = Instantiate(collectible, randomPosition, collectible.transform.rotation);

                if (IsColldingWithAnotherObject(randomPosition, collectibleClone))
                {
                    Destroy(collectibleClone);
                    tries++;
                }
                else
                {
                    break;
                }
            }
        }

        onCollectiblesSpawned?.Invoke();
    }

    public bool IsColldingWithAnotherObject(Vector3 randomPosition, GameObject collectibleClone)
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(randomPosition, 0.2f);
        foreach (var collider in nearbyColliders)
        {
            if(collider.gameObject.GetComponent<Collectible>() != null
                && !ReferenceEquals(collectibleClone, collider.gameObject) &&
                !collider.gameObject.tag.Equals("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
