using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameScript : MonoBehaviour {

	public GameObject button;
	InputField field;

	// Use this for initialization
	void Start () {
		field = this.GetComponent<InputField> ();
		field.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
		field.text = "";
	}
	
	public void ValueChangeCheck()
	{
		if (field.text.Length != 0) 
		{
			//TODO improve, just save the name when it is confirmed
			SessionManager.GetInstance ().animal.SetNickName (field.text);
			button.GetComponent<Button> ().interactable = true;

			//TODO just a test
			//SessionManager.GetInstance().EndState ((int)SessionManager.State.price);

		}
		else
		{
			button.GetComponent<Button> ().interactable = false;
		}			
	}
}
