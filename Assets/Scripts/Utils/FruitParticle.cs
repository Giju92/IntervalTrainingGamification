using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (WaitandDestroy ());
	}
	
	IEnumerator WaitandDestroy()
	{
		yield return new WaitForSeconds (2f);
		Destroy(gameObject);
	}
}
