using System.Collections;
using UnityEngine;

public class MyVibration : MonoBehaviour {

	public MyVibration instance;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad (transform.gameObject);		
	}

	public void Vibrate()
	{
		StartCoroutine ("IncrementalVibration");
	}


	public IEnumerator simpleVibration()
	{
		int cnt = (int)Helper.DURATION_TIME;
		float fraction = 1000f / (float)(Helper.DURATION_TIME * 1);

		while (cnt > 0) 
		{
			long time = (long)fraction * cnt;
			Vibration.Vibrate (time);
			yield return new WaitForSeconds (1f);
			cnt--;
		}
		Vibration.Cancel ();

	}

	public IEnumerator IncrementalVibration()
	{
		long[] pattern = new long[1000];
		pattern [0] = 0;
		int x = (int) Helper.DURATION_TIME;
		int c = 800;
		long y =(long) (c / x);

		long tot_sum = 0;
		int i = 0;
		int cnt = 0;
		//Creating the pattern
		while(tot_sum < Helper.DURATION_TIME*1000) 
		{			
			i++;
			pattern [i] = 100;
			tot_sum += pattern [i];

			i++;
			long sub = (long)(1000 - y * cnt );

			if (sub <= 0)
				sub = y;

			pattern [i] = sub;
			tot_sum += pattern [i];	
			cnt++;
		}


		Vibration.Vibrate (pattern, 1);
		yield return new WaitForSeconds (Helper.DURATION_TIME);
		Vibration.Cancel ();

		Vibration.Vibrate (1000);
	}
}

