using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UIElements;
using System.Reflection.Emit;

public class UserData : MonoBehaviour
{
    public GameSaver data;
    public BinaryFormatter bf;
    public FileStream file;
    public FileStream fileAppend;
    public string[] inp;
    public string path;
    public bool decendAcend;
    public Material[,] colorVals;
    public bool isMain;
    public GameObject matParent;
    public colorBones coloring;

    /*public Material bonesoflove_base;
    public Material bonesoflove_win;
    public Material bonesoflove_closest;
    public Material bonesoflove_closer;
    public Material bonesoflove_close;
    public Material bonesoflove_far;
    public Material bonesoflove_farther;
    public Material bonesoflove_farthest;

    public Material bonesofwhite_base;
    public Material bonesofwhite_win;
    public Material bonesofwhite_closest;
    public Material bonesofwhite_closer;
    public Material bonesofwhite_close;
    public Material bonesofwhite_far;
    public Material bonesofwhite_farther;
    public Material bonesofwhite_farthest;

    public Material bonesofhope_base;
    public Material bonesofhope_win;
    public Material bonesofhope_closest;
    public Material bonesofhope_closer;
    public Material bonesofhope_close;
    public Material bonesofhope_far;
    public Material bonesofhope_farther;
    public Material bonesofhope_farthest;

    public Material bonesofsun_base;
    public Material bonesofsun_win;
    public Material bonesofsun_closest;
    public Material bonesofsun_closer;
    public Material bonesofsun_close;
    public Material bonesofsun_far;
    public Material bonesofsun_farther;
    public Material bonesofsun_farthest;

    public Material bonesofseige_base;
    public Material bonesofseige_win;
    public Material bonesofseige_closest;
    public Material bonesofseige_closer;
    public Material bonesofseige_close;
    public Material bonesofseige_far;
    public Material bonesofseige_farther;
    public Material bonesofseige_farthest;

    public Material bonesofdespair_base;
    public Material bonesofdespair_win;
    public Material bonesofdespair_closest;
    public Material bonesofdespair_closer;
    public Material bonesofdespair_close;
    public Material bonesofdespair_far;
    public Material bonesofdespair_farther;
    public Material bonesofdespair_farthest;

    public Material bonesofhareld_base;
    public Material bonesofhareld_win;
    public Material bonesofhareld_closest;
    public Material bonesofhareld_closer;
    public Material bonesofhareld_close;
    public Material bonesofhareld_far;
    public Material bonesofhareld_farther;
    public Material bonesofhareld_farthest;*/

    public Material[] childMats;
    public int numberofMats = 56;

    void Start()
    {
        childMats = new Material[numberofMats];
        colorVals = new Material[7, 8];
        Debug.Log(DateTime.Now.ToString());
        path = UnityEngine.Application.persistentDataPath + "/save.txt";
        deSerialize();
        decendAcend = data.leaderboardOrder;
        Debug.Log(data.OpenCounter);
        if (isMain) 
        {
            coloringThings();
        }
        serialization();
    }

    public void coloringThings() 
    {
        for (int child = 0; child < matParent.transform.childCount; child++)
        {
            childMats[child] = matParent.transform.GetChild(child).gameObject.GetComponent<MeshRenderer>().material;
        }
        setColors();
        coloring.updateColors();
    }

    public void setColors()
    {
        // When adding a bone color, make sure to pay mind to statBehavior, colorize, and colorBones
        // Set colors based on hex
        /*Table of Contents
        0 == Bones of White
        1 == Bones of Hope
        2 == Bones of Sun
        */
        int indexTemp = 0;
        for (int i = 0; i < colorVals.GetLength(0); i++)
        {
            for (int j = 0; j < colorVals.GetLength(1); j++)
            {
                colorVals[i, j] = childMats[indexTemp];
                Debug.Log("I HATE YOU UNITY DIE IN HELL: " + colorVals[i,j]);
                indexTemp++;
            }
        }
        /*// Bones of White
        colorVals[0, 0] = bonesofwhite_base; // Base
        colorVals[0, 1] = bonesofwhite_win; // Win
        colorVals[0, 2] = bonesofwhite_closest; // Closest
        colorVals[0, 3] = bonesofwhite_closer; // Closer
        colorVals[0, 4] = bonesofwhite_close; // Close
        colorVals[0, 5] = bonesofwhite_far; // Far
        colorVals[0, 6] = bonesofwhite_farther; // Farther
        colorVals[0, 7] = bonesofwhite_farthest; // Farthest

        // Bones of Hope
        colorVals[1, 0] = bonesofhope_base; // Base
        colorVals[1, 1] = bonesofhope_win; // Win
        colorVals[1, 2] = bonesofhope_closest; // Closest
        colorVals[1, 3] = bonesofhope_closer; // Closer
        colorVals[1, 4] = bonesofhope_close; // Close
        colorVals[1, 5] = bonesofhope_far; // Far
        colorVals[1, 6] = bonesofhope_farther; // Farther
        colorVals[1, 7] = bonesofhope_farthest; // Farthest


        // Bones of Sun
        colorVals[2, 0] = bonesofsun_base; // Base
        colorVals[2, 1] = bonesofsun_win; // Win
        colorVals[2, 2] = bonesofsun_closest; // Closest
        colorVals[2, 3] = bonesofsun_closer; // Closer
        colorVals[2, 4] = bonesofsun_close; // Close
        colorVals[2, 5] = bonesofsun_far; // Far
        colorVals[2, 6] = bonesofsun_farther; // Farther
        colorVals[2, 7] = bonesofsun_farthest; // Farthest

        // Bones of Love
        colorVals[3, 0] = bonesoflove_base; // Base
        colorVals[3, 1] = bonesoflove_win; // Win
        colorVals[3, 2] = bonesoflove_closest; // Closest
        colorVals[3, 3] = bonesoflove_closer; // Closer
        colorVals[3, 4] = bonesoflove_close; // Close
        colorVals[3, 5] = bonesoflove_far; // Far
        colorVals[3, 6] = bonesoflove_farther; // Farther
        colorVals[3, 7] = bonesoflove_farthest; // Farthest

        // Bones of Seige
        colorVals[4, 0] = bonesofseige_base; // Base
        colorVals[4, 1] = bonesofseige_win; // Win
        colorVals[4, 2] = bonesofseige_closest; // Closest
        colorVals[4, 3] = bonesofseige_closer; // Closer
        colorVals[4, 4] = bonesofseige_close; // Close
        colorVals[4, 5] = bonesofseige_far; // Far
        colorVals[4, 6] = bonesofseige_farther; // Farther
        colorVals[4, 7] = bonesofseige_farthest; // Farthest

        // Bones of Despair
        colorVals[5, 0] = bonesofdespair_base; // Base
        colorVals[5, 1] = bonesofdespair_win; // Win
        colorVals[5, 2] = bonesofdespair_closest; // Closest
        colorVals[5, 3] = bonesofdespair_closer; // Closer
        colorVals[5, 4] = bonesofdespair_close; // Close
        colorVals[5, 5] = bonesofdespair_far; // Far
        colorVals[5, 6] = bonesofdespair_farther; // Farther
        colorVals[5, 7] = bonesofdespair_farthest; // Farthest

        // Bones of Hareld
        colorVals[6, 0] = bonesofhareld_base; // Base
        colorVals[6, 1] = bonesofhareld_win; // Win
        colorVals[6, 2] = bonesofhareld_closest; // Closest
        colorVals[6, 3] = bonesofhareld_closer; // Closer
        colorVals[6, 4] = bonesofhareld_close; // Close
        colorVals[6, 5] = bonesofhareld_far; // Far
        colorVals[6, 6] = bonesofhareld_farther; // Farther
        colorVals[6, 7] = bonesofhareld_farthest; // Farthest*/

    }

    public void deSerialize() 
    {
        path = Application.persistentDataPath + "/save.txt";
        data = new GameSaver();
        bf = new BinaryFormatter();
        if (File.Exists(path))
        {
            file = File.Open(path, FileMode.Open);
            file.Position = 0;
            Debug.Log("what the fuck: " + file);
            data = (GameSaver)bf.Deserialize(file);
        }
        else
        {
            file = File.Create(path);
        }
        file.Close();
    }

    public void serialization()
    {
        path = Application.persistentDataPath + "/save.txt";
        fileAppend = File.Open(path, FileMode.Truncate);
        bf.Serialize(fileAppend, data);
        fileAppend.Close();
    }

    public void AddUser(string username) 
    {
        path = Application.persistentDataPath + "/save.txt";
        deSerialize();
        //7 1Debug.Log("Adding User: " + username);
        data.users.Add(username);
        data.userVal++;
        //Debug.Log("Users: " + string.Join("," , data.users));
        serialization();
    }

    public GameSaver GetSaver() 
    {
        deSerialize();
        GameSaver temp = data;
        serialization();
        return temp;
    }

    public void SendSaver(GameSaver gameSaver) 
    {
        deSerialize();
        data = gameSaver;
        serialization();
    }

    public void updateStat(string name, float input) 
    {
        deSerialize();
        data.GetType().GetField(name).SetValue(data, input);
        serialization();
    }

    public void setOrder(bool option)
    {
        deSerialize();
        data.leaderboardOrder = option;
        data.scores.Reverse();
        serialization();
    }

    public int compareTrophies(string[] trophy1, string[] trophy2) 
    {
        double t1Sum = Convert.ToDouble(trophy1[0]) + Convert.ToDouble(trophy1[1]);
        double t2Sum = Convert.ToDouble(trophy2[0]) + Convert.ToDouble(trophy2[1]);
        Debug.Log("t1Sum: " + t1Sum);
        Debug.Log("t2Sum: " + t2Sum);
        int t1 = 0; int t2 = 0;
        switch (trophy1[5]) 
        {
            case "diamond":
                t1 = 5;
                break;
            case "platinum":
                t1 = 4;
                break;
            case "gold":
                t1 = 3;
                break;
            case "silver":
                t1 = 2;
                break;
            case "bronze":
                t1 = 1;
                break;
        }
        switch (trophy2[5])
        {
            case "diamond":
                t2 = 5;
                break;
            case "platinum":
                t2 = 4;
                break;
            case "gold":
                t2 = 3;
                break;
            case "silver":
                t2 = 2;
                break;
            case "bronze":
                t2 = 1;
                break;
        }
        if (decendAcend)
        {
            if (t1 > t2)
            {
                return 0;
            }
            else if (t1 < t2)
            {
                return 1;
            }
            else
            {
                Debug.Log("HELLO ????");
                if (t1Sum < t2Sum)
                {
                    return 0;
                }
                else
                {
                    Debug.Log("we out 1 wtf");
                    return 1;
                }
            }
        }
        else 
        {
            if (t1 < t2)
            {
                return 0;
            }
            else if (t1 > t2)
            {
                return 1;
            }
            else
            {
                Debug.Log("HELLO ????");
                if (t1Sum > t2Sum)
                {
                    return 0;
                }
                else
                {
                    Debug.Log("we out 1 wtf");
                    return 1;
                }
            }
        }
    }

    public void addPoints(int numberofPoints)
    {
        deSerialize();
        addInsidePoints(numberofPoints);
        //data.stats.IndexOf(new string[] {data.currentUser, "Bonepoints"}) += numberofPoints;
        serialization();
    }

    public void addInsidePoints(int numberofPoints)
    {
        foreach (string[] stat in data.stats)
        {
            if (stat[0] == data.currentUser && stat[1] == "Bonepoints")
            {
                int temp = Int32.Parse(stat[2]);
                stat[2] = "" + (numberofPoints + temp);
                Debug.Log("H E E L L L L O :" + stat[2]);
            }
        }
    }

    public void designateTrophyPoints(string trophy, string[] levelParts, int[] diamond, int[] platinum, int[] gold, int[] silver, int[] bronze) 
    {
        if (trophy == "diamond")
        {
            if (levelParts[1] == "EASY")
            {
                addInsidePoints(diamond[0]);
            }
            else if (levelParts[1] == "NORMAL")
            {
                addInsidePoints(diamond[1]);
            }
            else if (levelParts[1] == "HARD")
            {
                addInsidePoints(diamond[2]);
            }
        }
        else if (trophy == "platinum")
        {
            if (levelParts[1] == "EASY")
            {
                addInsidePoints(platinum[0]);
            }
            else if (levelParts[1] == "NORMAL")
            {
                addInsidePoints(platinum[1]);
            }
            else if (levelParts[1] == "HARD")
            {
                addInsidePoints(platinum[2]);
            }
        }
        else if (trophy == "gold")
        {
            if (levelParts[1] == "EASY")
            {
                addInsidePoints(gold[0]);
            }
            else if (levelParts[1] == "NORMAL")
            {
                addInsidePoints(gold[1]);
            }
            else if (levelParts[1] == "HARD")
            {
                addInsidePoints(gold[2]);
            }
        }
        else if (trophy == "silver")
        {
            if (levelParts[1] == "EASY")
            {
                addInsidePoints(silver[0]);
            }
            else if (levelParts[1] == "NORMAL")
            {
                addInsidePoints(silver[1]);
            }
            else if (levelParts[1] == "HARD")
            {
                addInsidePoints(silver[2]);
            }
        }
        else if (trophy == "bronze")
        {
            if (levelParts[1] == "EASY")
            {
                addInsidePoints(bronze[0]);
            }
            else if (levelParts[1] == "NORMAL")
            {
                addInsidePoints(bronze[1]);
            }
            else if (levelParts[1] == "HARD")
            {
                addInsidePoints(bronze[2]);
            }
        }
    }

    public void addScore(string levelName, int numberofGuesses, double time, string trophy) 
    {
        deSerialize();
        string[] levelParts = levelName.Split('_');
        inp = new string[6] {"" + numberofGuesses, "" + time, levelName, data.currentUser, DateTime.Now.ToString(), trophy};
        Debug.Log(data.scores);
        int i = 0;
        int finalI = 0;
        bool iChanged = false;
        foreach (string[] score in data.scores) 
        {
            if (compareTrophies(score, inp) == 1 && !iChanged) 
            {
                finalI = data.scores.IndexOf(score);
                Debug.Log("solved: " + finalI);
                iChanged = true;
            }
            i++;
        }
        if (iChanged)
        {
            data.scores.Insert(finalI, inp);
        }
        else 
        {
            data.scores.Add(inp);
        }
        if (levelParts[0] == "HUMAN") 
        {
            designateTrophyPoints(trophy, levelParts, new int[] { 400, 600, 800 }, new int[] { 200, 400, 600 }, new int[] { 100, 200, 400 }, new int[] { 50, 100, 200 }, new int[] { 25, 50, 100 });
        }
        else if (levelParts[0] == "TREX")
        {
            designateTrophyPoints(trophy, levelParts, new int[] { 500, 750, 1000 }, new int[] { 250, 500, 750 }, new int[] { 100, 250, 500 }, new int[] { 50, 100, 250 }, new int[] { 25, 50, 100 });
        }
        else if (levelParts[0] == "CAT")
        {
            designateTrophyPoints(trophy, levelParts, new int[] { 450, 675, 900 }, new int[] { 225, 450, 675 }, new int[] { 100, 225, 450 }, new int[] { 50, 100, 225 }, new int[] { 25, 50, 100 });
        }
        data.scoreVal++;
        iChanged = false;
        serialization();
    }
}

[Serializable]
public class GameSaver
{
    public ArrayList users;
    public string currentUser;
    public int OpenCounter;
    public int userVal;
    public int scoreVal;
    public ArrayList scores;
    public bool leaderboardOrder;
    public ArrayList stats;
    public string currentDifficulty;

    // Start is called before the first frame update
    public GameSaver()
    {
        OpenCounter = 0;
        users = new ArrayList();
        scores = new ArrayList();
        stats = new ArrayList();
        userVal = users.Count;
        scoreVal = scores.Count;
        leaderboardOrder = true;
        currentDifficulty = null;
    }
}

public class UserStore 
{
    public bool lionUnlock;
    public bool horseUnlock;
    public bool whaleUnlock;
    // Start is called before the first frame update
    public UserStore()
    {
        lionUnlock = false;
        horseUnlock = false;
        whaleUnlock = false;
    }
}
