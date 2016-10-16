using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointerCounter : MonoBehaviour {

	public int totalPointers = 10;
	public int userPointers = 0;

	private Text text;
	private GvrAudioSource src;

	public void markPointed () {
		userPointers++;
		text.text = userPointers.ToString () + "/" + totalPointers.ToString ();
		Debug.Log ("src" + src.clip.length);

		src.Play ();
	}
	public void Start () {
		GalleryCanvas[] canvases = FindObjectsOfType<GalleryCanvas> ();
		totalPointers = canvases.Length;
		text = GetComponent<Text> ();
		src = GetComponent<GvrAudioSource> ();
	}
}
