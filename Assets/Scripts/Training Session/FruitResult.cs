using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitResult : MonoBehaviour {

	int state = (int)SessionManager.State.fruit;
	public GameObject father;
	public Text result;
	public Text comment;

	public void StartState () 
	{
		for (int i = 0; i < 12; i++) {

			father.transform.GetChild(i).GetComponent<Text>().text = "";
		}
		comment.color = Color.clear;
		result.text = "";
		StartCoroutine(ShowScore());
	}

	IEnumerator ShowScore()
	{
		

		int total = 0;

		for (int i = 0; i < 12; i++) {
			Text label = father.transform.GetChild (i).GetComponent<Text> ();
			int max = Spawner.instance.getValueFromIndex (i);
			if(max > 0)
				GetComponent<AudioSource> ().Play ();

			label.text = "0";
			for (int j = 1; j <= max; j++) {
				label.text = "" + j;
				total++;
				yield return new WaitForSeconds (0.1f);
			}
			GetComponent<AudioSource> ().Stop ();
			yield return new WaitForSeconds (0.2f);
		}

		if(total > 0)
			GetComponent<AudioSource> ().Play ();
		float delay = 0.08f;
		for (int i = 0; i <= total; i++) 
		{
			result.text = "" + i;

			yield return new WaitForSeconds (delay);
		}
		GetComponent<AudioSource> ().Stop ();


		yield return new WaitForSeconds (0.5f);
		result.text += " of "; 
		yield return new WaitForSeconds (0.5f);
		result.text += Spawner.instance.getTotalSpawnedObject ();

		yield return new WaitForSeconds (1f);
		if (Spawner.instance.getTotalSpawnedObject () == total && total != 0) {

			comment.color = Color.green;
			Vibration.Vibrate (1000);

		}
		yield return new WaitForSeconds (6);

		EndState ();
	}


	void EndState()
	{
		SessionManager.GetInstance().EndState (state);
	}
}
