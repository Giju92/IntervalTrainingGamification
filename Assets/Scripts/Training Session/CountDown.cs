using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour {

	int state;
	public GameObject number;

	void Start()
	{
		Scene currentScene = SceneManager.GetActiveScene ();
		string nameScene = currentScene.name;


		if (nameScene == "Shaking")
		{
			state = (int) ShakingSessionManager.State.countdown;
		}
		else if (nameScene == "TrainingSession")
		{
			state = (int) SessionManager.State.countdown;
		}
	}

	public void StartState () 
	{
		number.GetComponent<Animator> ().Play ("TextCountDown");
	}

	public void AbortCountDown()
	{
		number.GetComponent<Animator> ().Play ("Idle");
	}


	public void EndState()
	{
		Scene currentScene = SceneManager.GetActiveScene ();
		string nameScene = currentScene.name;


		if (nameScene == "Shaking")
		{
			ShakingSessionManager.instance.EndState (state);
		}
		else if (nameScene == "TrainingSession")
		{
			SessionManager.GetInstance().EndState (state);
		}


	}
}
