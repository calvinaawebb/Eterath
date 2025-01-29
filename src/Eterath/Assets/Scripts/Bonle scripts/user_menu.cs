using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class user_menu : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Vector3 scaleChange;
    public Vector3 offsetChange;
    public RectTransform m_RectTransform;
    public RectTransform t_RectTransform;
    public GameObject menu_text;
    public Boolean upDown;
    void Start()
    {
        scaleChange = new Vector3(0.25f,0.25f,0.25f);
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        //t_RectTransform = menu_text.GetComponent<RectTransform>();
        upDown = true;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ExtendMenu() 
    {
        if (upDown)
        {
            m_RectTransform.transform.localPosition += offsetChange;
            upDown = false;
        }
        else
        {
            m_RectTransform.transform.localPosition -= offsetChange;
            upDown = true;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        ExtendMenu();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_RectTransform.sizeDelta += new Vector2(15f, 15f);
        //t_RectTransform.sizeDelta += new Vector2(15f, 15f);
        menu_text.transform.localScale += scaleChange;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_RectTransform.sizeDelta -= new Vector2(15f, 15f);
        //t_RectTransform.sizeDelta -= new Vector2(15f, 15f);
        menu_text.transform.localScale -= scaleChange;
    }
}
