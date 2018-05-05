using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterButton : MonoBehaviour {

	void Start ()
	{
		SceneManager.LoadScene ("Register", LoadSceneMode.Additive);
	}
}

