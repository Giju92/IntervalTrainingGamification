using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	//for manage the password
	public InputField passwordField;
	private string pass = "test";
	public GameObject distance;
	public GameObject runningTime;
	public GameObject walkingTime;

	public static Settings Instance { set; get; }

	private void Awake()
	{		
		Instance = this;
	}

	public void InitSettings()
	{
		passwordField.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}

	// Invoked when the value of the text field changes.
	public void ValueChangeCheck()
	{
		if (passwordField.text.Equals (pass)) 
		{
			distance.GetComponent<Scrollbar> ().interactable = true;
			runningTime.GetComponent<Scrollbar> ().interactable = true;
			walkingTime.GetComponent<Scrollbar> ().interactable = true;
		}
		else
		{
			distance.GetComponent<Scrollbar> ().interactable = false;
			runningTime.GetComponent<Scrollbar> ().interactable = false;
			walkingTime.GetComponent<Scrollbar> ().interactable = false;
		}			
	}

	public void ExitSetting()
	{
		passwordField.text = "";
		MainManager.Instance.ShowMain ();
	}
}
