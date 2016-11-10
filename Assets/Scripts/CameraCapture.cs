using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;

[RequireComponent (typeof (RawImage))]
// Camera controller module
public class CameraCapture : MonoBehaviour 
{
	/// <summary>
	/// The shutter simulator.
	/// </summary>
	public Graphic shutterSimulator;

	/// <summary>
	/// The previews.
	/// </summary>
	public RawImage[] previews;

	/// <summary>
	/// The capture button.
	/// </summary>
	public CanvasGroup captureButton;

	/// <summary>
	/// The VR button.
	/// </summary>
	public CanvasGroup VRButton;

	/// <summary>
	/// The index of the image.
	/// </summary>
	public int imageIndex = 0;

	// This is the rawImage component which will hold the output of the live camera
	public RawImage raw;

	// Storage for the images
	public List<Texture> imageStorage;

	// This connects to the live camera on the device
	private WebCamTexture webCamTexture;

	// Runs at Start
	void Start() 
	{
		// Don't destroy this on load
		DontDestroyOnLoad (this.gameObject);

		// This gets the camera going
		webCamTexture = new WebCamTexture();
		webCamTexture.Play();

		// Set the shuttersimulator to transparent
		if (shutterSimulator)
			shutterSimulator.canvasRenderer.SetAlpha (0);

		imageStorage = new List<Texture> ();
	}

	// Runs once per frame
	void Update()
	{
		if (raw != null)
			raw.texture = webCamTexture;
	}

	// Take a Photo
	public void TakePhoto() {
		if (shutterSimulator) {
			shutterSimulator.canvasRenderer.SetAlpha (1f);
			shutterSimulator.CrossFadeAlpha (0f, 1.5f, true);
		}
		StartCoroutine (_TakePhoto());
	}

	private IEnumerator _TakePhoto()
	{
		// Only finish at the end of the gather frame
		yield return new WaitForEndOfFrame(); 

		// Create the photo object based on the import data
		Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();

		//Encode to a PNG
		byte[] bytes = photo.EncodeToPNG();

		// play audio if available (to indicate that the photo was captured)
		GetComponent<AudioSource>().Play();

		// If the imageIndex is less than the number of previews, add the image to the current previews at Index
		previews[imageIndex].texture = photo;

		// Store the image in the list
		imageStorage.Add (photo);

		// increment the imageIndex
		imageIndex++;

		// If the imageIndex is greater than the number of previews, make the VRMode available
		if (imageIndex >= previews.Length) {
			if (VRButton) {
				VRButton.alpha = 1;
				VRButton.blocksRaycasts = true;
				VRButton.interactable = true;
			}
			if (captureButton) {
				captureButton.alpha = 0;
				captureButton.blocksRaycasts = false;
				captureButton.interactable = false;
			}
		}
	}
}