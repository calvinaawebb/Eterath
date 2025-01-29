using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class statsBehavior : MonoBehaviour
{
    public GameObject scrollObject;
    public TextMeshProUGUI bonePoints;
    public UserData userData;
    public GameSaver data;
    public Transform content;
    public string currentUser;

    void Start()
    {
        data = userData.GetSaver();
        /*data.OpenCounter++;
        userData.SendSaver(data);*/
        //initializeStats();
        /*if (data.stats.Count == 0)
        {
            string[] test = new string[2] { "Main Menu Opened", "" + data.OpenCounter };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played T-rex Easy Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played T-rex Normal Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played T-rex Hard Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played Cat Easy Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played Cat Normal Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played Cat Hard Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played Human Easy Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played Human Normal Mode", "0" };
            data.stats.Add(test);
            test = new string[3] { data.currentUser, "Played Human Hard Mode", "0" };
            data.stats.Add(test);
            userData.SendSaver(data);
        }
        else 
        {
            data.stats[0] = new string[2] { "Menu Opened", "" + data.OpenCounter };
        }*/
    }

    public void initializeStats(bool isStarting) 
    {
        data = userData.GetSaver();
        int i = 0;
        try
        {
            foreach (string[] stat in data.stats)
            {
                if (stat[0] == data.currentUser)
                {
                    i++;
                }
            }
            if (i == 0)
            {
                string[] test = new string[3] { data.currentUser, "Bonepoints", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Main Menu Opened", "0"};
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played T-rex Easy Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played T-rex Normal Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played T-rex Hard Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played Cat Easy Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played Cat Normal Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played Cat Hard Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played Human Easy Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played Human Normal Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Played Human Hard Mode", "0" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Equipped Hat HUMAN", "Null" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Equipped Hat TREX", "Null" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Equipped Hat CAT", "Null" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Equipped Bone HUMAN", "Null" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Equipped Bone TREX", "Null" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Equipped Bone CAT", "Null" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hat of Nothing", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hat of Hope", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hat of Sun", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hat of Despair", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hat of Love", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hareld the Top Hat", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Hat of Seige", "1" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of White", "true" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of Hope", "true" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of Despair", "true" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of Sun", "true" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of Love", "true" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of Seige", "true" };
                data.stats.Add(test);
                test = new string[3] { data.currentUser, "Bones of Hareld", "true" };
                data.stats.Add(test);
                userData.SendSaver(data);
            }
            else 
            {
                if (isStarting) 
                {
                    foreach (string[] stat in data.stats)
                    {
                        if (stat[0] == data.currentUser && stat[1] == "Main Menu Opened")
                        {
                            int temp = Int32.Parse(stat[2]);
                            stat[2] = "" + (temp + 1);
                            Debug.Log("Please work: " + stat[2]);
                        }
                    }
                    userData.SendSaver(data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        foreach (string[] stat in data.stats)
        {
            if (stat[0] == data.currentUser && stat[1] == "Bonepoints")
            {
                bonePoints.text = "" + stat[2];
            }
        }
        depopulateScoreMenu();
        populateScoreMenu();
        userData.SendSaver(data);
    }

    public void populateScoreMenu()
    {
        data = userData.GetSaver();
        int i = 0;
        foreach (string[] stat in data.stats)
        {
            if (stat[0] == data.currentUser && stat[1] != "Bonepoints") 
            {
                if (i <= 9) 
                {
                    var itemInst = Instantiate(scrollObject) as GameObject;
                    itemInst.SetActive(true);
                    itemInst.transform.SetParent(content, false);
                    //itemInst.transform.localScale = Vector2.one;
                    Debug.Log("WHAT IN THE FUCK IS GOING ON: " + stat[2]);
                    itemInst.transform.Find("Main").GetComponent<TMP_Text>().text = "" + stat[1];
                    itemInst.transform.Find("Output").GetComponent<TMP_Text>().text = "" + stat[2];
                }
                i++;
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

    void Update()
    {
        data = userData.GetSaver();
        foreach (string[] stat in data.stats)
        {
            if (stat[0] == data.currentUser && stat[1] == "Bonepoints")
            {
                bonePoints.text = "" + stat[2];
            }
        }
    }
}
