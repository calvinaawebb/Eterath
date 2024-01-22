using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Camera mainCam = gameObject.GetComponent<Camera>();
        Debug.Log("scroll: " + Input.mouseScrollDelta.y );
        if(Input.mouseScrollDelta.y < 0) 
        {
            mainCam.transform.position += new Vector3(0, 0, 10);
            /*if(mainCam.transform.position.z < 0) 
            {
                mainCam.transform.position += new Vector3(0, 0, 10);
            }*/
        } 
        if(Input.mouseScrollDelta.y > 0)
        {
            mainCam.transform.position += new Vector3(0, 0,-10);
            /*if (mainCam.transform.position.z > -300) 
            {
                mainCam.transform.position += new Vector3(0,0,-10);
            }*/
        }
    }
}
