using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUpdate : MonoBehaviour {

	public static bool created = false;
	static Coroutine c = null;
	public static ManagerUpdate instance;

	void Awake()
	{
		if (!created) {
			DontDestroyOnLoad (this);
			instance = this;
			created = true;
		} 
		else
		{
			Destroy (this.gameObject);
		}
	}

	public void ForceStart () 
	{		
		if (!SaveManager.Instance.GetLastDate ().Equals("")) 
		{
			CountTimeElapsed ();
		}
		else
		{
			SaveManager.Instance.SetLastDate (System.DateTime.Now.ToString());
		}

		if(c == null)
			c = StartCoroutine (UpdateRoutine());
	}

	void CountTimeElapsed () {

		DateTime t = DateTime.Parse(SaveManager.Instance.GetLastDate ());
		TimeSpan elapsedTime = System.DateTime.Now - t;
		SaveManager.Instance.UpdateAnimals((int) elapsedTime.TotalMinutes / 30);
		SaveManager.Instance.SetLastDate (System.DateTime.Now.ToString());
	}

	IEnumerator UpdateRoutine()
	{
		while (true) 
		{
			DateTime now = System.DateTime.Now;

			if (now.Minute == 30 || now.Minute == 0) 
			{	
				SaveManager.Instance.SetLastDate (now.ToString());
				SaveManager.Instance.UpdateAnimals (1);
			}

			yield return new WaitForSeconds (60);
		}
	}

	void OnApplicationFocus(bool hasFocus)
	{
		CountTimeElapsed ();
	}

	void OnApplicationPause(bool pauseStatus)
	{
		CountTimeElapsed ();
	}

}
