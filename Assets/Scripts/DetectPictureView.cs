using UnityEngine;
using System.Collections;

public class DetectPictureView : MonoBehaviour {
	// This should be the object which will get the glow when the picture is highlighted
	public GameObject glowedThing;

	// whether the thing is seen
	public bool isSeen = false;

	// The material to glow with
	public Material glowMaterial;

	// The original material for the item
	private Material originalMaterial;

	// The renderer of the glowedThing
	private Renderer rend;

	private PointerCounter pc;

	// Call when the pointer enters
	private void onPointer(bool pointed) {
		// Debug.Log ("onPointer");
		if (pointed && !isSeen) {
			isSeen = true;
			if (pc != null)
				pc.markPointed();
		}

		// do outline glow
		if (pointed && rend != null && glowMaterial != null)
			rend.material = glowMaterial;

		// remove outline glow
		if (!pointed && rend != null && originalMaterial != null)
			rend.material = originalMaterial;
	}

	// Use this for initialization
	void Start () {
		// Get the PointerCounter
		pc = FindObjectOfType<PointerCounter> ();

		// Store the original material
		if (glowedThing != null) {
			rend = glowedThing.GetComponent<Renderer> ();
			originalMaterial = rend.material;
		}
	}
	
	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		onPointer(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		onPointer(false);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		onPointer(true);
	}

	#endregion
}
