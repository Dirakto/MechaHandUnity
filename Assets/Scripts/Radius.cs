using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius


// [RequireComponent(typeof(LineRenderer))]
public class Radius : MonoBehaviour
{

    // [Range(0,50)]
    public int segments = 100;
    // [Range(0,5)]
    public float xradius;
    // [Range(0,5)]
    public float yradius;
    LineRenderer circle;
    LineRenderer line2;
    LineRenderer line1;

    float width = 0.3f;


    void Start()
    {
        float length = GameObject.Find("Chwytak").transform.position.y;
        this.xradius = length;
        this.yradius = length;

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
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
            {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            circle.SetPosition (i,new Vector3(x,0.1f,z) );
            angle += (360f / segments);
            }
    }





    void Update()
    {
        
    }
}
