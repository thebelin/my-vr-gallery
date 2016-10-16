using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Attach this to a stare target to cause that target to do a teleport on the Camera
public class TeleportToTarget : MonoBehaviour {

	// The target to teleport to
	public Transform target;

	// The burn meter to use to indicate burn progress
	// Use the slider min/max for seconds to burn
	public Slider burnSlider;

	// Whether it's currently gazed at
	private bool isGazed = false;

	// The time the gaze started
	private float gazeStart;

	// The camera being teleported
	private GameObject mainCamera;

	// Run on start
	void Start () {
		// Find the camera
		mainCamera = GameObject.FindWithTag("MainCamera");
	}

	// Update is called once per frame
	void Update () {
		// If this is attached to a burnSlider, increment that Slider
		if (isGazed && burnSlider != null) {
			burnSlider.value = Time.fixedTime - gazeStart;
			if (burnSlider.value >= burnSlider.maxValue)
				DoTeleport ();
		} else if (!isGazed && burnSlider != null && burnSlider.value != burnSlider.minValue) {
			burnSlider.value = burnSlider.minValue;
		}
	}

	public void SetGazed () {
		isGazed = true;
		gazeStart = Time.fixedTime;
	}

	public void NotGazed () {
		isGazed = false;
	}

	// Move the camera to the new location
	public void DoTeleport () {
		// Move it to the new location
		if (mainCamera != null)
			mainCamera.transform.position = target.position;
	}
}
