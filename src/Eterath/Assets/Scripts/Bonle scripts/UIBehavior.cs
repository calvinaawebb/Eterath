using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Vector3 scaleChange;
    public GameObject indivText;
    public RectTransform m_RectTransform;
    public static bool queriesHitTriggers;
    // Start is called before the first frame update
    void Start()
    {
        //scaleChange = new Vector3(100f,100f,100f);
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        UnityEngine.Physics2D.queriesHitTriggers = true;
        m_RectTransform.sizeDelta -= new Vector2(1f, 150f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse IN");
        //gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        //gameObject.transform.localScale += scaleChange;
        for (int i = 0; i < 30; i++)
        {
            m_RectTransform.sizeDelta += new Vector2(1f, 100 / 30f);
        }
        //m_RectTransform.sizeDelta += new Vector2(1f, 100f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLICK");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //gameObject.transform.localScale -= scaleChange;
        for (int i = 0; i < 30; i++) 
        {
            m_RectTransform.sizeDelta -= new Vector2(1f, 100/30f);
        }
        //m_RectTransform.sizeDelta -= new Vector2(1f, 100f);
        //gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
    }
}
