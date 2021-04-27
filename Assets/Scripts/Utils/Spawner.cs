using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour {

	public GameObject fruit;
	private AndroidJavaObject curActivity;
	public static Spawner instance;

	private int SpawnedObject = 0;
	private int[] fruits; 
	int count = 0;
	bool active = true;

	void Start(){
		instance = this;
	}

	public void StartSpawning(){

		SessionManager.GetInstance().CallJavaFunc( "startService", "GPS_WALKING");
		active = true;
		SpawnedObject = 0;
		fruits = new int[12];
		count = 0;
	}

	public void RestartStopSpawning(bool isActive){

		if (isActive)
		{
			SessionManager.GetInstance().CallJavaFunc( "startService", "GPS_WALKING");
		}
		else
		{
			SessionManager.GetInstance().CallJavaFunc( "stopService", "GPS_WALKING");
		}	

		active = isActive;
	}


	public void StopSpawning(){
		SessionManager.GetInstance().CallJavaFunc( "stopService", "GPS_WALKING");
		SaveManager.Instance.AddFruits (fruits);
		active = false;
	}

	public void CatchObject(int i)
	{
		fruits[i]++;
	}

	public void CheckSpawn(string s){

		//TODO change min value
		float minSpeed = 0.0f;
		float maxSpeed = 1.8f;


		if (active) 
		{
			string[] subStrings = s.Split('/');
			float accuracy = float.Parse(subStrings[0]);
			float speed = float.Parse(subStrings[1]);
			if ( accuracy > Helper.MIN_ACCURACY && accuracy < Helper.MAX_ACCURACY) 
			{
				if (speed < maxSpeed && speed > minSpeed) 
				{
					count++;
				}
				else 
				{
					//TODO find a better decrementation
					count--;
					if (count < 0)
						count = 0;
				}
			}
			StartCoroutine (Spawn());
		}
	}


	IEnumerator Spawn()
	{
		int threshold0 = 0; 
		int threshold1 = 10; 
		int threshold2 = 18;
		int threshold3 = 24;
		int threshold4 = 28;

		int i = 0;

		if ( threshold0 < count && count < threshold1) {
			i = 1;
		} else if (threshold1 <= count && count < threshold2) {
			i = 2;
		} else if (threshold2 <= count && count < threshold3) {
			i = 3;
		}else if (threshold3 <= count && count < threshold4) {
			i = 4;		
		}else if (count >= threshold4) {
			i = 5;
		}

		for (int j = 0; j < i; j++) {
			if (active) {
				SpawnedObject++;
				GameObject f = Instantiate (fruit);
				f.transform.parent = this.transform;
				f.transform.localPosition = new Vector3 (Random.Range (-(Screen.width / 2)+200, (Screen.width / 2)-200), Random.Range (-(Screen.height / 2)+200, (Screen.height / 2)-200), 0);
				yield return new WaitForSeconds (0.25f);
			} else
				break;
		}
	}


	public int getValueFromIndex(int index){
	
		return fruits [index];
	}

	public int getTotalSpawnedObject(){

		return SpawnedObject;
	}
}
