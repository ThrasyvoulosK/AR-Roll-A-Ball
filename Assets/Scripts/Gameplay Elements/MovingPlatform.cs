using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Patrolling
{
    // Update is called once per frame
    void Update()
    {
        PatrolBetweenTwoPoints();  
    }
}
