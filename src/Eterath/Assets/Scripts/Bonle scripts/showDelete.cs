using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showDelete : MonoBehaviour
{
    public UserData userData;
    GameSaver data;
    public GameObject deleteMenu;
    // Start is called before the first frame update
    void Start()
    {
        data = userData.GetSaver();
    }

    // Update is called once per frame
    void Update()
    {
        data = userData.GetSaver();
        if (data.currentUser != null) 
        {
            //Debug.Log("text:" + deleteMenu.GetComponent<TextMeshPro>().text);
            deleteMenu.GetComponent<TextMeshProUGUI>().text = data.currentUser;
        }
    }

    public void deleteAccount() 
    {
        Debug.Log("Deleted!");
        data.users.Remove(data.currentUser);
        data.currentUser = null;
        userData.SendSaver(data);
    }
}
