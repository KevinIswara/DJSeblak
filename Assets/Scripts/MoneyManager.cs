using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MoneyManager : MonoBehaviour {

	public string UserId;
	public int UserMoney;
	public int UserDiamond;
	public int AddMoney;
	public int AddDiamond;

	void Start () {
		PlayerPrefs.SetInt("Money", "10");
		PlayerPrefs.SetInt("Diamond", "2");
		AddMoney = PlayerPrefs.GetInt("Money");
		AddDiamond = PlayerPrefs.GetInt("Diamond");
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
		UserId = PlayerPrefs.GetString("Id");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserId);
		reference.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				UserMoney = "0";
				UserDiamond = "0";
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				UserMoney = snapshot.Child("Money").Value;
				UserDiamond = snapshot.Child("Diamond").Value;
			}
		});
		reference.Child("Money").SetValueAsync(UserMoney + AddMoney);
		reference.Child("Diamond").SetValueAsync(UserDiamond + AddDiamond);
	}
	
}
