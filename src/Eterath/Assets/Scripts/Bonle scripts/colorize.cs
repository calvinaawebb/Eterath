using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GraphNode;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;
using TMPro;

public class colorize : MonoBehaviour
{
    public InputField input;
    public TextMeshProUGUI guessOut;
    public TextMeshProUGUI timeOut;
    public TextMeshProUGUI trophyOutText;
    public Image trophyOut;
    public Transform skeleB;
    public Transform skeleA;
    public GameObject bone;
    public GameObject tBone;
    public Transform randB;
    public GameObject skeleton;
    public Transform resetSkeleton;
    public int num;

    public Sprite diamondTrophy;
    public Sprite platinumTrophy;
    public Sprite goldTrophy;
    public Sprite silverTrophy;
    public Sprite bronzeTrophy;
    public nameScript trophies;

    public static Dictionary<string, int> scoresc = new Dictionary<string, int>();

    public Dictionary<string, string> prevNode = new Dictionary<string, string>();

    public Dictionary<string, double> shortestdistance = new Dictionary<string, double>();

    public GraphNode guess;
    public GraphNode target;

    public graphClass graph;
    public graphClass easyGraph;
    public graphClass normalGraph;
    public graphClass hardGraph;

    public UserData data;
    public GameSaver gameSaver;

    public Canvas Main;
    public GameObject GameOver;
    public GameObject errOut;

    public int guesses = 0;

    public ArrayList boneNames;

    public bool isBonePresent = false;

    public double timeTaken;
    public double retryStart;

    public int indexRight;
    public GameObject skeletonParent;
    public GameObject[] skeletons = new GameObject[3];



    // material.colors
    public Material[] colors;
    public static Material farthest;
    public static Material farther;
    public static Material far;
    public static Material close;
    public static Material closer;
    public static Material closest;
    public static Material based;
    public static Material win;
    public ArrayList colorsforCheatsheet;

    public String[] boneParts;

    void Start()
    {
        gameSaver = data.GetSaver();
        string[] inputArray = { "Testing", "FARTHER", "color"};
        colorsforCheatsheet = new ArrayList() { inputArray };
        string[] sceneParts = gameSaver.currentDifficulty.Split('_');

        int skeleind = 0;
        foreach (Transform child in skeletonParent.transform)
        {
            Debug.Log("in here");
            if (sceneParts[1] == "EASY" && skeleind == 0)
            {
                child.gameObject.SetActive(true);
                skeleton = child.gameObject;
                Debug.Log("SKELETON MAN: " + skeleton);
                break;
            }
            else 
            {
                child.gameObject.SetActive(false);
            }

            if (sceneParts[1] == "NORMAL" && skeleind == 1)
            {
                child.gameObject.SetActive(true);
                skeleton = child.gameObject;
                break;
            }
            else
            {
                child.gameObject.SetActive(false);
            }

            if (sceneParts[1] == "HARD" && skeleind == 2)
            {
                child.gameObject.SetActive(true);
                skeleton = child.gameObject;
                break;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            skeleind++;
        }

        if (sceneParts[1] == "EASY") 
        {
            graph = easyGraph;
        }
        else if (sceneParts[1] == "NORMAL")
        {
            graph = normalGraph;
        }
        else if (sceneParts[1] == "HARD")
        {
            graph = hardGraph;
        }

        // Sets all the colors.
        indexRight = 0;
        foreach (string[] stat in gameSaver.stats) 
        {
            if (("Equipped Bone " + sceneParts[0]) == stat[1] && stat[0] == gameSaver.currentUser) 
            {
                if (stat[2] == "Bones of White") 
                {
                    indexRight = 0;
                    break;
                }
                else if (stat[2] == "Bones of Hope")
                {
                    indexRight = 1;
                    break;
                }
                else if (stat[2] == "Bones of Sun")
                {
                    indexRight = 2;
                    break;
                }
                else if (stat[2] == "Bones of Love")
                {
                    indexRight = 3;
                    break;
                }
                else if (stat[2] == "Bones of Seige")
                {
                    indexRight = 4;
                    break;
                }
                else if (stat[2] == "Bones of Despair")
                {
                    indexRight = 5;
                    break;
                }
                else if (stat[2] == "Bones of Hareld")
                {
                    indexRight = 6;
                    break;
                }
            }
        }
        
        try 
        {
            Debug.Log("INDEXRIGHT: " + indexRight + " " + data.colorVals[indexRight, 7]);
        }
        catch (Exception e) 
        {
            //data.coloringThings();
            Debug.Log(e);
        }

        farthest = data.colorVals[indexRight, 7];
        farther = data.colorVals[indexRight, 6];
        far = data.colorVals[indexRight, 5];
        close = data.colorVals[indexRight, 4];
        closer = data.colorVals[indexRight, 3];
        closest = data.colorVals[indexRight, 2];
        win = data.colorVals[indexRight, 1];
        based = data.colorVals[indexRight, 0];


        colors = new Material[] { closest, closer, close, far, farther, farthest };

        diamondTrophy = trophies.diamondTrophy;
        platinumTrophy = trophies.platinumTrophy;
        goldTrophy = trophies.goldTrophy;
        silverTrophy = trophies.silverTrophy;
        bronzeTrophy = trophies.bronzeTrophy;

        // Random Bone(randB) to use as target bone(tBone) using random number(num).
        num = UnityEngine.Random.Range(0, skeleton.transform.childCount);
        randB = skeleton.transform.GetChild(num);
        tBone = GameObject.Find(randB.name);
        Debug.Log("asdasdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa: " + tBone.name);

        // Excuses the gameobject I use to center the camera when it is moving.
        while (tBone.name == "Orbit") 
        {
            num = UnityEngine.Random.Range(0, skeleton.transform.childCount);
            randB = skeleton.transform.GetChild(num);
            tBone = GameObject.Find(randB.name);
        }
    }

    void Update() 
    {
        input.Select();
        input.ActivateInputField();
    }

    // Dijkstra's Algorithm: basically just takes the node I give it(target) and finds the shortest distance to all other nodes based off of their connections.
    public void djk(GraphNode anchor, Dictionary<String, Double> dist, Dictionary<String, Double> edges)
    {
        foreach (GraphNode child in anchor.Children)
        {
            try
            {
                if (dist.ContainsKey(child.Name))
                {
                    if (dist[child.Name] > (edges[anchor.Name + child.Name] + dist[anchor.Name]))
                    {
                        dist[child.Name] = (edges[anchor.Name + child.Name] + dist[anchor.Name]);
                    }
                }
                else
                {
                    dist.Add(child.Name, edges[anchor.Name + child.Name] + dist[anchor.Name]);
                    prevNode.Add(child.Name, anchor.Name);
                    djk(child, dist, edges);
                }
            }
            catch (KeyNotFoundException e)
            {
                if (dist.ContainsKey(child.Name))
                {
                    if (dist[child.Name] > (edges[child.Name + anchor.Name] + dist[anchor.Name]))
                    {
                        dist[child.Name] = (edges[child.Name + anchor.Name] + dist[anchor.Name]);
                    }
                }
                else
                {
                    dist.Add(child.Name, edges[child.Name + anchor.Name] + dist[anchor.Name]);
                    prevNode.Add(child.Name, anchor.Name);
                    djk(child, dist, edges);
                }
            }
        }
    }

    // Retry function that resets all of the bone meshes to their base color and regenerates the target bone so you can play again.
    public void Retry()
    {
        guesses = 0;
        retryStart = Math.Round((double)Time.timeSinceLevelLoad, 2, MidpointRounding.AwayFromZero);
        //Main.enabled = true;
        GameOver.SetActive(false);
        num = UnityEngine.Random.Range(0, skeleton.transform.childCount);
        randB = skeleton.transform.GetChild(num);
        tBone = GameObject.Find(randB.name);
        foreach (Transform child in resetSkeleton)
        {
            if (child.name != "Orbit")
            {
                try
                {
                    child.GetComponent<MeshRenderer>().material = based;
                }
                catch (Exception e)
                {
                    foreach (Transform children in child)
                    {
                        children.GetComponent<MeshRenderer>().material = based;
                    }
                }
            }
        }
    }

    // Simple optimization to make coloring a bone faster and take less space.
    public void colorBone(Transform bone, double num, Material[] cols) 
    {
        try
        {
            bone.GetComponent<MeshRenderer>().material = cols[(int)num-1];
            string[] splitName = cols[(int)num - 1].name.Split('(');
            string inpColor = "";
            foreach (Material mat in colors)
            {
                Debug.Log("JESUSUSUSUSUSUUSUSUUSUSUSUUS: " + mat.name + " " + cols[(int)num - 1].name);
                if (mat.name == cols[(int)num - 1].name)
                {
                    // IF ADDING A NEW BONE COLOR AND IT HAS THE SIMPLETRI SHADER ADD THE INDEX NUMBER HERE FROM START OF THIS FILE
                    if (indexRight == 3 || indexRight == 4)
                    {
                        inpColor = "#" + ColorUtility.ToHtmlStringRGBA(mat.GetColor("_Input_Color"));
                    }
                    else 
                    {
                        inpColor = "#" + ColorUtility.ToHtmlStringRGBA(mat.color);
                    }
                    Debug.Log("JESUSUSUSUSUSUUSUSUUSUSUSUUS: " + inpColor);
                }
            }
            string[] inputArray = { boneParts[1], splitName[0], inpColor };
            colorsforCheatsheet.Add(inputArray);
            Debug.Log(colorsforCheatsheet[-1]);
            //errOut.GetComponent<TextMeshProUGUI>().text = bone.GetComponent<Renderer>().material.name;
        }
        catch(IndexOutOfRangeException e) 
        {
            bone.GetComponent<MeshRenderer>().material = cols[cols.Length-1];
            string[] splitName = cols[cols.Length - 1].name.Split('(');
            string inpColor = "";
            foreach (Material mat in colors)
            {
                Debug.Log("JESUSUSUSUSUSUUSUSUUSUSUSUUS: " + mat.name + " " + cols[cols.Length - 1].name);
                if (mat.name == cols[cols.Length - 1].name)
                {
                    if (indexRight == 3 || indexRight == 4)
                    {
                        inpColor = "#" + ColorUtility.ToHtmlStringRGBA(mat.GetColor("_Input_Color"));
                    }
                    else
                    {
                        inpColor = "#" + ColorUtility.ToHtmlStringRGBA(mat.color);
                    }
                    Debug.Log("JESUSUSUSUSUSUUSUSUUSUSUSUUS: " + inpColor);
                }
            }
            string[] inputArray = { boneParts[1], splitName[0], inpColor };
            colorsforCheatsheet.Add(inputArray);
            Debug.Log(colorsforCheatsheet[-1]);
            //errOut.GetComponent<TextMeshProUGUI>().text = bone.GetComponent<Renderer>().material.name;
        }
    }

    public string findTrophy(string level, int guesses, double time)
    {
        string[] levelParts = level.Split('_');
        if (levelParts[1] == "EASY")
        {
            switch (levelParts[0])
            {
                case "TREX":
                    if (14 < guesses || time > 35d)
                    {
                        return "bronze";
                    }
                    else if (7 < guesses && guesses <= 14 || time > 20d && time <= 35d)
                    {
                        return "silver";
                    }
                    else if (4 < guesses && guesses <= 7 || time > 8.5d && time <= 20d)
                    {
                        return "gold";
                    }
                    else if (2 < guesses && guesses <= 4 || time > 5d && time <= 8.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 2 || time <= 5d)
                    {
                        return "diamond";
                    }
                    break;

                case "CAT":
                    if (12 < guesses || time > 30d)
                    {
                        return "bronze";
                    }
                    else if (6 < guesses && guesses <= 12 || time > 15d && time <= 30d)
                    {
                        return "silver";
                    }
                    else if (3 < guesses && guesses <= 6 || time > 8.5d && time <= 15d)
                    {
                        return "gold";
                    }
                    else if (1 < guesses && guesses <= 3 || time > 5d && time <= 8.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 1 || time <= 5d)
                    {
                        return "diamond";
                    }
                    break;

                case "HUMAN":
                    if (12 < guesses || time > 30d)
                    {
                        return "bronze";
                    }
                    else if (6 < guesses && guesses <= 12 || time > 15d && time <= 30d)
                    {
                        return "silver";
                    }
                    else if (3 < guesses && guesses <= 6 || time > 7.5d && time <= 15d)
                    {
                        return "gold";
                    }
                    else if (1 < guesses && guesses <= 3 || time > 5d && time <= 7.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 1 || time <= 5d)
                    {
                        return "diamond";
                    }
                    break;
            }
        }
        else if (levelParts[1] == "NORMAL") 
        {
            switch (levelParts[0])
            {
                case "TREX":
                    if (15 < guesses || time > 35d)
                    {
                        return "bronze";
                    }
                    else if (8 < guesses && guesses <= 15 || time > 20d && time <= 35d)
                    {
                        return "silver";
                    }
                    else if (5 < guesses && guesses <= 8 || time > 9.5d && time <= 20d)
                    {
                        return "gold";
                    }
                    else if (2 < guesses && guesses <= 5 || time > 6d && time <= 9.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 2 || time <= 6d)
                    {
                        return "diamond";
                    }
                    break;

                case "CAT":
                    if (14 < guesses || time > 30d)
                    {
                        return "bronze";
                    }
                    else if (7 < guesses && guesses <= 14 || time > 15d && time <= 30d)
                    {
                        return "silver";
                    }
                    else if (4 < guesses && guesses <= 7 || time > 9.5d && time <= 15d)
                    {
                        return "gold";
                    }
                    else if (1 < guesses && guesses <= 4 || time > 6d && time <= 9.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 1 || time <= 6d)
                    {
                        return "diamond";
                    }
                    break;

                case "HUMAN":
                    if (14 < guesses || time > 30d)
                    {
                        return "bronze";
                    }
                    else if (7 < guesses && guesses <= 14 || time > 15d && time <= 30d)
                    {
                        return "silver";
                    }
                    else if (4 < guesses && guesses <= 7 || time > 8.5d && time <= 15d)
                    {
                        return "gold";
                    }
                    else if (1 < guesses && guesses <= 4 || time > 6d && time <= 8.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 1 || time <= 6d)
                    {
                        return "diamond";
                    }
                    break;
            }
        }
        else if (levelParts[1] == "HARD")
        {
            switch (levelParts[0])
            {
                case "TREX":
                    if (15 < guesses || time > 35d)
                    {
                        return "bronze";
                    }
                    else if (8 < guesses && guesses <= 15 || time > 20d && time <= 35d)
                    {
                        return "silver";
                    }
                    else if (5 < guesses && guesses <= 8 || time > 9.5d && time <= 20d)
                    {
                        return "gold";
                    }
                    else if (3 < guesses && guesses <= 5 || time > 7d && time <= 10.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 3 || time <= 7d)
                    {
                        return "diamond";
                    }
                    break;

                case "CAT":
                    if (14 < guesses || time > 30d)
                    {
                        return "bronze";
                    }
                    else if (7 < guesses && guesses <= 14 || time > 15d && time <= 30d)
                    {
                        return "silver";
                    }
                    else if (4 < guesses && guesses <= 7 || time > 9.5d && time <= 15d)
                    {
                        return "gold";
                    }
                    else if (1 < guesses && guesses <= 4 || time > 7d && time <= 10.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 2 || time <= 5d)
                    {
                        return "diamond";
                    }
                    break;

                case "HUMAN":
                    if (14 < guesses || time > 30d)
                    {
                        return "bronze";
                    }
                    else if (7 < guesses && guesses <= 14 || time > 15d && time <= 30d)
                    {
                        return "silver";
                    }
                    else if (4 < guesses && guesses <= 7 || time > 8.5d && time <= 15d)
                    {
                        return "gold";
                    }
                    else if (1 < guesses && guesses <= 4 || time > 6d && time <= 8.5d)
                    {
                        return "platinum";
                    }
                    else if (guesses <= 2 || time <= 5d)
                    {
                        return "diamond";
                    }
                    break;
            }
        }
        return "Error";
    }

    // Main runtime of the logic where all the logic is combined to color the bones based of their nodal distance to the target.
    public void activate()
    {
        /*farthest = data.colorVals[indexRight, 7];
        farther = data.colorVals[indexRight, 6];
        far = data.colorVals[indexRight, 5];
        close = data.colorVals[indexRight, 4];
        closer = data.colorVals[indexRight, 3];
        closest = data.colorVals[indexRight, 2];
        win = data.colorVals[indexRight, 1];
        based = data.colorVals[indexRight, 0];

        *//*data.colorVals[indexRight, 6] = farther;
        data.colorVals[indexRight, 5] = far;
        data.colorVals[indexRight, 4] = close;
        data.colorVals[indexRight, 3] = closer;
        data.colorVals[indexRight, 2] = closest;
        data.colorVals[indexRight, 1] = win;
        data.colorVals[indexRight, 0] = based;*//*


        colors = new Material[] { closest, closer, close, far, farther, farthest };*/

        String[] words = tBone.name.ToLower().Split("_");
        //Debug.Log("Skeleb: " + skeleB.name);
        //Debug.Log("this target: " + tBone.name.ToLower());

        // Finding the node that corresponds with the bone object in unity.
        foreach (GraphNode node in graph.node_list)
        {
            string[] nodeName = node.Name.Split(" ");
            Debug.Log("node: " + node.Name + " target: " + words[1]);
            if (node.Name == words[1])
            {
                target = node;
                Debug.Log("target: " + target.Name);
            }
            if (nodeName.Length != 1) 
            {
                if (nodeName[1] == "l")
                {
                    if (input.text.ToLower() == "left " + nodeName[0])
                    {
                        guess = node;
                        Debug.Log("guess: " + guess.Name);
                    }
                }
                if (nodeName[1] == "r")
                {
                    if (input.text.ToLower() == "right " + nodeName[0])
                    {
                        guess = node;
                        Debug.Log("guess: " + guess.Name);
                    }
                }
            }
            if (node.Name == input.text.ToLower())
            {
                guess = node;
                Debug.Log("guess: " + guess.Name);
            }
            //if (guess != null && )
        }

        // Getting the bone object
        foreach (Transform bone in skeleton.transform) 
        {
            boneParts = bone.name.Split("_");
            Debug.Log("bone: " + bone.name + "bonepart: " + boneParts[1]);
            if (boneParts[1] == input.text.ToLower() || boneParts[0].ToLower() == input.text.ToLower())
            {
                skeleB = bone;
                isBonePresent = true;
                break;
            }
            else 
            {
                isBonePresent = false;
            }
        }

        //skeleB = skeleton.transform.Find(words[1]);

        // Activate dijkstra's algorithm.
        if (shortestdistance.ContainsKey(target.Name) == false) 
        {
            shortestdistance.Add(target.Name, 0);
        }
        djk(target, shortestdistance, graph.valuePairs);

        try
        {
            if (guess.Name != null)
            {
                if (guess.Name == target.Name)
                {
                    errOut.GetComponent<TextMeshProUGUI>().text = "";
                    guesses += 1;
                    skeleB.GetComponent<MeshRenderer>().material = win;
                    num = UnityEngine.Random.Range(0, skeleton.transform.childCount);

                    if (timeTaken != null)
                    {
                        timeTaken = Math.Round((double)Time.timeSinceLevelLoad, 2, MidpointRounding.AwayFromZero) - retryStart;
                    }
                    else 
                    {
                        timeTaken = Math.Round((double)Time.timeSinceLevelLoad, 2, MidpointRounding.AwayFromZero);
                    }

                    // Setting trophy image in gameover screen.
                    string trophyName = findTrophy(gameSaver.currentDifficulty, guesses, timeTaken);
                    Debug.Log(trophyName);
                    trophyOutText.text = trophyName;

                    data.addScore(gameSaver.currentDifficulty, guesses, timeTaken, trophyName);
                    Debug.Log("A D D E D S C O R E");
                    switch (trophyName) 
                    {
                        case "diamond":
                            trophyOut.sprite = diamondTrophy;
                            break;

                        case "platinum":
                            trophyOut.sprite = platinumTrophy;
                            break;

                        case "gold":
                            trophyOut.sprite = goldTrophy;
                            break;

                        case "silver":
                            trophyOut.sprite = silverTrophy;
                            break;

                        case "bronze":
                            trophyOut.sprite = bronzeTrophy;
                            break;
                    }

                    //Main.enabled = false;
                    GameOver.SetActive(true);
                    guessOut.text = "" + guesses;
                    timeOut.text = "" + timeTaken;
                    try
                    {
                        scoresc.Add(getName.name + ":" + gameSaver.currentDifficulty, guesses);
                    }
                    catch (Exception e)
                    {
                        if (scoresc[getName.name + ":" + gameSaver.currentDifficulty] > guesses)
                        {
                            scoresc[getName.name + ":" + gameSaver.currentDifficulty] = guesses;
                        }
                    }
                    Debug.Log("in");
                    for (int i = 0; i < scoresc.Count; i++)
                    {
                        Debug.Log(scoresc.ElementAt(i).Key + " " + scoresc.ElementAt(i).Value);
                    }
                }
                else
                {
                    guesses += 1;
                    try
                    {
                        if (isBonePresent)
                        {
                            errOut.GetComponent<TextMeshProUGUI>().text = "";
                            colorBone(skeleB, shortestdistance[guess.Name], colors);
                            Debug.Log("WHAT");
                        }
                        else
                        {
                            errOut.GetComponent<TextMeshProUGUI>().text = "No such bone";
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                        /*if (isBonePresent)
                        {
                            for (int i = 0; i < skeleB.transform.childCount; i++)
                            {
                                skeleA = skeleB.transform.GetChild(i);
                                colorBone(skeleA, shortestdistance[guess.Name], colors);
                            }
                        }
                        else
                        {
                            errOut.GetComponent<TextMeshProUGUI>().text = "No such bone";
                        }*/
                    }
                }
            }
            else 
            {
                errOut.GetComponent<TextMeshProUGUI>().text = "No such bone 2";
            }

        }
        catch (Exception e)
        {
            errOut.GetComponent<TextMeshProUGUI>().text = "No such bone 3";
            Debug.Log(e);
        }
        // Process the input from the user and color the bones respectively as well as handle if player guess the right bone.
        
       
        shortestdistance.Clear();
        prevNode.Clear();
    }
}
