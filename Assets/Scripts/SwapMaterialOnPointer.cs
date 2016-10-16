using UnityEngine;
using System.Collections;

public class SwapMaterialOnPointer : MonoBehaviour {

	public Material swapMaterial;

	private Renderer rend;
	private Material startMaterial;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		startMaterial = rend.material;
	}

	public void PointerIn (bool status) {
		Debug.Log ("PointerIn" + status.ToString ());
		if (status) {
			rend.material = swapMaterial;
		} else {
			rend.material = startMaterial;
		}
	}
}
