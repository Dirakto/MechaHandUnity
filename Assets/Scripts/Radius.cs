using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius


// [RequireComponent(typeof(LineRenderer))]
public class Radius : MonoBehaviour
{
    private int segments = 100;
    private float length;
    private LineRenderer circle;
    private LineRenderer line1;
    private LineRenderer line2;

    private float width = 0.3f;


    void Start()
    {
        length = GameObject.Find("Chwytak").transform.position.y;

        circle = gameObject.GetComponent<LineRenderer>();

        circle.positionCount = (segments + 1);
        circle.useWorldSpace = true;
        circle.widthMultiplier = width;
        CreatePoints ();

        GameObject tmp = new GameObject();
        tmp.transform.position = new Vector3(0,0,0);
        tmp.AddComponent<LineRenderer>();

        line1 = tmp.GetComponent<LineRenderer>();
        line1.positionCount = 3;
        line1.useWorldSpace = true;
        line1.widthMultiplier = width;
        
        line1.SetPosition(0, new Vector3(Mathf.Sin(60*Mathf.Deg2Rad)*length,0.1f,-Mathf.Cos(60*Mathf.Deg2Rad)*length));
        line1.SetPosition(1, new Vector3(0,0,0));
        line1.SetPosition(2, new Vector3(-Mathf.Sin(60*Mathf.Deg2Rad)*length,0.1f,-Mathf.Cos(60*Mathf.Deg2Rad)*length));
    }

    void CreatePoints ()
    {
        float x;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * length;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * length;

            circle.SetPosition (i, new Vector3(x,0.1f,z) );
            angle += (360f / segments);
        }
    }

    void Update()
    {   
    }
}
