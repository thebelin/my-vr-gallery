using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserConfig : MonoBehaviour {
	public string defaultUserCode;
	public string usercode;
	public Text textBox;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		SceneManager.activeSceneChanged += newScene;
	}

	void newScene (Scene a, Scene b) {
		// Find the FillGalleryImages and update the setting
		FillGalleryImages fgi = FindObjectOfType<FillGalleryImages>();
		if (fgi != null) {
			fgi.GetGallery (usercode);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void setUserCode (string newCode) {
		if (textBox != null)
			newCode = textBox.text;

		Debug.Log ("setUserCode:" + newCode);
		usercode = newCode;
	}

	public void sendUserCode () {
		if (textBox != null)
			usercode = textBox.text;
		loadGallery ();	
	}
	public void loadDefaults() {
		usercode = defaultUserCode;
		loadGallery ();
	}

	private void loadGallery() {
		SceneManager.LoadScene ("BasicGallery");
	}
}
