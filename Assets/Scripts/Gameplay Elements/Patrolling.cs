using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    public Vector3 startingPosition;
    public Transform endingTransform;
    public bool hasReachedEndingPosition = false;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        PatrolBetweenTwoPoints();
    }

    public void PatrolBetweenTwoPoints()
    {
        if (!hasReachedEndingPosition)
        {
            if (transform.position != endingTransform.position)
                transform.position = Vector3.MoveTowards(transform.position, endingTransform.position, speed * Time.deltaTime);
            else
                hasReachedEndingPosition = true;
        }
        else
        {
            if (transform.position != startingPosition)
                transform.position = Vector3.MoveTowards(transform.position, startingPosition, speed * Time.deltaTime);
            else
                hasReachedEndingPosition = false;
        }
    }
}
