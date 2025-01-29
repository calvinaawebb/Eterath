using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScroll : MonoBehaviour
{
    public GameObject panelParent;
    public GameObject rightarrowObject;
    public GameObject leftarrowObject;
    public RectTransform panelRectTransform;
    public int indexCounter;
    public int maxIndex;
    public int minIndex;
    // Start is called before the first frame update
    private void Start()
    {
        indexCounter = 0;
        maxIndex = 1;
        minIndex = 0;
        panelRectTransform = panelParent.GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (indexCounter == maxIndex)
        {
            rightarrowObject.SetActive(false);
        }
        if (indexCounter != minIndex)
        {
            leftarrowObject.SetActive(true);
        }
        if (indexCounter != maxIndex)
        {
            rightarrowObject.SetActive(true);
        }
        if (indexCounter == minIndex)
        {
            leftarrowObject.SetActive(false);
        }
    }

    public void leftArrow()
    {
        if (indexCounter >= maxIndex) 
        {
            panelRectTransform.anchoredPosition += new Vector2(239.5f, 0f);
            indexCounter--;
        }
    }

    public void rightArrow()
    {
        if (indexCounter < maxIndex)
        {
            panelRectTransform.anchoredPosition -= new Vector2(239.5f, 0f);
            indexCounter++;
        }
    }
}
