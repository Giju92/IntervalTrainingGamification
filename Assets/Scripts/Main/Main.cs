using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Threading;

public class Main : MonoBehaviour
{
	private AndroidJavaObject curActivity;
	static Main _instance;
	private Text myAccuracy;
	private GameObject progressbar;
	private Button startButton;
	private static int cnt = 0;


	public static Main GetInstance()
	{
		if( _instance == null )
		{
			_instance = new GameObject("Main").AddComponent<Main>();   
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
	}

	void Start()
	{		
		ReStart ();
	}

	public void ReStart()
	{	
		cnt = 0;
		Main.GetInstance().CallJavaFunc( "startService", "GPS_BACKGROUND");
		//Main.GetInstance().CallJavaFunc( "checkStatusGPS", "");
		myAccuracy = (Text)GameObject.Find ("Canvas/MenuContainer/Main/progressbar/accuracy").GetComponent<Text>();
		progressbar = (GameObject)GameObject.Find ("Canvas/MenuContainer/Main/progressbar");
		startButton  = (Button)GameObject.Find ("Canvas/MenuContainer/Main/startbutton").GetComponent<Button>();
		MusicBox.instance.SetVolume (0.7f);

	}


	public void CallJavaFunc( string strFuncName, string strTemp )
	{
		curActivity.Call( strFuncName, strTemp );
	}

	//TODO cnt=5 
	void SetAccuracy(string s)
	{
		if (myAccuracy != null && progressbar != null && startButton != null) 
		{
			myAccuracy.text = s;
			float accuracy = float.Parse (s); 
			if (accuracy > Helper.MIN_ACCURACY && accuracy <= Helper.MAX_ACCURACY) 
			{
				if (cnt < 7) 
				{
					Interlocked.Increment(ref Main.cnt);
					if (cnt == 2) 
					{	
						startButton.interactable = true;
						progressbar.SetActive (false);
						myAccuracy.text = "start!";
					}
				}

			} 
			else
			{
				if (cnt > 0) 
				{
					Interlocked.Decrement(ref Main.cnt);
					if (cnt == 4) 
					{
						startButton.interactable = false;
						progressbar.SetActive (true);
						myAccuracy.text = "wait signal";
					}
				}
			}

		}

	}


}