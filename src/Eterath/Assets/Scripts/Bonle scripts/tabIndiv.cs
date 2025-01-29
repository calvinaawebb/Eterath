using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tabIndiv : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerUpHandler
{
    public TabOraganization tabSet;
    public int tabNumber;
    public Vector3 scaleChange;

    public void Start()
    {
        scaleChange = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabSet.setCanvas(tabNumber);
        gameObject.transform.localScale -= scaleChange;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.transform.localScale += scaleChange;
    }
}
