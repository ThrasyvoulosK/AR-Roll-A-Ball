using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellMovement : Patrolling
{
    // Update is called once per frame
    void Update()
    {
        PatrolBetweenTwoPoints();

        if (!hasReachedEndingPosition)
            transform.LookAt(endingTransform);
        else
            transform.LookAt(startingPosition);
    }
}
