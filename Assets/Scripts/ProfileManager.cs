using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ProfileManager : MonoBehaviour
{

    public string UserId;
    public string UserName;
    public string UserCountry;
    public string UserJoinDate;
    public string UserMoney;
    public string UserDiamond;

    public Text Name;
    public Text Country;
    public Text JoinDate;
    public Text Money;
    public Text Diamond;

    void Start()
    {
        UserName = "User";
        UserCountry = "Indonesia";
        UserJoinDate = "January 1, 2018";
        UserMoney = "0";
        UserDiamond = "0";
        Name.text = UserName;
        Country.text = UserCountry;
        JoinDate.text = UserJoinDate;
        Money.text = UserMoney;
        Diamond.text = UserDiamond;
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
        {
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        }
        startListener();
    }

    void startListener()
    {
        Debug.Log("fuck");
        PlayerPrefs.SetString("Id", "1");
        UserId = PlayerPrefs.GetString("Id");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserId);
        Debug.Log("this");
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                UserName = "User";
                UserCountry = "Indonesia";
                UserJoinDate = "January 1, 2018";
                UserMoney = "0";
                UserDiamond = "0";
                Name.text = UserName;
                Country.text = UserCountry;
                JoinDate.text = UserJoinDate;
                Money.text = UserMoney;
                Diamond.text = UserDiamond;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("gagaga");
                if (snapshot.Value != null)
                {
                    Debug.Log("asdasd");
                    UserName = (string)snapshot.Child("Name").Value.ToString();
                    UserCountry = (string)snapshot.Child("Country").Value.ToString();
                    UserJoinDate = (string)snapshot.Child("JoinDate").Value.ToString();
                    UserMoney = (string)snapshot.Child("Money").Value.ToString();
                    UserDiamond = (string)snapshot.Child("Diamond").Value.ToString();
                    Name.text = UserName;
                    Country.text = UserCountry;
                    JoinDate.text = UserJoinDate;
                    Money.text = UserMoney;
                    Diamond.text = UserDiamond;
                }
            }
        });
        reference.ValueChanged += HandleValueChanged;
        Debug.Log("shit");
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        DataSnapshot snapshot = args.Snapshot;
        UserName = (string)snapshot.Child("Name").Value.ToString();
        UserCountry = (string)snapshot.Child("Country").Value.ToString();
        UserJoinDate = (string)snapshot.Child("JoinDate").Value.ToString();
        UserMoney = (String)snapshot.Child("Money").Value.ToString();
        UserDiamond = (String)snapshot.Child("Diamond").Value.ToString();
        Debug.Log("wao");
        Name.text = UserName;
        Country.text = UserCountry;
        JoinDate.text = UserJoinDate;
        Money.text = UserMoney;
        Diamond.text = UserDiamond;
    }

    void Update()
    {

    }

}