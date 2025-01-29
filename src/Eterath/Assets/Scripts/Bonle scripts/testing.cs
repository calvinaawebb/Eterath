using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public GameObject testingText;
    // Start is called before the first frame update
    void Start()
    {
        testingText.SetActive(false);
    }

    void OnMouseOver() 
    {
        Debug.Log("MOUSE IN");
        testingText.SetActive(true);
    }

    // Update is called once per frame
    void OnMouseExit()
    {
        testingText.SetActive(false);
    }
}
