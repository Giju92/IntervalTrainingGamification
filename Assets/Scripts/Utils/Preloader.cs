using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour 
{
	private CanvasGroup fadeGroup;
	private float loadTime;
	private float minimumLogoTime = 2.0f;


	void Start () 
	{
		fadeGroup = FindObjectOfType<CanvasGroup> ();

		fadeGroup.alpha = 1f;

		//HERE TO PRELOAD THE GAME

		if (Time.time < minimumLogoTime)
			loadTime = minimumLogoTime;
		else
			loadTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Fade-in
		if(Time.time <minimumLogoTime)
		{
			fadeGroup.alpha = 1 - Time.time;
		}

		//Fade-out
		if(Time.time >minimumLogoTime && loadTime !=0)
		{
			fadeGroup.alpha = Time.time - minimumLogoTime;
			if (fadeGroup.alpha >= 1) 
			{
				SceneManager.LoadScene (1);

			}
		}
	}
}
