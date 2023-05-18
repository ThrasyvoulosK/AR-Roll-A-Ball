using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : PlayerDeath
{
    public MovingCollectible movingCollectible;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowCollectible());   
    }

    IEnumerator FollowCollectible()
    {
        while (movingCollectible.gameObject.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingCollectible.transform.position, (movingCollectible.speed*0.5f) * Time.deltaTime);

            yield return null;
        }

        Destroy(gameObject);

    }
}
