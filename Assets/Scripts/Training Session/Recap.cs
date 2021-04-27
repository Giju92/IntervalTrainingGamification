using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recap : MonoBehaviour {

	int state = (int) SessionManager.State.recap;
	public Text info;
	public Text buttonTxt;

	public void StartState () 
	{
		if (SessionManager.GetInstance ().IsFinished ())
			buttonTxt.text = "RETURN";
		else
			buttonTxt.text = "START";
		

		info.text = "" + SessionManager.GetInstance().GetAttempt () + "-" + Helper.MAX_ATTEMPT;
	}

	public void OnClick()
	{
		if (SessionManager.GetInstance ().IsFinished ()) 
			SessionManager.GetInstance().EndSession();
		else
			EndState();
	}

	public void OnClickEndSession()
	{	
		SessionManager.GetInstance().EndSession();		
	}

	void EndState()
	{
		SessionManager.GetInstance().EndState (state);
	}
}
