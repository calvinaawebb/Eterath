using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabOraganization : MonoBehaviour
{
    public GameObject activeCanvas;
    public List<GameObject> canvases = new List<GameObject>();
    public List<GameObject> tabs = new List<GameObject>();

    public void Start()
    {
        foreach (GameObject canvas in canvases) 
        {
            canvas.SetActive(false);
        }
        activeCanvas = canvases[0];
        activeCanvas.SetActive(true);   
    }

    public void setCanvas(int indexCan)
    {
        activeCanvas.SetActive(false);
        activeCanvas = canvases[indexCan];
        activeCanvas.SetActive(true);
    }
}
