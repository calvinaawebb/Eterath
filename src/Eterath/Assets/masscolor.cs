using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masscolor : MonoBehaviour
{
    // Start is called before the first frame update
    public Material color;
    public Material temp;
    void Start()
    {
        foreach (Transform child in gameObject.transform) 
        {
            float offset = Random.Range(0.25f, 0.3f);
            Color tempc = new Color();
            tempc.g = offset;
            tempc.r = offset;
            tempc.b = offset;
            child.gameObject.GetComponent<MeshRenderer>().material.color = tempc;
            child.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Smoothness", 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
