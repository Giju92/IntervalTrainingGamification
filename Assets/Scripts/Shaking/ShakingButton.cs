using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakingButton : MonoBehaviour {

	public int value = 1;
	public int time = 1;


	void Start()
	{
		GetComponent<Button> ().onClick.AddListener (() => OnClick ());
	}

	public void OnClick()
	{
		ShakingSessionManager.instance.ShackingButtonClick(time);
		SaveManager.Instance.SumTicket (-value);
	}

	public void CheckValue()
	{
		
		if (SaveManager.Instance.GetTicketsCount () >= value) 
		{
			GetComponent<Button> ().interactable = true;
		} 
		else
		{
			GetComponent<Button> ().interactable = false;
		}
	}

}
