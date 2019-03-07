﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamerasSwitching : MonoBehaviour
{
    private Camera[] cameras;
    private int currentMax;
    private int currentIndex;
    private CameraIndex myCameraIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentMax = Camera.allCamerasCount;
        cameras = new Camera[currentMax];
        Camera.GetAllCameras(cameras);
        myCameraIndex = CameraIndex.getInstance();

        currentIndex = myCameraIndex.index;
        for (int i = 0; i < currentMax; i++)
            cameras[i].gameObject.SetActive(false);
        cameras[myCameraIndex.index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
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
            myCameraIndex.index = currentIndex;
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
            myCameraIndex.index = currentIndex;
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
        SceneManager.LoadScene("SampleScene") ;
        }
    }
}

// class CameraIndex
// {
//     private static CameraIndex _me = null;
//     public int index { get; set; }
//     private CameraIndex(int i){
//         index = 0;
//     }
//     public static CameraIndex getInstance(){
//         if(_me == null)
//             _me = new CameraIndex(0);
//         return _me;
//     }
// }
