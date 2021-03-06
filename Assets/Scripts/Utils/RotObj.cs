using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour {

	float rotSpeed = 100;

	void OnMouseDrag()
	{
		
		float rotX = Input.GetAxis ("Mouse X") * rotSpeed * Mathf.Deg2Rad;
		transform.Rotate (Vector3.up, -rotX);
	}

	void OnMouseDown()
	{
		this.gameObject.GetComponent<Animator> ().Play ("Squeeze");
	}
}
