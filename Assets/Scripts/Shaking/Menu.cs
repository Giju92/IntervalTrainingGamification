using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Button b1;
	public Button b2;
	public Button b3;
	public Text ticket;


	public void StartState () 
	{
		b1.GetComponent<ShakingButton> ().CheckValue ();
		b2.GetComponent<ShakingButton> ().CheckValue ();
		b3.GetComponent<ShakingButton> ().CheckValue ();
		ticket.text = "Your ticket/s: " + SaveManager.Instance.GetTicketsCount ();
	}

}
