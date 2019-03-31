using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Algorytm;

public class TargetMoving : MonoBehaviour
{

    public GameObject obiekt;
    float movementSpeed = 40f;
    // float length;

    void Start()
    {
        obiekt = GameObject.Find("Obiekt");
        // length = GameObject.Find("Chwytak").transform.position.y;
    }

    void Update()
    {
        // if(canChangeObjekt == 0b1_0000){
        if(Input.GetKey(KeyCode.UpArrow)){
            if((obiekt.transform.position + Vector3.forward*Time.deltaTime*movementSpeed).magnitude <= MAX_LENGTH)
                obiekt.transform.position += Vector3.forward*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            if((obiekt.transform.position + -1*Vector3.forward*Time.deltaTime*movementSpeed).magnitude <= MAX_LENGTH)
                obiekt.transform.position += -1*Vector3.forward*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            if(( obiekt.transform.position + Vector3.right*Time.deltaTime*movementSpeed).magnitude <= MAX_LENGTH)
                obiekt.transform.position += Vector3.right*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            if((obiekt.transform.position + -1*Vector3.right*Time.deltaTime*movementSpeed).magnitude <= MAX_LENGTH)
                obiekt.transform.position += -1*Vector3.right*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.Space)){
            if((obiekt.transform.position + Vector3.up*Time.deltaTime*movementSpeed).magnitude <= MAX_LENGTH)
                obiekt.transform.position += Vector3.up*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.LeftControl)){
            if((obiekt.transform.position.y - Vector3.up.y*Time.deltaTime*movementSpeed) >= obiekt.GetComponent<Renderer>().bounds.size.y/2){
                obiekt.transform.position += -1*Vector3.up*Time.deltaTime*movementSpeed;
            }else{
                obiekt.transform.position = new Vector3(obiekt.transform.position.x, 0+obiekt.GetComponent<Renderer>().bounds.size.y/2, obiekt.transform.position.z);
            }
        }
        // }
    }
}
