using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorytm : MonoBehaviour
{

	readonly static float LOWER_ARM_LENGTH = 12.1678f+0.5f; // +korekta jointa
	readonly static float MEDIUM_ARM_LENGTH = 12.282f+0.5f; // +korekta jointa
	readonly static float UPPER_ARM_LENGTH = 14.5079f+0.25f+(0.4835968f);// +korekta jointa i chwytaka

        // Debug.Log(joint2.transform.position.y - joint1.transform.position.y);
        // Debug.Log(joint3.transform.position.y - joint2.transform.position.y);
        // Debug.Log(chwytak.transform.position.y - joint3.transform.position.y);

    public GameObject joint1;
    public GameObject joint2;
    public GameObject joint3;
    public GameObject dolne;
    public GameObject srodkowe;
    public GameObject gorne;
    public GameObject baza;
    public GameObject obiekt;
    public GameObject chwytak;

    // Vector3 lastTargetPosition;

    float angle;
    float currentAngle = 0;
    float rotateDirection = -1;

    float[] armAngles;
    float[] currentArmAngles = new float[]{90, 90, 90};
    float moveDirection = 1;

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

        calculateAllData();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.5f;
        if(currentAngle < angle){
            joint1.transform.RotateAround(joint1.transform.position, new Vector3(0, 1, 0), rotateDirection*speed);
            currentAngle+=speed;
        }

        if(currentArmAngles[0] > armAngles[0]){
            dolne.transform.RotateAround(joint1.transform.position, joint1.transform.right, moveDirection*speed);
            currentArmAngles[0]-=0.5f;
        }
        if(currentArmAngles[1] > armAngles[1]){

            srodkowe.transform.RotateAround(joint2.transform.position, joint2.transform.right, moveDirection*speed);
            currentArmAngles[1]-=0.5f;
        }
        if(currentArmAngles[2] > armAngles[2]){
            gorne.transform.RotateAround(joint3.transform.position, joint3.transform.right, moveDirection*speed);
            currentArmAngles[2]-=0.5f;
        }


        if(Input.GetKeyDown(KeyCode.B)){
            // float[] tmp = new float[]{ 90, 90, 90};
            // for(int i = 0; i < currentArmAngles.Length; i++)
            calculateAllData();
            if(angle < 0){
                rotateDirection = -1;
                angle*=-1;
            }
            currentAngle = (currentAngle + angle);
            Debug.Log(currentAngle+" > "+angle+" "+rotateDirection);
        }


    }

    public float rotateTowardsTarget(Vector3 target)
    {
        // os Z jest Polnoca

        // float current_y = joint1.transform.rotation.y;

        // chyba zla wersja, trzeba uzyc .right
        // float cosAlfa = (target.x*Mathf.Cos(current_y) +
        //                  target.z*Mathf.Sin(current_y)) /
        //                  Mathf.Sqrt( Mathf.Pow(target.x, 2)+Mathf.Pow(target.z,2));
        float cosAlfa = ((target.x*Vector3.forward.x) + (target.z*Vector3.forward.z)) /
                        Mathf.Sqrt( Mathf.Pow(target.x, 2)+Mathf.Pow(target.z,2));
        float angle = Mathf.Acos(cosAlfa);// % Mathf.PI);

        short direction = 0;
        // ten sam powod co wyzej
        // if((target.x*Mathf.Sin(current_y) + target.z*Mathf.Cos(current_y)*-1) < 0)
        Vector3 right = Vector3.Cross(joint1.transform.forward, new Vector3(0,1,0));
        if(Vector3.Dot(right, target) < 0)
            direction = -1;
        else
            direction = 1;

        angle *= Mathf.Rad2Deg;

        if(angle > 120)
            throw new AngleValueException("Angle is greater than 120 degrees");
        
        return angle * direction;
    }

    public float[] moveTowardsTarget_Perpendicular(Vector3 target){

        float distance = Vector3.Distance(target, joint1.transform.position);
        // float distance = Vector3.Distance(new Vector3(target.x,target.y,0), new Vector3(joint1.transform.position.x, (joint1.transform.position.y+0.25f), 0)); // 0,25 to 1/4 joint1, o ktory jest podniesiona podstawa
        float x2 = (Mathf.Pow(UPPER_ARM_LENGTH, 2) - Mathf.Pow(LOWER_ARM_LENGTH, 2) + Mathf.Pow(distance - MEDIUM_ARM_LENGTH, 2)) /
                    (2 * (distance - MEDIUM_ARM_LENGTH));
        float x1 = distance - MEDIUM_ARM_LENGTH - x2;

        // Debug.Log(distance);
        // Debug.Log(x1+" "+MEDIUM_ARM_LENGTH+" "+x2);

        float alfa = Mathf.Acos(x1 / LOWER_ARM_LENGTH)*Mathf.Rad2Deg;
        float beta = Mathf.Acos(x2 / UPPER_ARM_LENGTH)*Mathf.Rad2Deg;

        // Debug.Log(alfa+" "+beta);
        return new float[]{alfa, 90-alfa, 90-beta};
    }




    public void calculateAllData(){
        try{
            angle = rotateTowardsTarget(obiekt.transform.position);
            if(angle < 0){
                rotateDirection = 1;
                angle*=-1;
            }
        }catch(AngleValueException ave) { log(ave.Message); }

        armAngles = moveTowardsTarget_Perpendicular(obiekt.transform.position);
    }

    public static void log(string txt, bool shouldShow = true){
        if(shouldShow) Debug.Log(txt);
    }
    public static void log(float txt, bool shouldShow = true){
        if(shouldShow) Debug.Log(txt);
    }



}
