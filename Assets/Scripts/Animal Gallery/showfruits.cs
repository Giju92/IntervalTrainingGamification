using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class showfruits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		GetComponent<Text> ().text = SaveManager.Instance.GetFruitsList ();
		
	}	

}
