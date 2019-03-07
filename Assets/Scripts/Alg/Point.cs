using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public float x;
    public float y;
    public float z;

    public Point(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Point(Vector3 point){
        this.x = point.x;
        this.y = point.y;
        this.z = point.z;
    }

    public override string ToString()
    {
        return "("+x+","+y+","+z+")";
    }

}