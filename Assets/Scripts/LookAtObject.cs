using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {
	private GameObject target;
	void Start () {
		target = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	void LateUpdate () {
		transform.LookAt (target.transform);
	}
}
