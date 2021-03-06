﻿using UnityEngine;

using static Algorytm;

public class Movement
{

    private static float obiektCorrection = GameObject.Find("Obiekt").GetComponent<Renderer>().bounds.size.y/2;

    /* Rotacja wokół osi Y (dodatnia liczba oznacza kierunek: dodatnia - lewo, ujemna - prawo) */
    public static float rotateTowardsTarget(Vector3 target, GameObject joint)
    {
        // os Z jest Polnoca
        float cosAlfa = ((target.x*Vector3.forward.x) + (target.z*Vector3.forward.z)) /
                        Mathf.Sqrt( Mathf.Pow(target.x, 2)+Mathf.Pow(target.z,2));
        float angle = Mathf.Acos(cosAlfa);// % Mathf.PI);

        int rotateDirection = 1;
        if(target.x * joint.transform.forward.x >= 0){
            Vector3 right = Vector3.Cross(-1*joint.transform.forward, new Vector3(0,1,0));
            if(Vector3.Dot(right, target) < 0)
                rotateDirection = -1;
        }else{
            if(target.x < 0)
                rotateDirection = -1;
        }

        angle *= Mathf.Rad2Deg;

        if(angle > 120)
            throw new AngleValueException("Angle is greater than 120 degrees");
        
        return angle*rotateDirection;
    }

    public float[] moveTowardsTarget_Perpendicular(Vector3 target, Vector3 joint1position){

        float angleCorrection = 0;
        if(target.y > obiektCorrection){
            angleCorrection = Mathf.Asin( (target.y-joint1position.y)/Vector3.Distance(target, joint1position) )*Mathf.Rad2Deg;
        }else{
            angleCorrection = -Mathf.Acos( Mathf.Sqrt( Mathf.Pow(target.x,2)+Mathf.Pow(target.z,2))/Vector3.Distance(target, joint1position) )*Mathf.Rad2Deg;
        }

        float distance = Vector3.Distance(target, joint1position);
        float x2 = (Mathf.Pow(UPPER_ARM_LENGTH, 2) - Mathf.Pow(LOWER_ARM_LENGTH, 2) + Mathf.Pow(distance - MEDIUM_ARM_LENGTH, 2)) /
                    (2 * (distance - MEDIUM_ARM_LENGTH));
        float x1 = distance - MEDIUM_ARM_LENGTH - x2;

        float alfa = Mathf.Acos(x1 / LOWER_ARM_LENGTH)*Mathf.Rad2Deg;
        float beta = Mathf.Acos(x2 / UPPER_ARM_LENGTH)*Mathf.Rad2Deg;

        return new float[]{alfa+angleCorrection, 90-alfa, 90-beta};
    }



}
