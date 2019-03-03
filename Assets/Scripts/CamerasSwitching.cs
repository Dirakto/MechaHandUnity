using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamerasSwitching : MonoBehaviour
{
    private Camera[] cameras;
    private int currentMax;
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentMax = Camera.allCamerasCount;
        cameras = new Camera[currentMax];
        Camera.GetAllCameras(cameras);

        currentIndex = 0;
        cameras[0].gameObject.SetActive(true);
        for (int i = 1; i < currentMax; i++)
            cameras[i].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(cameras.ToString());
            currentIndex++;
            if (currentIndex < currentMax)
            {
                cameras[currentIndex - 1].gameObject.SetActive(false);
                cameras[currentIndex].gameObject.SetActive(true);
            }
            else
            {
                cameras[currentIndex - 1].gameObject.SetActive(false);
                currentIndex = 0;
                cameras[currentIndex].gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentIndex--;
            if (currentIndex > -1)
            {
                cameras[currentIndex + 1].gameObject.SetActive(false);
                cameras[currentIndex].gameObject.SetActive(true);
            }
            else
            {
                cameras[currentIndex + 1].gameObject.SetActive(false);
                currentIndex = currentMax-1;
                cameras[currentIndex].gameObject.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
        SceneManager.LoadScene("SampleScene") ;
        }
    }
}
