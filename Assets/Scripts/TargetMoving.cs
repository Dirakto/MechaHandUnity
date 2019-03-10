using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obiekt;
    float movementSpeed = 40f;

    void Start()
    {
        obiekt = GameObject.Find("Obiekt");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            obiekt.transform.position += Vector3.forward*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            obiekt.transform.position += -1*Vector3.forward*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            obiekt.transform.position += Vector3.right*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            obiekt.transform.position += -1*Vector3.right*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.Space)){
            obiekt.transform.position += Vector3.up*Time.deltaTime*movementSpeed;
        }
        if(Input.GetKey(KeyCode.LeftControl)){
            if((obiekt.transform.position.y - Vector3.up.y*Time.deltaTime*movementSpeed) >= obiekt.GetComponent<Renderer>().bounds.size.y/2){
                obiekt.transform.position += -1*Vector3.up*Time.deltaTime*movementSpeed;
            }else{
                obiekt.transform.position = new Vector3(obiekt.transform.position.x, 0+obiekt.GetComponent<Renderer>().bounds.size.y/2, obiekt.transform.position.z);
            }
        }
    }
}
