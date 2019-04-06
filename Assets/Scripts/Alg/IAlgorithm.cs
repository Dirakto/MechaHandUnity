using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlgorithm
{
    float[] rotateTowardsTarget(Vector3 target);
    float[] moveTowardsTarget(Vector3 target);
}
