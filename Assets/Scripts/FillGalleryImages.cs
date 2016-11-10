using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System;
using System.Text;
using System.IO;

public class FillGalleryImages : MonoBehaviour {
	// The root of the api for fetching the gallery data from
	public string apiUrl;

	// This should be set to the gallery id to fetch
	public string galleryId;

	// Holder for retrieved images (they are in an array which is a property of this class)
	private GalleryImage images;

	// This should be attached to all the canvas objects in the scene
	public GalleryCanvas[] canvases;

	// This is the CameraCapture object
	// It might have images already captured
	private CameraCapture camCap;

	// Use this for initialization
	void Start () {
		// get all the display gallery canvases
		canvases = FindObjectsOfType<GalleryCanvas> ();

		// Get the camera capture object
		camCap = FindObjectOfType<CameraCapture>();

		// If the camCap object is loaded with images, use it as the source of this tour
		if (camCap.imageIndex != 0) {
			Debug.Log ("adding raw items to gallery canvas");
			showTexturesOnCanvases (camCap.imageStorage.ToArray());
		} else if (galleryId != null) {
			GetGallery (galleryId);
		}
	}

	public void showTexturesOnCanvases (Texture[] raws) {
		for (int i = 0; (i < raws.Length && i < canvases.Length); i++) {
			Debug.Log ("show raw on canvas " + i.ToString () + " " + canvases[i].name);
			if (raws[i] != null && canvases [i] != null)
				canvases [i].loadTexture (raws [i]);
		}
	}

	public void GetGallery(string id) {
		// Set the gallery id
		galleryId = id;

		// Get the Gallery JSON
		StartCoroutine (GetGalleryJSON (loadJSON));
	}


	private IEnumerator GetGalleryJSON(Action<string> callback = null) {
		WWW www = new WWW(apiUrl + "/json/" + galleryId);
		yield return www;
		if (callback != null)
			callback (www.text);
	}

	void loadJSON (string jsonData) {
		try {
			Debug.Log("loadJSON -> jsonData: " + jsonData);
			Debug.Log ("gallery canvas count: " + canvases.Length.ToString ());

			// Read the collection of gallery images into an array of classes
			images = JsonUtility.FromJson<GalleryImage>(jsonData);

			Debug.Log ("Got images.files: " + JsonUtility.ToJson(images));

			// Process each of the images and place them within the scene
			for (int i = 0 ; (i < images.files.Length && i < canvases.Length); i++) {
				Debug.Log ("image file " + i + ":" + images.files[i].url.ToString ());

				// Get the image from the server and load it as a texture
				StartCoroutine(canvases[i].getImage(images.files[i].url));


				// @todo: The aspect ratio of the image canvas should determine the aspect ratio of the plane the image is displayed on

			}

		} catch(Exception e) {
			Debug.Log (e.ToString());
		}
	}

	// File loader for reading into a string
	private bool Load(string fileName, Action<string> callback)
	{
		// holds the output
		string outStr = "";

		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();

					if (line != null)
					{
						outStr += line;
					}
				}
				while (line != null);

				callback(outStr);
				
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}
}
