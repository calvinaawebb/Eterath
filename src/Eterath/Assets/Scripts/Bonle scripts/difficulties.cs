using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using TMPro;

public class difficulties : MonoBehaviour
{
    public Text inp;
    public GameObject inpObject;
    public string[] difficultyList = new string[] {"EASY", "NORMAL", "HARD"};
    public ArrayList hats;
    public GameObject hatParent;
    public GameObject hatPoint;
    public int current;
    public UserData userdata;
    public GameSaver data;
    public int indexSaveEquippedHat;
    public int indexSaveEquippedBone;
    public colorBones colorBonesDiff;
    public bool isInstance = false;

    void Start()
    {
        hats = new ArrayList();
        if (!isInstance) 
        {
            inp = inpObject.GetComponent<Text>();
        }
        Debug.Log("THIS BETTER BE RIGHT: " + hatParent);
        data = userdata.GetSaver();
        foreach (string[] stat in data.stats) 
        {
            if (gameObject.transform.parent.name == "HUMAN")
            {
                if (stat[1] == "Equipped Hat HUMAN") 
                {
                    indexSaveEquippedHat = data.stats.IndexOf(stat);
                }
            }
            else if (gameObject.transform.parent.name == "TREX")
            {
                if (stat[1] == "Equipped Hat TREX")
                {
                    indexSaveEquippedHat = data.stats.IndexOf(stat);
                }
            }
            else if (gameObject.transform.parent.name == "CAT")
            {
                if (stat[1] == "Equipped Hat CAT")
                {
                    indexSaveEquippedHat = data.stats.IndexOf(stat);
                }
            }

            if (gameObject.transform.parent.name == "HUMAN")
            {
                if (stat[1] == "Equipped Bone HUMAN")
                {
                    indexSaveEquippedBone = data.stats.IndexOf(stat);
                }
            }
            else if (gameObject.transform.parent.name == "TREX")
            {
                if (stat[1] == "Equipped Bone TREX")
                {
                    indexSaveEquippedBone = data.stats.IndexOf(stat);
                }
            }
            else if (gameObject.transform.parent.name == "CAT")
            {
                if (stat[1] == "Equipped Bone CAT")
                {
                    indexSaveEquippedBone = data.stats.IndexOf(stat);
                }
            }
        }

        
        foreach (string[] stat in data.stats)
        {
            if (stat[2] == "true" && data.currentUser == stat[0])
            {
                if (((string[])data.stats[indexSaveEquippedBone])[2] == "Null")
                {
                    ((string[])data.stats[indexSaveEquippedBone])[2] = stat[1];
                    userdata.SendSaver(data);
                }
                break;
            }
        }
        


        if (hatParent != null) 
        {
            Debug.Log("DOES THIS RUN ??");
            data = userdata.GetSaver();
            Debug.Log(((string[])data.stats[indexSaveEquippedHat])[2]);
            foreach (Transform child in hatParent.transform)
            {
                hats.Add(child.gameObject);
                Debug.Log("WHAT IS GOING ON WITH SORTING: " + child.gameObject + " HEFUHUEHFUEUEFHU: " + hats.Count);
            }

            // Find first hat that the user has in the stats list and set it as the equipped hat
            for (int i = 0; i < hats.Count; i++)
            {
                foreach (string[] stat in data.stats)
                {
                    if (stat[1] == ((GameObject)hats[i]).name && Int32.Parse(stat[2]) == 1 && data.currentUser == stat[0])
                    {
                        if (((string[])data.stats[indexSaveEquippedHat])[2] == "Null")
                        {
                            ((string[])data.stats[indexSaveEquippedHat])[2] = stat[1];
                            userdata.SendSaver(data);
                        }
                        break;
                    }
                }
            }

            data = userdata.GetSaver();
            Debug.Log("HUUUUUUUUUHHHHHHHH????: " + hats.Count);
            foreach (GameObject hat in hats)
            {
                Debug.Log("WHAT THE FUCK HELLO ???? 33333333333333");
                if (hat.name == ((string[])data.stats[indexSaveEquippedHat])[2])
                {
                    hat.SetActive(true);
                    Debug.Log("WHAT THE FUCK HELLO ????");
                }
                else
                {
                    hat.SetActive(false);
                    Debug.Log("WHAT THE FUCK HELLO ???? 22222222222");
                }
            }
        }
    }

    // Used to inumerate up in the different difficulties in the difficulty selectors.
    public void inumeraterUp() 
    {
        for (int i=0;i<difficultyList.Length;i++) 
        {
            if (difficultyList[i] == inp.text) 
            {
                current = i+1;
            }
        }
        try 
        {
            inp.text = difficultyList[current];
        }
        catch (IndexOutOfRangeException e) 
        {
            inp.text = difficultyList[0];
        }
    }

    // Used to inumerate down in the different difficulties in the difficulty selectors.
    public void inumeraterDown() 
    {
        for (int i=0;i<difficultyList.Length;i++) 
        {
            if (difficultyList[i] == inp.text) 
            {
                current = i-1;
            }
        }
        try 
        {
            inp.text = difficultyList[current];
        }
        catch (IndexOutOfRangeException e) 
        {
            inp.text = difficultyList[difficultyList.Length-1];
        }
    }

    // Used to inumerate up in the different hats in the hat selectors.
    public void inumeraterUpHat()
    {
        data = userdata.GetSaver();
        int index = 0;
        int originalIndex = 0;
        int initialIndex = 0;
        int indexOfStart = 0;
        int indexOfEnd = 0;
        bool beenDone = false;

        for (int i = 0; i < data.stats.Count; i++)
        {
            if (((string[])data.stats[i])[1] == ((string[])data.stats[indexSaveEquippedHat])[2] && ((string[])data.stats[i])[0] == data.currentUser)
            {
                index = i;//36
                initialIndex = i;
            }
            if (((string[])data.stats[i])[1] == "Hat of Nothing" && ((string[])data.stats[i])[0] == data.currentUser) 
            {
                indexOfStart = i;//35
            }
        }

        for (int i = data.stats.Count-1; i >= 0; i--)
        {
            if (((string[])data.stats[i])[2] == "1" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfEnd = i;//40
                break;
            }
        }
        Debug.Log("THIS IS WHAT THIS IS IN THE WORLD OF THIS: " + ((string[])data.stats[indexOfEnd])[1]);

        int breakOut = 0;
        originalIndex = index;
        for (int i = index; i < indexOfEnd+1; i++)
        {
            Debug.Log("this: " + index + " should equal this: " + (data.stats.Count-1));
            Debug.Log("ME WHEN I INDEX ON THAT THANG: " + index + " " + i + " " + indexOfEnd);
            //40
            if (i == indexOfEnd && breakOut == 0)
            {
                i = indexOfStart;
                beenDone = true;
                Debug.Log("man got reset lol2 " + indexOfStart);
            }
            if (index != indexOfEnd && beenDone == false)
            {
                i = initialIndex + 1;//37
                beenDone = true;
                Debug.Log("GOIN IN");
            } /*else if (index == indexOfEnd)
            {
                i = indexOfStart;
                beenDone = true;
                Debug.Log("man got reset lol " + indexOfStart);
            }*/
            
            Debug.Log("wqwedfqwdqwd: " + i);
            Debug.Log("WHAT IN THE MOTHER OF FUCXKING SHIT IS GOING ON: " + ((string[])data.stats[i])[2] + " " + ((string[])data.stats[i])[1]);
            if (((string[])data.stats[i])[2] == "1") //&& ((string[])data.stats[index])[1] != ((string[])data.stats[indexSaveEquippedHat])[2]) 
            {
                index = i;
                Debug.Log(((string[])data.stats[index])[1]);
                Debug.Log("EGWEWEFGWEFWEFWEFWEFEW: " + ((string[])data.stats[indexSaveEquippedHat])[2]);
                break;
            }

            breakOut++;
            if (breakOut > 200)
            {
                break;
            }
            /*if (i == data.stats.Count - 1)
            {
                i = 14;
            }*/
        }

        for (int i = 0; i < hats.Count; i++)
        {
            if (((GameObject)hats[i]).name == ((string[])data.stats[indexSaveEquippedHat])[2])
            {
                initialIndex = i;
            }
            if (((GameObject)hats[i]).name == ((string[])data.stats[index])[1])
            {
                index = i;
            }
        }

        ((GameObject)hats[initialIndex]).SetActive(false);
        ((GameObject)hats[index]).SetActive(true);
        ((string[])data.stats[indexSaveEquippedHat])[2] = ((GameObject)hats[index]).name;
        userdata.SendSaver(data);
    }

    // Used to inumerate down in the different hats in the hat selectors.
    public void inumeraterDownHat()
    {
        data = userdata.GetSaver();
        int index = 0;
        int initialIndex = 0;
        int indexOfStart = 0;
        int indexOfEnd = 0;

        for (int i = 0; i < data.stats.Count; i++)
        {
            if (((string[])data.stats[i])[1] == ((string[])data.stats[indexSaveEquippedHat])[2] && ((string[])data.stats[i])[0] == data.currentUser)
            {
                index = i;
            }
            if (((string[])data.stats[i])[1] == "Hat of Nothing" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfStart = i;
            }
            if (((string[])data.stats[i])[1] == "Hat of Seige" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfEnd = i;
            }
        }

        for (int i = index - 1; i < data.stats.Count; i--)
        {
            Debug.Log("wqwedfqwdqwd: " + i);
            if (i == indexOfStart-1)
            {
                i = indexOfEnd;
            }
            if (((string[])data.stats[i])[2] == "1") //&& ((string[])data.stats[index])[1] != ((string[])data.stats[indexSaveEquippedHat])[2]) 
            {
                index = i;
                Debug.Log(((string[])data.stats[index])[1]);
                Debug.Log("EGWEWEFGWEFWEFWEFWEFEW: " + ((string[])data.stats[indexSaveEquippedHat])[2]);
                break;
            }
        }

        for (int i = 0; i < hats.Count; i++)
        {
            if (((GameObject)hats[i]).name == ((string[])data.stats[indexSaveEquippedHat])[2])
            {
                initialIndex = i;
            }
            if (((GameObject)hats[i]).name == ((string[])data.stats[index])[1])
            {
                index = i;
            }
        }

        ((GameObject)hats[initialIndex]).SetActive(false);
        ((GameObject)hats[index]).SetActive(true);
        ((string[])data.stats[indexSaveEquippedHat])[2] = ((GameObject)hats[index]).name;
        userdata.SendSaver(data);
    }

    public void inumeraterUpBone()
    {
        data = userdata.GetSaver();
        int index = 0;
        int originalIndex = 0;
        int initialIndex = 0;
        int indexOfStart = 0;
        int indexOfEnd = 0;
        bool beenDone = false;

        for (int i = 0; i < data.stats.Count; i++)
        {
            if (((string[])data.stats[i])[1] == ((string[])data.stats[indexSaveEquippedBone])[2] && ((string[])data.stats[i])[0] == data.currentUser)
            {
                index = i;//36
                initialIndex = i;
            }
            if (((string[])data.stats[i])[1] == "Bones of White" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfStart = i;//35
            }
        }

        for (int i = data.stats.Count - 1; i >= 0; i--)
        {
            if (((string[])data.stats[i])[2] == "true" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfEnd = i;//40
                break;
            }
        }
        Debug.Log("THIS IS WHAT THIS IS IN THE WORLD OF THIS: " + ((string[])data.stats[indexOfEnd])[1]);

        int breakOut = 0;
        originalIndex = index;
        for (int i = index; i < indexOfEnd + 1; i++)
        {
            Debug.Log("this: " + index + " should equal this: " + (data.stats.Count - 1));
            Debug.Log("ME WHEN I INDEX ON THAT THANG: " + index + " " + i + " " + indexOfEnd);
            //40
            if (i == indexOfEnd && breakOut == 0)
            {
                i = indexOfStart;
                beenDone = true;
                Debug.Log("man got reset lol2 " + indexOfStart);
            }
            if (index != indexOfEnd && beenDone == false)
            {
                i = initialIndex + 1;//37
                beenDone = true;
                Debug.Log("GOIN IN");
            } /*else if (index == indexOfEnd)
            {
                i = indexOfStart;
                beenDone = true;
                Debug.Log("man got reset lol " + indexOfStart);
            }*/

            Debug.Log("wqwedfqwdqwd: " + i);
            Debug.Log("WHAT IN THE MOTHER OF FUCXKING SHIT IS GOING ON: " + ((string[])data.stats[i])[2] + " " + ((string[])data.stats[i])[1]);
            if (((string[])data.stats[i])[2] == "true") //&& ((string[])data.stats[index])[1] != ((string[])data.stats[indexSaveEquippedHat])[2]) 
            {
                index = i;
                Debug.Log(((string[])data.stats[index])[1]);
                Debug.Log("EGWEWEFGWEFWEFWEFWEFEW: " + ((string[])data.stats[indexSaveEquippedBone])[2]);
                break;
            }

            breakOut++;
            if (breakOut > 200)
            {
                break;
            }
            /*if (i == data.stats.Count - 1)
            {
                i = 14;
            }*/
        }
        ((string[])data.stats[indexSaveEquippedBone])[2] = ((string[])data.stats[index])[1];
        Debug.Log("WORKING HOLY COW: " + ((string[])data.stats[indexSaveEquippedBone])[2]);
        userdata.SendSaver(data);
        colorBonesDiff.updateColors();
    }

    // Used to inumerate down in the different hats in the hat selectors.
    public void inumeraterDownBone()
    {
        data = userdata.GetSaver();
        int index = 0;
        int initialIndex = 0;
        int indexOfStart = 0;
        int indexOfEnd = 0;

        for (int i = 0; i < data.stats.Count; i++)
        {
            if (((string[])data.stats[i])[1] == ((string[])data.stats[indexSaveEquippedBone])[2] && ((string[])data.stats[i])[0] == data.currentUser)
            {
                index = i;
            }
            if (((string[])data.stats[i])[1] == "Bones of White" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfStart = i;
            }
        }

        for (int i = data.stats.Count - 1; i >= 0; i--)
        {
            if (((string[])data.stats[i])[2] == "true" && ((string[])data.stats[i])[0] == data.currentUser)
            {
                indexOfEnd = i;//40
                break;
            }
        }

        for (int i = index - 1; i < data.stats.Count; i--)
        {
            Debug.Log("wqwedfqwdqwd: " + i);
            if (i == indexOfStart - 1)
            {
                i = indexOfEnd;
            }
            if (((string[])data.stats[i])[2] == "true") //&& ((string[])data.stats[index])[1] != ((string[])data.stats[indexSaveEquippedHat])[2]) 
            {
                index = i;
                Debug.Log(((string[])data.stats[index])[1]);
                Debug.Log("EGWEWEFGWEFWEFWEFWEFEW: " + ((string[])data.stats[indexSaveEquippedBone])[2]);
                break;
            }
        }
        ((string[])data.stats[indexSaveEquippedBone])[2] = ((string[])data.stats[index])[1];
        Debug.Log("WORKING HOLY COW: " + ((string[])data.stats[indexSaveEquippedBone])[2]);
        userdata.SendSaver(data);
        colorBonesDiff.updateColors();
    }
}
