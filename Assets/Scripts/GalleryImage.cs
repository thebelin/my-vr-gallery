using UnityEngine;

// A gallery image item, for storage in the API
[System.Serializable]
public class GalleryImage {
	// Gallery Image Properties
	public string pictureName;
	public string artist;
	public string url;
	public int width;
	public int height;

	// makes it possible to stack this item in arrays of itself
	public GalleryImage[] files;
}
