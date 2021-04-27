using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour {

	public enum Setting {distance,walkingTime,runningTime};
	public Text myText;
	public Scrollbar myScrollBar;
	public float max;
	public float min;
	public Setting type;

	// Use this for initialization
	void Start () 
	{
		switch (type) 
		{
			case Setting.distance:
				myText.text = "" + SaveManager.Instance.GetDistance();
				break;

			case Setting.walkingTime:
				myText.text = "" + SaveManager.Instance.GetWalkingTime();
				break;

			case Setting.runningTime:
				myText.text = "" + SaveManager.Instance.GetRunningTime();
				break;
		}

		myScrollBar.onValueChanged.AddListener( delegate { valueUpdate();});

		myScrollBar.value = float.Parse(myText.text)/(max-min);

	}


	public void valueUpdate()
	{
		myText.text = "" + Mathf.Round((float)reScale(myScrollBar.value));

		switch (type) 
		{
		case Setting.distance:
			SaveManager.Instance.SetDistance(int.Parse(myText.text));
			break;

		case Setting.walkingTime:
			SaveManager.Instance.SetWalkingTime(int.Parse(myText.text));
			break;

		case Setting.runningTime:
			SaveManager.Instance.SetRunningTime(int.Parse(myText.text));
			break;
		}
	}

	private double reScale(float x)
	{
		return ((max-min)*x + min);	
	}

}
