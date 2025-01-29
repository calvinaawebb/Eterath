using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectClick : MonoBehaviour
{
    public Text outp;
    public Text outpB;
    public Material selected;
    public Material unselected;
    private GameObject prevSelected;
    public Transform hitChild;
    public bool didStart = false;

    private void Start()
    {
        outp.text = "Click on a bone!";
        outpB.text = "What you should type will show up here";
        didStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Used to tell if you clicked a button in the bone index.
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (didStart) 
                {
                    outp.text = "";
                    outpB.text = "";
                    didStart = false;
                }
                if (outp.text != "") 
                {
                    prevSelected.transform.GetComponent<MeshRenderer>().material = unselected;
                }   
                if (hit.transform.name.Length != 0) {
                    String[] words = hit.transform.name.Split("_");  
                    outp.text = words[0];
                    outpB.text = "You Type: " + words[1];
                    hit.transform.GetComponent<MeshRenderer>().material = selected;
                    /*
                    for (int i = 0; i < hit.transform.childCount; i++) 
                    {
                        hitChild = hit.transform.GetChild(i);
                        hitChild.transform.GetComponent<MeshRenderer>().material = selected;
                    }
                    */
                    prevSelected = hit.transform.gameObject;   
                }      
            }  
        }  
    }
}
