using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mouseScrollDelta.y);
        Camera mainCam = gameObject.GetComponent<Camera>();
        if(Input.mouseScrollDelta.y < 0) 
        {
            if(mainCam.orthographicSize < 100) 
            {
                mainCam.orthographicSize += 2;
            }
        } 
        else if(Input.mouseScrollDelta.y > 0)
        {
            if(mainCam.orthographicSize > 40) 
            {
                mainCam.orthographicSize += -2;
            }
        }
    }
}
