using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{

    private float speed = 20f;
    public GameObject joint1;
    public GameObject joint2;
    public GameObject joint3;
    public GameObject dolne;
    public GameObject srodkowe;
    public GameObject gorne;
    public GameObject baza;
    public GameObject obiekt;
    public GameObject chwytak;
    

    public const float myViewAngle = 1f;

    void Start()
    {
        joint1 = GameObject.Find("Joint1");
        joint2 = GameObject.Find("Joint2");
        joint3 = GameObject.Find("Joint3");
        dolne = GameObject.Find("Dolne");
        srodkowe = GameObject.Find("Srodkowe");
        gorne = GameObject.Find("Gorne");
        baza = GameObject.Find("Baza");
        obiekt = GameObject.Find("Obiekt");
        chwytak = GameObject.Find("Chwytak");

        dolne.transform.RotateAround(joint1.transform.position, joint2.transform.right, 90);
        // srodkowe.transform.RotateAround(joint2.transform.position, joint2.transform.right, 20);
        // gorne.transform.RotateAround(joint3.transform.position, joint3.transform.right, 45);

    }

    void Update()
    {

        // Vector3 direction = new Vector3(obiekt.transform.position.x, 0, obiekt.transform.position.z) - new Vector3(joint1.transform.position.x, 0,joint1.transform.position.z);
        // float angle = Vector3.Angle(direction, joint1.transform.forward);//-1*joint1.transform.right);
        // // Debug.Log(myViewAngle);
        // if ( angle - myViewAngle * 0.5f < 0 )
        // {

        // }
        // else
        // {
        //     // Debug.Log(joint3.transform.rotation.y);
        //     joint1.transform.RotateAround(joint1.transform.position, new Vector3(0, 1, 0), 0.5f); // dobre
        //     srodkowe.transform.RotateAround(joint2.transform.position, joint2.transform.right, 0.5f);
        //     gorne.transform.RotateAround(joint3.transform.position, joint3.transform.right, 0.5f);
        // }








        //float tmp = speed * Time.deltaTime;
        //angle += tmp;
        //if (angle <= 90)
        //{

        // transform.RotateAround(joint1.transform.position, new Vector3(joint1.transform.position.x, joint1.transform.position.y, joint1.transform.position.z), 0.5f);
        // srodkowe.transform.RotateAround(joint2.transform.position, Vector3.back, tmp);
        // gorne.transform.RotateAround(joint3.transform.position, new Vector3(0,0,-joint3.transform.position.z), 0.5f);
        // }
        // baza.transform.Rotate(new Vector3(0, 1, 0));

        // baza.transform.Rotate(tmp, tmp, tmp, Space.World);

        // if (transform.rotation.y >= chwytak.transform.rotation.y-1 || transform.rotation.y <= chwytak.transform.rotation.y+1)
        // {
        //    transform.Rotate(new Vector3(0, 1, 0));
        // }
    }
}
