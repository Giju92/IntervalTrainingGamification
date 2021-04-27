using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SessionManager.GetInstance ().ReStart ();
	}
	

}
