using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class loadScene : MonoBehaviour
{
    public string scene;
    public static string skeleType;
    public Animator transistion;
    public static float transistionTime = 1.1f;
    public string difficulty;
    public GameObject slider;
    public bool practice;
    public UserData userData;
    public GameSaver data;
    public statsBehavior statsBehavior;

    // Used with the escape key to close the application.
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    // Final function to load the scene provided.
    public void sceneLoad(string inpScene)
    {
        SceneManager.LoadScene(inpScene);
        difficulty = "";
    }

    public void canvaLoad(GameObject loading, Boolean onOff) 
    {
        loading.SetActive(onOff);
    }

    // First function that gets called, calls coroutine to start its processes.
    public void startLevel(string inpSceneMain) 
    {
        if (userData != null) 
        {
            data = userData.GetSaver();
            if (data.currentUser == null)
            {
                data.currentUser = "Save#" + (data.users.Count + 1);
                userData.SendSaver(data);
                userData.AddUser("Save#" + (data.users.Count + 1));
                statsBehavior.initializeStats(true);
                Debug.Log("YIPPEE KIYAY MOTHER FUCKER");
            }
        }
        StartCoroutine(LoadLevel(inpSceneMain));
    }

    // Sets the name of the scene that is being loaded using the variables inputed into the script.
    public void difficultySet() 
    {
        data = userData.GetSaver();
        if (skeleType.Length != 0) 
        {
            if (practice) 
            {
                difficulty = skeleType + "_" + difficulty + "_PRACTICE" ;
                sceneLoad(difficulty);
            } else 
            {
                int temp;
                if (skeleType == "TREX")
                {
                    if (difficulty == "EASY")
                    {
                        temp = Int32.Parse(((string[])data.stats[2])[2]);
                        ((string[])data.stats[2])[2] = "" + (temp + 1);
                    }
                    else if (difficulty == "NORMAL")
                    {
                        temp = Int32.Parse(((string[])data.stats[3])[2]);
                        ((string[])data.stats[3])[2] = "" + (temp + 1);
                    }
                    else if (difficulty == "HARD")
                    {
                        temp = Int32.Parse(((string[])data.stats[4])[2]);
                        ((string[])data.stats[4])[2] = "" + (temp + 1);
                    }
                }
                else if (skeleType == "CAT")
                {
                    if (difficulty == "EASY")
                    {
                        temp = Int32.Parse(((string[])data.stats[5])[2]);
                        ((string[])data.stats[5])[2] = "" + (temp + 1);
                    }
                    else if (difficulty == "NORMAL")
                    {
                        temp = Int32.Parse(((string[])data.stats[6])[2]);
                        ((string[])data.stats[6])[2] = "" + (temp + 1);
                    }
                    else if (difficulty == "HARD")
                    {
                        temp = Int32.Parse(((string[])data.stats[7])[2]);
                        ((string[])data.stats[7])[2] = "" + (temp + 1);
                    }
                }
                else if (skeleType == "HUMAN")
                {
                    if (difficulty == "EASY")
                    {
                        temp = Int32.Parse(((string[])data.stats[8])[2]);
                        Debug.Log("WHAT IS GOING ON: " + temp);
                        ((string[])data.stats[8])[2] = "" + (temp + 1);
                    }
                    else if (difficulty == "NORMAL")
                    {
                        temp = Int32.Parse(((string[])data.stats[9])[2]);
                        ((string[])data.stats[9])[2] = "" + (temp + 1);
                    }
                    else if (difficulty == "HARD")
                    {
                        temp = Int32.Parse(((string[])data.stats[10])[2]);
                        ((string[])data.stats[10])[2] = "" + (temp + 1);
                    }
                }
                Debug.Log(((string[])data.stats[8])[2]);
                difficulty = skeleType + "_" + difficulty;
                data.currentDifficulty = difficulty;
                userData.SendSaver(data);
                sceneLoad(skeleType);
            }
        }
    }

    // Main runtime that combines all the functions to process the input given to the script, turn it into a scene name, and load said scene.
    IEnumerator LoadLevel(string inp) 
    {
        transistion.SetTrigger("out");

        yield return new WaitForSeconds(transistionTime);

        Debug.Log("going in");
        skeleType = gameObject.name;
        try 
        {
            difficulties scrip = slider.GetComponent<difficulties>();
            difficulty = scrip.inp.text;
            difficultySet();
        } 
        catch (Exception e) {
            sceneLoad(inp);
        }
    }
}
