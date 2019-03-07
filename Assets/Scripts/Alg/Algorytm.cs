using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorytm : MonoBehaviour
{

	readonly static float LOWER_ARM_LENGTH = 12.1678f;
	readonly static float MEDIUM_ARM_LENGTH = 12.282f;
	readonly static float UPPER_ARM_LENGTH = 14.5079f;


    public GameObject joint1;
    public GameObject joint2;
    public GameObject joint3;
    public GameObject dolne;
    public GameObject srodkowe;
    public GameObject gorne;
    public GameObject baza;
    public GameObject obiekt;
    public GameObject chwytak;


    float angle;
    float currentAngle = 0;

    float[] armAngles;
    float[] currentArmAngles = new float[]{90, 90, 90};

    // Start is called before the first frame update
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

        angle = rotateTowardsTarget(new Point(obiekt.transform.position.x, obiekt.transform.position.y, obiekt.transform.position.z));


        armAngles = moveTowardsTarget_Perpendicular(obiekt.transform.position);
        // Debug.Log(armAngles[0]+" "+armAngles[1]+" "+armAngles[2]);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentAngle+" "+-1*angle);
        if(currentAngle < -1*angle){
            joint1.transform.RotateAround(joint1.transform.position, new Vector3(0, 1, 0), 0.5f);
            currentAngle+=0.5f;
        }
        // Debug.Log(currentArmAngles[0]+" "+armAngles[0]);
        if(currentArmAngles[0] > armAngles[0]){
            dolne.transform.RotateAround(joint1.transform.position, joint1.transform.right, 0.5f);
            currentArmAngles[0]-=0.5f;
        }
        if(currentArmAngles[1] > armAngles[1]){
            // Debug.Log(srodkowe.GetComponent<Renderer>().bounds.size.z);
            srodkowe.transform.RotateAround(joint2.transform.position, joint2.transform.right, 0.5f);
            currentArmAngles[1]-=0.5f;
        }
        if(currentArmAngles[2] > armAngles[2]){
            gorne.transform.RotateAround(joint3.transform.position, joint3.transform.right, 0.5f);
            currentArmAngles[2]-=0.5f;
        }
    }

    public float rotateTowardsTarget(Point target)
    {
        float current_y = joint1.transform.rotation.y;
        float cosAlfa = (target.x*Mathf.Cos(current_y) +
                         target.z*Mathf.Sin(current_y)) /
                         Mathf.Sqrt( Mathf.Pow(target.x, 2)+Mathf.Pow(target.z,2));
        float angle = Mathf.Acos(cosAlfa % Mathf.PI);

        short direction = 0;
        if(target.x*Mathf.Sin(current_y) + target.z*Mathf.Cos(current_y)*-1 < 0)
            direction = -1;
        else
            direction = 1;

        if(angle*Mathf.Rad2Deg > 90){
            angle -= 90*Mathf.Deg2Rad;
            direction *= -1;
        }
        return Mathf.Rad2Deg*angle * direction;
    }

    public float[] moveTowardsTarget_Perpendicular(Vector3 target){

        float distance = Vector3.Distance(target, joint1.transform.position);
        // float distance = Vector3.Distance(new Vector3(target.x,target.y,0), new Vector3(joint1.transform.position.x, (joint1.transform.position.y+0.25f), 0)); // 0,25 to 1/4 joint1, o ktory jest podniesiona podstawa
        float x2 = (Mathf.Pow(UPPER_ARM_LENGTH, 2) - Mathf.Pow(LOWER_ARM_LENGTH, 2) + Mathf.Pow(distance - MEDIUM_ARM_LENGTH, 2)) /
                    (2 * (distance - MEDIUM_ARM_LENGTH));
        float x1 = distance - MEDIUM_ARM_LENGTH - x2;

        Debug.Log(distance);
        Debug.Log(x1+" "+MEDIUM_ARM_LENGTH+" "+x2);

        float alfa = Mathf.Acos(x1 / LOWER_ARM_LENGTH)*Mathf.Rad2Deg;
        float beta = Mathf.Acos(x2 / UPPER_ARM_LENGTH)*Mathf.Rad2Deg;

        Debug.Log(alfa+" "+beta);
        return new float[]{alfa, 90-alfa, beta};
    }
}
