using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cheatsheetBehavior : MonoBehaviour
{
    public GameObject scrollObject;
    public TextMeshProUGUI bonePoints;
    public UserData userData;
    public GameSaver data;
    public Transform content;
    public string currentUser;
    public colorize input;
    public int initialVal;
    public Color temp;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HELLLOOOOOOOOFEOPFKEWOFKEOFKOEWFKOEFK");
        data = userData.GetSaver();
        initialVal = input.colorsforCheatsheet.Count;
        populateScoreMenu();
    }

    public void populateScoreMenu()
    {
        data = userData.GetSaver();
        Debug.Log("Hello? what");
        foreach (Transform stat in input.skeleton.transform)
        {
            Debug.Log("OQWIODIOQWI IN");
            var itemInst = Instantiate(scrollObject) as GameObject;
            itemInst.SetActive(true);
            itemInst.transform.SetParent(content, false);
            //itemInst.transform.localScale = Vector2.one;
            string[] splitParts = stat.name.Split('_');
            itemInst.transform.Find("Main").GetComponent<TMP_Text>().text = "" + splitParts[0];
            itemInst.transform.Find("Output").GetComponent<TMP_Text>().text = "?";
            foreach (string[] inputString in input.colorsforCheatsheet) 
            {
                if (inputString[0] == splitParts[1]) 
                {
                    Debug.Log("BE THE SAME PLEASE: " + inputString[2] + " " + temp);
                    ColorUtility.TryParseHtmlString(inputString[2], out temp);
                    Debug.Log("BE THE SAME PLEASE: " + temp);
                    itemInst.transform.Find("Output").GetComponent<TMP_Text>().text = inputString[1];
                    itemInst.transform.Find("Output").GetComponent<TMP_Text>().color = temp;
                }
            }
        }
    }

    public void depopulateScoreMenu()
    {
        try
        {
            for (int child = 0; child < content.transform.childCount; child++)
            {
                GameObject.Destroy(content.GetChild(child).transform.gameObject);
                //content.tra
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        data = userData.GetSaver();
        if (input.colorsforCheatsheet.Count != initialVal) 
        {
            Debug.Log("WHAT ETIHWEIRFHWEIUFHWIUEHFIHWEFHIWUEHFIUWHEFIUHWEFHIU");
            depopulateScoreMenu();
            populateScoreMenu();
        }
    }
}
