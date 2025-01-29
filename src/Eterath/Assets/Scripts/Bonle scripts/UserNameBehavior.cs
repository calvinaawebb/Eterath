using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UserNameBehavior : MonoBehaviour
{
    public GameObject userCanvas;
    public GameObject scrollObject;
    public statsBehavior statsBehavior;
    public user_menu menu;
    public UserData userData;
    public GameSaver data;
    //public GameObject UserMenu;
    public Transform content;
    public string currentUser;
    //public GameObject inputObj;
    //public UnityEngine.UIElements.Button button;
    public TMP_InputField inputUser;
    public UnityAction first;
    public UnityAction second;
    public UnityAction testing;
    public int initialUserVal = 0;

    void Start()
    {
        //button = scrollObject.GetComponent<UnityEngine.UIElements.Button>();
        //first += placeHoldFunc;
        data = userData.GetSaver();
        statsBehavior.initializeStats(true);
        scrollObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(placeHoldFunc);
        populateUserMenu();
        scrollObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { userCanvas.SetActive(true); });
    }

    public void populateUserMenu() 
    {
        foreach (string userName in data.users)
        {
            var itemInst = Instantiate(scrollObject) as GameObject;
            var textField = itemInst.GetComponentInChildren<TMP_Text>();
            textField.text = userName;
            itemInst.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            itemInst.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { userClick(userName); });
            itemInst.transform.SetParent(content, false);
            itemInst.transform.localScale = Vector2.one;
        }
    }

    public void depopulateUserMenu() 
    {
        for (int child = 1; child < content.transform.childCount; child++)
        {
            content.GetChild(child).GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
            GameObject.Destroy(content.GetChild(child).transform.gameObject);
            //content.tra
        }
    }

    void Update()
    {
        data = userData.GetSaver();
        if (data.users.Count < data.userVal)
        {
            data = userData.GetSaver();
            data.userVal--;
            userData.SendSaver(data);
            depopulateUserMenu();
            populateUserMenu();
        }
/*        else if (data.users.Count > data.userVal) 
        {
            data = userData.GetSaver();
            data.userVal++;
            userData.SendSaver(data);
            depopulateUserMenu();
            populateUserMenu();
        }*/
    }

    public void placeHoldFunc() 
    {
        userCanvas.SetActive(true);
    }

    public void userClick(string name) 
    {
        Debug.Log("CLICK");
        menu.ExtendMenu();
        data.currentUser = name;
        data.currentDifficulty = null;
        Debug.Log("Current User: " + data.currentUser);
        userData.SendSaver(data);
        statsBehavior.initializeStats(false);
    } 
    public void userMenu() 
    {
        userCanvas.SetActive(false);
        menu.ExtendMenu();
        var itemInst = Instantiate(scrollObject) as GameObject;
        var textField = itemInst.GetComponentInChildren<TMP_Text>();
        textField.text = inputUser.text.ToLower();
        data.currentUser = inputUser.text.ToLower();
        data.currentDifficulty = null;
        userData.SendSaver(data);
        userData.AddUser(inputUser.text.ToLower());
        statsBehavior.initializeStats(false);
        //data.users.Add(inputUser.text.ToLower());
        itemInst.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();   
        itemInst.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { userClick(inputUser.text.ToLower()); });
        itemInst.transform.SetParent(content, false);
        itemInst.transform.localScale = Vector2.one;
    }
    public void rename() 
    {
        data = userData.GetSaver();
        string input = inputUser.text.ToLower();
        userCanvas.SetActive(false);
        for (var i=0;i<data.users.Count;i++)
        {
            if ((string)data.users[i] == data.currentUser) 
            {
                data.users.Remove((string)data.users[i]);
                data.users.Add(input);
                data.currentUser = input;
            }
        }
        userData.SendSaver(data);
        depopulateUserMenu();
        populateUserMenu();
    }
}
