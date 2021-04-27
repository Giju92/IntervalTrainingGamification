using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shake : MonoBehaviour {

	class Counter
	{
		private int i = 0;

		public void inc()
		{
			lock (this)
			{
				i++;
			}
		}

		public int GetCounter()
		{
			lock (this)
			{
				return i;
			}
		}
	}

	int state = (int) ShakingSessionManager.State.shake;
	Counter shake = null;

	float gravity = 5;
	float shakeThreshold = 0.8f;
	int waitTime = 20;

	float elapsedTime = 0;

	public GameObject fallingFruit;
	public GameObject particleLeaf;
	public static Shake instance;
	int iteration = 0;

	private int[] fruits; 

	void Awake()
	{
		instance = this;
	}

	public void StartState () 
	{
		fruits = new int[12];
		particleLeaf.SetActive (true);
		StartCoroutine(shacker());
		StartCoroutine(controller());
	}

	IEnumerator controller()
	{
		int oldV = 0;
		int newV = 0;
		int gap = 0;
		int cnt = 0;
		iteration = ShakingSessionManager.shakingTime;

		while (shake == null) 
		{
			yield return new WaitForEndOfFrame ();
		}

		oldV = shake.GetCounter();

		while (iteration != 0) {			

			yield return new WaitForSeconds (1);
			newV = shake.GetCounter ();

			gap = newV - oldV;

			if (gap == 0) 
			{
				cnt = 0;

			}
			else if (gap > 0 && gap < 3) 
			{
				cnt = 1;
			} 
			else if (gap >= 3 && gap < 5)
			{
				cnt = 2;
			} 
			else if (gap >= 5 && gap < 10) 
			{
				cnt = 3;
			}

			for (int i = 0; i < cnt; i++) 
			{
				//spawn a fruit
				GameObject gb = Instantiate(fallingFruit);
				gb.transform.SetParent (this.transform);
				int type = gb.GetComponent<FallingFruit> ().Inizialize (null);
				fruits [type]++;
			}
			oldV = newV;
			string s = "gap: " + gap + " count: " + shake.GetCounter(); 
			Debug.Log (s);
			iteration--;
		}

	}


	IEnumerator shacker()
	{
		shake = new Counter();

		elapsedTime = 0;
		waitTime = 20;

		while(elapsedTime < ShakingSessionManager.shakingTime) 
		{			
			float x = Input.acceleration.x;
			float y = Input.acceleration.y;
			float z = Input.acceleration.z;

			float gX = x / gravity;
			float gY = y / gravity;
			float gZ = z / gravity;

			float gForce = Mathf.Sqrt (gX * gX + gY * gY + gZ * gZ);				

			if (gForce > shakeThreshold) 
			{
				shake.inc();
			}

			//waiting time
			for (int i = 0; i < waitTime; i++) {
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}

		}

		StartCoroutine(endState ());
	}

	public int getValueFromIndex(int index){

		return fruits [index];
	} 

	IEnumerator endState(){

		while (iteration != 0)
			yield return new WaitForEndOfFrame ();
		Vibration.Vibrate (1000);

		shake = null;
		particleLeaf.SetActive (false);
		ShakingSessionManager.instance.EndState ((int)state);
	}
}


