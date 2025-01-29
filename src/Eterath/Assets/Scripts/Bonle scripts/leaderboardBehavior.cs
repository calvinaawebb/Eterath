using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class leaderboardBehavior : MonoBehaviour
{
    public GameObject scrollObject;
    public TMP_Dropdown decendDrop;
    public UserData userData;
    public GameSaver data;
    public Transform content;
    public string currentUser;
    public bool isLeaderboard = false;

    public Sprite diamondTrophy;
    public Sprite platinumTrophy;
    public Sprite goldTrophy;
    public Sprite silverTrophy;
    public Sprite bronzeTrophy;
    public Image trophyImage;

    void Start()
    {
        data = userData.GetSaver();
        isLeaderboard = true;
        if (data.leaderboardOrder)
        {
            if (decendDrop.value == 0) 
            {
                isLeaderboard = false;
            }
            decendDrop.value = 0;
            Debug.Log("RAN 0");
        }
        else 
        {
            if (decendDrop.value == 1)
            {
                isLeaderboard = false;
            }
            decendDrop.value = 1;
            Debug.Log("RAN 1");
        }
        populateScoreMenu();
    }

    public void populateScoreMenu()
    {
        Debug.Log("H E L L O ??");
        Debug.Log(data.scores[0]);
        //string[] score2 = data.scores[0];
        foreach (string[] score in data.scores)
        {
            Debug.Log("N O O O O O ??");
            var itemInst = Instantiate(scrollObject) as GameObject;
            itemInst.SetActive(true);
            itemInst.transform.SetParent(content, false);
            //itemInst.transform.localScale = Vector2.one;
            itemInst.transform.Find("Guesses").GetComponent<TMP_Text>().text = "" + score[0];
            itemInst.transform.Find("Seconds").GetComponent<TMP_Text>().text = "" + score[1];
            string[] level = score[2].ToString().Split("_");
            Debug.Log("L E V E L: " + level[0]);
            itemInst.transform.Find("Level Type").GetComponent<TMP_Text>().text = "" + level[0];
            itemInst.transform.Find("Level Difficulty").GetComponent<TMP_Text>().text = "" + level[1];
            itemInst.transform.Find("User").GetComponent<TMP_Text>().text = "" + score[3];
            itemInst.transform.Find("Date").GetComponent<TMP_Text>().text = "" + score[4];
            trophyImage = itemInst.transform.Find("Trophy").GetComponent<Image>();
            switch ("" + score[5])
            {
                case "diamond":
                    trophyImage.sprite = diamondTrophy;
                    break;

                case "platinum":
                    trophyImage.sprite = platinumTrophy;
                    break;

                case "gold":
                    trophyImage.sprite = goldTrophy;
                    break;

                case "silver":
                    trophyImage.sprite = silverTrophy;
                    break;

                case "bronze":
                    trophyImage.sprite = bronzeTrophy;
                    break;
            }
        }
    }

    public void depopulateScoreMenu()
    {
        for (int child = 0; child < content.transform.childCount; child++)
        {
            GameObject.Destroy(content.GetChild(child).transform.gameObject);
            //content.tra
        }
    }

    public void orderSet() 
    {
        if (!isLeaderboard) 
        {
            Debug.Log("WE IN THIS BITCH");
            depopulateScoreMenu();
            Debug.Log("drop text: " + decendDrop.options[decendDrop.value].text);
            if (decendDrop.options[decendDrop.value].text == "Decending")
            {
                userData.setOrder(true);
            }
            else if (decendDrop.options[decendDrop.value].text == "Acending")
            {
                userData.setOrder(false);
            }
            data = userData.GetSaver();
            populateScoreMenu();
        }
        isLeaderboard = false;
    }

    void Update()
    {
        data = userData.GetSaver();
        if (data.scores.Count < data.scoreVal)
        {
            data = userData.GetSaver();
            data.scoreVal--;
            userData.SendSaver(data);
            depopulateScoreMenu();
            populateScoreMenu();
        }
    }
}
