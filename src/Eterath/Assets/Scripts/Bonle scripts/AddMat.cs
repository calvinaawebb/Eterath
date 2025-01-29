using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddMat : MonoBehaviour
{
    public GameObject parentObj;
    public Material outline;
    public Material bone;
    //public Material[] inst = new MeshRenderer[] {outline, bone};
    public MeshRenderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        renderers = parentObj.GetComponentsInChildren<MeshRenderer>();
        Material[] inst = {outline, bone};
        foreach (MeshRenderer renderer in renderers) 
        {
            renderer.materials = inst;
            Debug.Log("Hello");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
