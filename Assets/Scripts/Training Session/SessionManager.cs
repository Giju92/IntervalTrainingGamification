using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.SceneManagement;
using TheNextFlow.UnityPlugins;

public class SessionManager: MonoBehaviour  
{
	private AndroidJavaObject curActivity;
	static SessionManager _instance;
	static int tot_lenght = 0;
	public Animal animal;
	public State currentState;

	RectTransform Container;

	RectTransform Recap;
	RectTransform CountDown;
	RectTransform Workout;
	RectTransform Result;
	RectTransform Price;
	RectTransform Walking;
	RectTransform FruitResult;

	private int[] distance = {-1,-1,-1,-1,-1};
	private int step = 0;
	private int attempt = 0;
	private static bool capture = false;
	private bool finished = false;

	public enum State {recap,countdown,workout,result,price,walking,fruit};

	public static SessionManager GetInstance()
	{
		if( _instance == null )
		{
			_instance = new GameObject("SessionManager").AddComponent<SessionManager>();   
		}
		return _instance;
	}

	void Awake()
	{
		#if UNITY_ANDROID 
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		curActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
		#endif

		DontDestroyOnLoad (gameObject);

		Container = GameObject.Find ("Canvas/Container").GetComponent<RectTransform>();
		Recap = GameObject.Find ("Canvas/Container/Recap").GetComponent<RectTransform>();
		CountDown = GameObject.Find ("Canvas/Container/CountDown").GetComponent<RectTransform>();
		Workout = GameObject.Find ("Canvas/Container/Workout").GetComponent<RectTransform>();
		Result = GameObject.Find ("Canvas/Container/Result").GetComponent<RectTransform>();
		Price = GameObject.Find ("Canvas/Container/Price").GetComponent<RectTransform>();
		Walking = GameObject.Find ("Canvas/Container/Walking").GetComponent<RectTransform>();
		FruitResult = GameObject.Find ("Canvas/Container/FruitResult").GetComponent<RectTransform>();
	}

	public void CallJavaFunc( string strFuncName, string strTemp )
	{
		curActivity.Call( strFuncName, strTemp );
	}

	void Start()
	{
		SessionManager.GetInstance().CallJavaFunc( "setAccuracy", "" + Helper.MAX_ACCURACY_RUN);
		SessionManager.GetInstance().CallJavaFunc( "startService", "GPS_RUN");
		//start recap scene
		Recap.GetComponent<Recap>().StartState();
		MusicBox.instance.SetVolume (0.3f);
		currentState = State.recap;
	}

	public void ReStart()
	{	
		step = 0;	
		attempt = 0;
		capture = false;
		finished = false;
		tot_lenght = 0;
		animal = new Animal ();
		animal.setAnimal ();
		currentState = State.recap;

		SessionManager.GetInstance().CallJavaFunc( "setAccuracy", "" + Helper.MAX_ACCURACY_RUN);
		SessionManager.GetInstance().CallJavaFunc( "startService", "GPS_RUN");
		Container = GameObject.Find ("Canvas/Container").GetComponent<RectTransform>();
		Recap = GameObject.Find ("Canvas/Container/Recap").GetComponent<RectTransform>();
		CountDown = GameObject.Find ("Canvas/Container/CountDown").GetComponent<RectTransform>();
		Workout = GameObject.Find ("Canvas/Container/Workout").GetComponent<RectTransform>();
		Result = GameObject.Find ("Canvas/Container/Result").GetComponent<RectTransform>();
		Price = GameObject.Find ("Canvas/Container/Price").GetComponent<RectTransform>();
		Walking = GameObject.Find ("Canvas/Container/Walking").GetComponent<RectTransform>();
		FruitResult = GameObject.Find ("Canvas/Container/FruitResult").GetComponent<RectTransform>();
		Recap.GetComponent<Recap>().StartState();
		//event for changing image
		GameObject.Find ("Canvas/Container/Recap/Egg/Animal").GetComponent<AnimalImage>().UpdateImage();
		GameObject.Find ("Canvas/Container/Price/BigEgg/Animal").GetComponent<AnimalImage>().UpdateImage();
	}

	void UpdateRun(string s)
	{	
		if (capture) 
		{
			string[] subStrings = s.Split('/');
			float accuracy = float.Parse(subStrings[0]);
			float fdist = float.Parse(subStrings[1]);
			if ( accuracy > Helper.MIN_ACCURACY && accuracy < Helper.MAX_ACCURACY) 
			{
				//int d = (int)Mathf.Round(fdist);
				int d = (int)Mathf.Ceil(fdist);
				Interlocked.Add(ref SessionManager.tot_lenght, d);
			}			
		}
	}

	public void EndState(int state)
	{
		switch (state) {
			case (int)State.recap:
				Container.anchoredPosition3D = Vector3.left * 1080 * 2;
				currentState = State.countdown;
				CountDown.GetComponent<CountDown> ().StartState ();	

				SessionManager.GetInstance().CallJavaFunc( "startService", "STEP_DETECTOR");
				break;
			case (int)State.countdown:
				Container.anchoredPosition3D = Vector3.left * 1080 * 4;
				currentState = State.workout;
				step = 0;
				Workout.GetComponent<Workout> ().StartState ();				
				break;
			case (int)State.workout:
				Container.anchoredPosition3D = Vector3.left * 1080 * 6;
				currentState = State.result;
				Result.GetComponent<Result> ().StartState ();
				SessionManager.GetInstance().CallJavaFunc( "stopService", "STEP_DETECTOR");
				break;
			case (int)State.result:
				Container.anchoredPosition3D = Vector3.left * 1080 * 8;
				currentState = State.price;
				Price.GetComponent<Price> ().StartState ();				
				break;
			case (int)State.price:
				
				if (finished) 	
				{
					SaveAnimal ();
					EndSession ();
					break;
				}		
				Container.anchoredPosition3D = Vector3.left * 1080 * 10;
				currentState = State.walking;				
				Walking.GetComponent<Walking> ().StartState ();
				break;
			case (int)State.walking:
				Container.anchoredPosition3D = Vector3.left * 1080 * 12;
				currentState = State.result;	
				FruitResult.GetComponent<FruitResult> ().StartState ();
				break;
			case (int)State.fruit:
				attempt++;
				if (attempt == Helper.MAX_ATTEMPT)
					finished = true;			

				Container.anchoredPosition3D = Vector3.zero;
				currentState = State.recap;	
				Recap.GetComponent<Recap> ().StartState ();
				break;
			}		

	}

	public IEnumerator MoveCameraDownRoutine()
	{
		float time = 0.5f;
		float elapsedTime = 0;
		Vector3 startPosition = Container.anchoredPosition3D;

		while (elapsedTime < time)
		{
			startPosition.y = (float) Mathf.Lerp(0, 1920, (elapsedTime / time));

			Container.anchoredPosition3D = startPosition;

			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		startPosition.y = 1920;
		Container.anchoredPosition3D = startPosition;
	}


	public void StartCapture()
	{
		capture = true;		
	}

	public void EndCapture()
	{
		if (step > 0) {
			distance [attempt] = tot_lenght;

			capture = false;

			if (GetPercentage () > 0.83) 
			{
				finished = true;
				//Just unlock the animal type
				SaveManager.Instance.UnlockAnimal (animal.type);
			}			
		}
		else
		{
			//discard the new distance if no step revealed 
			if (attempt > 0)
				tot_lenght = distance [attempt - 1];
			
			distance [attempt] = tot_lenght;
			AndroidNativePopups.OpenToast ("NO STEPS DETECTED", AndroidNativePopups.ToastDuration.Long);
		}
	}

	public void incStepCounter(string s)
	{
		step += int.Parse (s);
	}


	public void SaveAnimal()
	{		
		SaveManager.Instance.AddAnimal (animal);
	}

	public int getLastRunningDistance()
	{
		if(attempt > 0)
			return distance [attempt] - distance [attempt - 1];

		return distance [attempt];
	}

	public int GetTotalDistance()
	{
		return distance [attempt];
	}

	public int GetAttempt()
	{		
		return attempt;
	}

	public float GetPercentage()
	{
		return (float)tot_lenght / SaveManager.Instance.GetDistance ();
	}

	public bool IsFinished()
	{
		return finished;
	}

	public void EndSession()
	{
		SessionManager.GetInstance().CallJavaFunc( "stopService", "GPS_RUN");
		//Add ticket according to the attempts left
		SaveManager.Instance.SumTicket (Helper.MAX_ATTEMPT - attempt);
		SceneManager.LoadScene (1);
	}

	public void UpdateWalking(string s){
		Spawner.instance.CheckSpawn(s);
	}

	void OnApplicationFocus(bool hasFocus)
	{
		ManagerPauseFocus (hasFocus);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		ManagerPauseFocus (pauseStatus);
	}

	void ManagerPauseFocus(bool state)
	{
		if (currentState == State.workout) {
			
			if (state) 
			{
				attempt--;
				EndState ((int)State.fruit);
			}
			else 
			{
				capture = false;

				if (attempt > 0)
					tot_lenght = distance [attempt - 1];
				
				Workout.GetComponent<Workout>().AbortWorkout();

			}

		}
		else if (currentState == State.countdown) 
		{
			if (state) 
			{
				attempt--;
				EndState ((int)State.fruit);
			} 
			else 
			{
				CountDown.GetComponent<CountDown> ().AbortCountDown ();
			}
			
		}
		else if(currentState == State.walking)
		{
			Spawner.instance.RestartStopSpawning (state);
		}
	}

}
