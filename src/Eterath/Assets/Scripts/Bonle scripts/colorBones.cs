using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class colorBones : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> boneParents = new List<GameObject>();
    public List<string> boneTypes = new List<string>();
    public UserData userData;
    public GameSaver data;
    // Start is called before the first frame update
    void Start()
    {
        data = userData.GetSaver();
    }

    // Update is called once per frame
    public void updateColors()
    {
        data = userData.GetSaver();
        int index = 0;
        foreach (GameObject boneParent in boneParents)
        {
            foreach (Transform child in boneParent.transform)
            {
                if (child.GetComponent<MeshRenderer>() != null) 
                {
                    string inpText = "Equipped Bone " + boneTypes[index];
                    foreach (string[] stat in data.stats)
                    {
                        if (stat[1] == inpText && stat[0] == data.currentUser)
                        {
                            if (stat[2] == "Bones of White")
                            {
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[0, 0];
                                Debug.Log("MAN WHAT: " + userData.colorVals[0, 0]);
                            }
                            else if (stat[2] == "Bones of Hope")
                            {
                                Debug.Log("MAN WHAT: " + userData.colorVals[1, 0]);
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[1, 0];
                            }
                            else if (stat[2] == "Bones of Sun")
                            {
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[2, 0];
                                Debug.Log("MAN WHAT: " + userData.colorVals[2, 0]);
                            }
                            else if (stat[2] == "Bones of Love")
                            {
                                Debug.Log("MAN WHAT: " + userData.colorVals[3, 0]);
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[3, 0];
                            }
                            else if (stat[2] == "Bones of Seige")
                            {
                                Debug.Log("MAN WHAT: " + userData.colorVals[4, 0]);
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[4, 0];
                            }
                            else if (stat[2] == "Bones of Despair")
                            {
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[5, 0];
                                Debug.Log("MAN WHAT: " + userData.colorVals[5, 0]);
                            }
                            else if (stat[2] == "Bones of Hareld")
                            {
                                child.gameObject.GetComponent<MeshRenderer>().material = userData.colorVals[6, 0];
                                Debug.Log("MAN WHAT: " + userData.colorVals[6, 0]);
                            }
                        }
                    }
                }
            }
            index++;
        }
    }
}
