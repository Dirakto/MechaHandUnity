using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamerasSwitching : MonoBehaviour
{
    private Camera[] cameras;
    private int currentMax;
    private int currentIndex;
    private CameraIndex myCameraIndex;
    private ObiektPosition myObiektPosition;
    private GameObject obiekt;

    void Start()
    {
        obiekt = GameObject.Find("Obiekt");
        myObiektPosition = ObiektPosition.getInstance(obiekt.transform.position);
        GameObject.Find("Obiekt").transform.position = myObiektPosition.pos;

        currentMax = Camera.allCamerasCount;
        cameras = new Camera[currentMax];
        Camera.GetAllCameras(cameras);
        myCameraIndex = CameraIndex.getInstance();

        currentIndex = myCameraIndex.index;
        for (int i = 0; i < currentMax; i++)
            cameras[i].gameObject.SetActive(false);
        cameras[myCameraIndex.index].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentIndex++;
            cameras[currentIndex - 1].gameObject.SetActive(false);
            if (currentIndex >= currentMax){
                currentIndex = 0;
            }
            cameras[currentIndex].gameObject.SetActive(true);
            myCameraIndex.index = currentIndex;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentIndex--;
            cameras[currentIndex + 1].gameObject.SetActive(false);
            if (currentIndex <= -1)
            {
                currentIndex = currentMax-1;
            }
            cameras[currentIndex].gameObject.SetActive(true);
            myCameraIndex.index = currentIndex;
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            myObiektPosition.pos = obiekt.transform.position;
            SceneManager.LoadScene("SampleScene") ;
        }
    }
}

/* Singleton do przechowywania indeksu ostatniej kamery */
class CameraIndex
{
    private static CameraIndex _me = null;
    public int index { get; set; }
    private CameraIndex(int i){
        index = 0;
    }
    public static CameraIndex getInstance(){
        if(_me == null)
            _me = new CameraIndex(0);
        return _me;
    }
}

/* Singleton do przechowywania ostatniej pozycji obiektu */
class ObiektPosition
{
    private static ObiektPosition _me = null;
    public Vector3 pos { get; set; }
    private ObiektPosition(Vector3 targetPosition){
        pos = targetPosition;
    }
    public static ObiektPosition getInstance(Vector3 targetPosition){
        if(_me == null)
            _me = new ObiektPosition(targetPosition);
        return _me;
    }
}