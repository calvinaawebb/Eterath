using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraZoom : MonoBehaviour
{
    // Update is called once per frame
    public RenderTexture pixelres;
    public RenderTexture temp;
    public Image outputImage;
    public float intensity;
    public int upperlimit;
    public int lowerlimit;

    private void Start()
    {
        upperlimit = -490;
        lowerlimit = -160;
        pixelres.Release();
        pixelres.height = 1080;
        pixelres.width = 1920;
        pixelres.Create();
    }
    void Update()
    {
        Camera mainCam = gameObject.GetComponent<Camera>();
        Debug.Log("scroll: " + Input.mouseScrollDelta.y );
        if(Input.mouseScrollDelta.y < 0 && mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D.z >= upperlimit) 
        {
            mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0, 0, -10);
            Debug.Log("wihfowihfoijwfjwofi: " + (((-350) - mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D.z) * (-1) + 350) + " " + gameObject.transform.position.z);
            float temp = (mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D.z / 300) * intensity * -1;
            Debug.Log("testing: " + gameObject.transform.position.z + " " + temp);
            pixelres.Release();
            pixelres.height = (int)(1080 * (temp));
            pixelres.width = (int)(1920 * (temp));
            pixelres.Create();
            /*if(mainCam.transform.po*//*sition.z < 0) 
            {
                mainCam.transform.position += new Vector3(0, 0, 10);
            }*/
        }
        if (Input.mouseScrollDelta.y > 0 && mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D.z <= lowerlimit)
        {
            mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0, 0, 10);
            float temp = (mainCam.transform.GetComponent<RectTransform>().anchoredPosition3D.z / 300) * intensity * -1;
            Debug.Log("testing: " + gameObject.transform.position.z + " " + temp);
            pixelres.Release();
            pixelres.height = (int)(1080 * (temp));
            pixelres.width = (int)(1920 * (temp));
            pixelres.Create();
            /*if (mainCam.transform.position.z > -300) 
            {
                mainCam.transform.position += new Vector3(0,0,-10);
            }*/
        }
    }
}
