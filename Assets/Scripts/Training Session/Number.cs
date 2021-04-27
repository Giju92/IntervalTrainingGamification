using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Number : MonoBehaviour {

	public GameObject manager;

	public void StartCountDown()
	{
		manager.GetComponent<CountDown> ().EndState ();

	}
}
