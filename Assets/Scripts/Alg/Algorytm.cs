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


    private IAlgorithm algorithm;
    public MessageShower ms;

    public static GameObject joint1;
    public static GameObject joint2;
    public static GameObject joint3;
    public static GameObject dolne;
    public static GameObject srodkowe;
    public static GameObject gorne;
    public static GameObject baza;
    public static GameObject obiekt;
    public static GameObject chwytak;

    float targetAngle = 0;
    float startAtZero = 0;
    float rotateDirection = 1;

    float[] armAngles;
    float[] currentArmAngles = new float[]{90, 90, 90};
    float[] moveDirection = new float[]{1, 1, 1};


    private bool readyToChange = false;
    private float speed = 0.25f;

    void Start()
    {
        algorithm = new TomaszewAlgorithm();
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
            processAngles();
            readyToChange = true;
            ms.text = "";
        }catch(Exception ave){ 
            Debug.Log(ave.Message);
            ms.text = ave.Message;
        }
    }

    void Update()
    {
        if(readyToChange){
        if(startAtZero <  targetAngle){
            joint1.transform.RotateAround(joint1.transform.position, new Vector3(0, 1, 0), rotateDirection*speed);
            startAtZero+=speed;
        }

        if(currentArmAngles[0] > armAngles[0]){
            dolne.transform.RotateAround(joint1.transform.position, joint1.transform.right, moveDirection[0]*speed);
            currentArmAngles[0]-=speed;
        }
        if(currentArmAngles[1] > armAngles[1]){
            srodkowe.transform.RotateAround(joint2.transform.position, joint2.transform.right, moveDirection[1]*speed);
            currentArmAngles[1]-=speed;
        }
        if(currentArmAngles[2] > armAngles[2]){
            gorne.transform.RotateAround(joint3.transform.position, joint3.transform.right, moveDirection[2]*speed);
            currentArmAngles[2]-=speed;
        }
        }
        if(Input.GetKeyDown(KeyCode.B)){
            try{
                readyToChange = false;
                currentArmAngles = armAngles;
                startAtZero = 0;
                processAngles();
                readyToChange = true;
                ms.text = "";
            }catch(Exception ave) {
                Debug.Log(ave.Message);
                ms.text = ave.Message;
            }
        }
    }

    public void processAngles()
    {
        try{

            float[] rotateParams = algorithm.rotateTowardsTarget(obiekt.transform.position);
            float[] armAnglesTmp = algorithm.moveTowardsTarget(obiekt.transform.position);

            float angle = rotateParams[0];
            this.rotateDirection = rotateParams[1];

            if(joint1.transform.forward.x*obiekt.transform.position.x < 0){
                targetAngle = angle + Vector3.Angle(joint1.transform.forward, Vector3.forward);
            }else{
                if((obiekt.transform.position.x < 0 && rotateDirection == 1)||(obiekt.transform.position.x > 0 && rotateDirection == -1))
                    targetAngle = Vector3.Angle(joint1.transform.forward, Vector3.forward) - angle;
                else
                    targetAngle = angle - Vector3.Angle(joint1.transform.forward, Vector3.forward);               
            }

            armAngles = armAnglesTmp;
            Debug.Log(armAngles[0]+" "+armAngles[1]+" "+armAngles[2]);

            for(int i = 0; i<armAngles.Length; i++){
                if(armAngles[i] > 90)
                    throw new Exception("blad");

                if(currentArmAngles[i] < armAngles[i]){
                    currentArmAngles[i] = armAngles[i]+(armAngles[i]-currentArmAngles[i]);
                    moveDirection[i] = -1;
                }else
                    moveDirection[i] = 1;
            }


        }catch(Exception e){
            throw;
        }
    }


}
