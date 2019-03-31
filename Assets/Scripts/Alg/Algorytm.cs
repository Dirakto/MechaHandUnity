using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorytm : MonoBehaviour
{

	public readonly static float LOWER_ARM_LENGTH = 12.1678f+0.5f; // +korekta jointa
	public readonly static float MEDIUM_ARM_LENGTH = 12.282f+0.5f; // +korekta jointa
	public readonly static float UPPER_ARM_LENGTH = 14.5079f+0.25f+(0.4835968f);// +korekta jointa i chwytaka

    public readonly static float MAX_LENGTH = LOWER_ARM_LENGTH+MEDIUM_ARM_LENGTH+UPPER_ARM_LENGTH;

        // Debug.Log(joint2.transform.position.y - joint1.transform.position.y);
        // Debug.Log(joint3.transform.position.y - joint2.transform.position.y);
        // Debug.Log(chwytak.transform.position.y - joint3.transform.position.y);

    // public static 

    public MessageShower ms;

    public GameObject joint1;
    public GameObject joint2;
    public GameObject joint3;
    public GameObject dolne;
    public GameObject srodkowe;
    public GameObject gorne;
    public GameObject baza;
    public GameObject obiekt;
    public GameObject chwytak;

    float targetAngle = 0;
    float startAtZero = 0;
    float rotateDirection = 1;

    float[] armAngles;
    float[] currentArmAngles = new float[]{90, 90, 90};
    float[] moveDirection = new float[]{1, 1, 1};

    // private bool isReady = false;
    // public static int canChangeObjekt = 0b0_0001;

    void Start()
    {
        ms = MessageShower.instanceOf(GameObject.Find("Warning"));

        joint1 = GameObject.Find("Joint1");
        joint2 = GameObject.Find("Joint2");
        joint3 = GameObject.Find("Joint3");
        dolne = GameObject.Find("Dolne");
        srodkowe = GameObject.Find("Srodkowe");
        gorne = GameObject.Find("Gorne");
        baza = GameObject.Find("Baza");
        obiekt = GameObject.Find("Obiekt");
        chwytak = GameObject.Find("Chwytak");

        try{
            calculateAllData();
        }catch(Exception ave){ 
            Debug.Log(ave.Message);
            // armAngles = new float[]{1000f, 1000f, 1000f};
            ms.text = ave.Message;
            // isReady = false;
        }
    }

    void Update()
    {
        // if(isReady){
        float speed = 0.25f;
        if(startAtZero <  targetAngle){
            joint1.transform.RotateAround(joint1.transform.position, new Vector3(0, 1, 0), rotateDirection*speed);
            startAtZero+=speed;
        }else{
            // canChangeObjekt = canChangeObjekt << 1;
        }

        if(currentArmAngles[0] > armAngles[0]){
            dolne.transform.RotateAround(joint1.transform.position, joint1.transform.right, moveDirection[0]*speed);
            currentArmAngles[0]-=speed;
        }else{
            // canChangeObjekt = canChangeObjekt << 1;
        }
        if(currentArmAngles[1] > armAngles[1]){
            srodkowe.transform.RotateAround(joint2.transform.position, joint2.transform.right, moveDirection[1]*speed);
            currentArmAngles[1]-=speed;
        }else{
            // canChangeObjekt = canChangeObjekt << 1;
        }
        if(currentArmAngles[2] > armAngles[2]){
            gorne.transform.RotateAround(joint3.transform.position, joint3.transform.right, moveDirection[2]*speed);
            currentArmAngles[2]-=speed;
        }else{
            // canChangeObjekt = canChangeObjekt << 1;
        }

        if(Input.GetKeyDown(KeyCode.B)){
            try{
                calculateAllData();
                startAtZero = 0;
                // canChangeObjekt = 0b0_0001;
            }catch(Exception ave) {
                Debug.Log(ave.Message);
                ms.text = ave.Message;
                // isReady = false;
            }
        }
        // }
        // if(canChangeObjekt == 0b1_0000){
        //     isReady = false;

        // }
    }

    public float rotateTowardsTarget(Vector3 target)
    {
        // os Z jest Polnoca

        float cosAlfa = ((target.x*Vector3.forward.x) + (target.z*Vector3.forward.z)) /
                        Mathf.Sqrt( Mathf.Pow(target.x, 2)+Mathf.Pow(target.z,2));
        float angle = Mathf.Acos(cosAlfa);// % Mathf.PI);

        rotateDirection = 1;
        if(target.x * joint1.transform.forward.x >= 0){
            Vector3 right = Vector3.Cross(-1*joint1.transform.forward, new Vector3(0,1,0));
            if(Vector3.Dot(right, target) < 0)
                rotateDirection = -1;
        }else{
            if(target.x < 0)
                rotateDirection = -1;
        }

        angle *= Mathf.Rad2Deg;

        if(angle > 120)
            throw new AngleValueException("Angle is greater than 120 degrees");
        
        return angle;
    }

    public float[] moveTowardsTarget_Perpendicular(Vector3 target){

        float angleCorrection = 0;
        if(target.y > obiekt.GetComponent<Renderer>().bounds.size.y/2){
            angleCorrection = Mathf.Asin( (target.y-joint1.transform.position.y)/Vector3.Distance(target, joint1.transform.position) )*Mathf.Rad2Deg;
        }else{
            angleCorrection = -Mathf.Acos( Mathf.Sqrt( Mathf.Pow(target.x,2)+Mathf.Pow(target.z,2))/Vector3.Distance(target, joint1.transform.position) )*Mathf.Rad2Deg;
        }
        // if(target.y < joint1.transform.position.y){
        //     angleCorrection =  Mathf.Asin( (target.y-joint1.transform.position.y)/Vector3.Distance(target, joint1.transform.position) )*Mathf.Rad2Deg;
        // }else{
        //     angleCorrection = Mathf.Asin( target.y/Vector3.Distance(target, joint1.transform.position) )*Mathf.Rad2Deg;
        // }


        float distance = Vector3.Distance(target, joint1.transform.position);
        float x2 = (Mathf.Pow(UPPER_ARM_LENGTH, 2) - Mathf.Pow(LOWER_ARM_LENGTH, 2) + Mathf.Pow(distance - MEDIUM_ARM_LENGTH, 2)) /
                    (2 * (distance - MEDIUM_ARM_LENGTH));
        float x1 = distance - MEDIUM_ARM_LENGTH - x2;

        float alfa = Mathf.Acos(x1 / LOWER_ARM_LENGTH)*Mathf.Rad2Deg;
        float beta = Mathf.Acos(x2 / UPPER_ARM_LENGTH)*Mathf.Rad2Deg;

        return new float[]{alfa+angleCorrection, 90-alfa, 90-beta};
    }




    public void calculateAllData(){

        if(Vector3.Distance(obiekt.transform.position, joint1.transform.position) > MAX_LENGTH){
            Debug.Log(Vector3.Distance(obiekt.transform.position, joint1.transform.position));
            Debug.Log(MAX_LENGTH);
            throw new Exception("Target too far");
        }

            float angle = rotateTowardsTarget(obiekt.transform.position);

            if(joint1.transform.forward.x*obiekt.transform.position.x < 0){
                targetAngle = angle + Vector3.Angle(joint1.transform.forward, Vector3.forward);
            }else{
                if((obiekt.transform.position.x < 0 && rotateDirection == 1)||(obiekt.transform.position.x > 0 && rotateDirection == -1)){
                    targetAngle = Vector3.Angle(joint1.transform.forward, Vector3.forward) - angle;
                }else{
                    targetAngle = angle - Vector3.Angle(joint1.transform.forward, Vector3.forward);
                }
            }

            armAngles = moveTowardsTarget_Perpendicular(obiekt.transform.position);
            // Debug.Log(armAngles[0]+" "+armAngles[1]+" "+armAngles[2]);
            for(int i = 0; i<armAngles.Length; i++)
                if(armAngles[i] > 90)
                    throw new AngleValueException("Arm "+i+" has an angle greater than 90");
                else if(Double.IsNaN(armAngles[i]))
                    throw new FormatException("Number is NaN");
                

            for(int i = 0; i<armAngles.Length; i++){
                if(currentArmAngles[i] < armAngles[i]){
                    currentArmAngles[i] = armAngles[i]+(armAngles[i]-currentArmAngles[i]);
                    moveDirection[i] = -1;
                }else{
                    moveDirection[i] = 1;
                }
            // isReady = true;
        }


    }


}
