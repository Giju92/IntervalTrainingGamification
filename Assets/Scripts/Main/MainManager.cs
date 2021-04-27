using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

	private CanvasGroup fadeGroup;
	private float fadeInSpeed = 0.33f;

	public RectTransform menuContainer;

	public Canvas canvas;
	public Text level;
	int position = 0;


	private Vector3 desiredMenuPosition;

	public static MainManager Instance { set; get; }

	private void Awake()
	{		
		Instance = this;
	}


	void Start()
	{
		fadeGroup = FindObjectOfType<CanvasGroup> ();

		fadeGroup.alpha = 1f;

		Main.GetInstance ().ReStart ();

		Collection.Instance.InitCollection();
	
		Settings.Instance.InitSettings ();

		level.text = "Your level is: " + SaveManager.Instance.GetLevel ();
	}



	void Update () 
	{
		fadeGroup.alpha = 1 - Time.time*fadeInSpeed;

		//menuContainer.anchoredPosition3D = Vector3.Lerp (menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);

	}

	private void NavigateTo(int menuIndex)
	{		

		switch (menuIndex) 
		{
			default:
			case 0:
				menuContainer.anchoredPosition3D = Vector3.zero;
				//desiredMenuPosition = Vector3.zero;
				break;
			case -1:
				menuContainer.anchoredPosition3D = Vector3.right*1080;
				//desiredMenuPosition = Vector3.right*1080;
				break;
			case 1:
				menuContainer.anchoredPosition3D =	Vector3.left*1080;
				//desiredMenuPosition = Vector3.left*1080;
				break;
		}
	}

	//NAVIGATION BUTTON
	public void ShowMain()
	{
		position = 0;
		NavigateTo (0);
	}

	public void ShowCollection()
	{
		position = 1;
		NavigateTo (1);
	}

	public void Swipe(string s)
	{
		if (s.Equals ("right")) {
			if (position == 0)
				ShowSettings ();
			else if (position == 1)
				ShowMain ();
		} 
		else
		{
			if (position == 0)
				ShowCollection ();
			else if (position == -1)
				ShowMain ();
		}
	}

	public void ShowSettings()
	{
		Debug.Log ("click");
		position = -1;
		NavigateTo (-1);
	}


	public void ShowMessage()
	{
		Debug.Log ("click");
		menuContainer.anchoredPosition3D = new Vector3 (0, -1920, 0);
	}


	public void StarSession()
	{
		SceneManager.LoadScene ("TrainingSession");
	}

	public void StarGallery()
	{
		SceneManager.LoadScene ("AnimalGallery");
	}

	public void StarMission()
	{
		SceneManager.LoadScene ("Missions");
	}

	public void StarShakingSession()
	{
		SceneManager.LoadScene ("Shaking");
	}

}
