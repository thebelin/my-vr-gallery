using UnityEngine;
using System.Collections;
using System;

public class GalleryCanvas : MonoBehaviour {

	// The texture which will be applied to this image
	public Texture canvasTexture;

	// the object's renderer, fetch at start
	private Renderer rend;

	// Load a texture onto this item
	public void loadTexture(Texture newTex) {
		if (rend)
			rend.material.mainTexture = newTex;
	}

	// Load a texture from a byte array
	public void loadImage(byte[] image) {
		Texture2D tex = new Texture2D (2, 2);
		tex.LoadImage (image);
		loadTexture (tex);
	}

	// Load an image from a url
	public IEnumerator getImage(string url) {
		// Start a download of the given URL
		WWW www = new WWW(url);

		// Wait for download to complete
		yield return www;

		// assign texture
		loadTexture(www.texture);
	}

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
}
