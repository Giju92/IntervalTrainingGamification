using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walking : MonoBehaviour {

	int state = (int)SessionManager.State.walking;

	public Slider slider;
	public Image img;
	public Text timeTxt;


	public void StartState () 
	{
		StartCoroutine(WorkoutCoroutine());

	}

	public IEnumerator WorkoutCoroutine()
	{
		float elapsedTime = 0;

		float time = SaveManager.Instance.GetWalkingTime();
		float timeleft = (int) time;
		float startValue = slider.value;

		Spawner.instance.StartSpawning ();

		while (elapsedTime < time)
		{			
			slider.value = Mathf.Lerp(0, 1, (elapsedTime / time));
			img.color = Color.Lerp( Color.red, Color.green, (elapsedTime / time));
			elapsedTime += Time.deltaTime;

			//showing the time left
			timeleft = SaveManager.Instance.GetWalkingTime() - elapsedTime; 
			timeTxt.text = "" + string.Format("{0:00}",Mathf.Floor(timeleft / 60)) + ":" + string.Format("{0:00}",Mathf.Floor(timeleft % 60));
			yield return new WaitForEndOfFrame();
		}
		timeTxt.text = "00:00";

		Spawner.instance.StopSpawning ();
		yield return new WaitForSeconds(3);
		EndState ();
	}


	void EndState()
	{
		SessionManager.GetInstance().EndState (state);
	}
}
