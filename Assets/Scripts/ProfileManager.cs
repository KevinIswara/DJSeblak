using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ProfileManager : MonoBehaviour {

	public string UserId;
	public string UserName;
	public string UserCountry;
	public string UserJoinDate;
	public string UserMoney;
	public string UserDiamond;

	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
		PlayerPrefs.SetString("Id", "1");
		UserId = PlayerPrefs.GetString("Id");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserId);
		reference.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				UserName = "User";
				UserCountry = "Indonesia";
				UserJoinDate = "January 1, 2018";
				UserMoney = "0";
				UserDiamond = "0";
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				UserName = snapshot.Child("Name").Value;
				UserCountry = snapshot.Child("Country").Value;
				UserJoinDate = snapshot.Child("JoinDate").Value;
				UserMoney = snapshot.Child("Money").Value.ToString();
				UserDiamond = snapshot.Child("Diamond").Value.ToString();
			}
		})
		.ValueChanged += HandleValueChanged;
	}
	
	void HandleValueChanged(object sender, ValueChangedEventArgs args) {
		if (args.DatabaseError != null) {
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		DataSnapshot snapshot = args.Snapshot;
		UserName = snapshot.Child("Name").Value;
		UserCountry = snapshot.Child("Country").Value;
		UserJoinDate = snapshot.Child("JoinDate").Value;
		UserMoney = snapshot.Child("Money").Value.ToString();
		UserDiamond = snapshot.Child("Diamond").Value.ToString();
	}

}