using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using Firebase.Database;

public class User : MonoBehaviour {
	public Text username;
	public Text country;
	private string nama;
	private string countries;
	public int diamond;
	public int money;
	public System.DateTime date;
	private string joinDate;
	public DatabaseReference mDatabaseRef;
	public Firebase.Auth.FirebaseAuth auth;

	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/Users");

		DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public User() {

	}

	public User(Text username, Text country) {
		this.username = username;
		this.country = country;
		nama = this.username.ToString ();
		countries = this.country.ToString ();
		this.diamond = 0;
		this.money = 0;
		this.date = System.DateTime.Today;
		this.joinDate = date.ToString();
	}

	public void writeNewUser(Text username, Text country) {
		Firebase.Auth.FirebaseUser currUser = auth.CurrentUser;
		User user = new User(username, country);
		string json = JsonUtility.ToJson(user);

		mDatabaseRef.Child("Users").Child(currUser.UserId).SetRawJsonValueAsync(json);
		SceneManager.LoadScene(1);
	}
}
