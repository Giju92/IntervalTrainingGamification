using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalleryManager : MonoBehaviour {

	public static int animalIndex = -1;
	public static GameObject particle;
	public static GameObject slider;
	public static GameObject foodInfo;

	int dim;

	public Image balloon;
	public Image img;
	public Text txt;


	//swipe variables
	public float maxTime;
	public float minSwipeDist;

	float startTime;
	float endTime;

	float swipeDistance;
	float swipeTime;

	Vector3 startPos;
	Vector3 endPos;

	void Awake(){
		particle = GameObject.Find("Canvas/Particle System");
		slider = GameObject.Find("Canvas/Schedule/Slider");
		foodInfo = GameObject.Find("Canvas/Schedule/FoodInfo");
	
	}

	// Use this for initialization
	void Start () {

		dim = SaveManager.Instance.GetListDimension ();
		if (dim == 0) {
			txt.text = "no animal";
			foodInfo.GetComponent<Text>().text = ""; 
			balloon.gameObject.SetActive(false);
			img.gameObject.SetActive(false);
			slider.gameObject.SetActive(false);

		} else {
			animalIndex = 0;
			UpdateAnimal ();
		}

		var mainModule = particle.GetComponent<ParticleSystem> ().main;
		mainModule.playOnAwake = true;
		particle.GetComponent<AudioSource> ().playOnAwake = true;
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("Main");

		//TODO find a way to make the y resizable
		if (Input.touchCount > 0 && Input.GetTouch (0).position.y > 300 ) 
		{
			
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {

				startTime = Time.time;
				startPos = touch.position;


			} else if (touch.phase == TouchPhase.Ended) {

				endTime = Time.time;
				endPos = touch.position;

				//give the distance by two points
				swipeDistance = (endPos - startPos).magnitude;
				swipeTime = endTime - startTime;

				if (swipeTime < maxTime && swipeDistance > minSwipeDist) {

					Swipe();

				}

			}

		}

	}

	void Swipe()
	{

		Vector2 distance = endPos - startPos;

		if (distance.x > minSwipeDist) {
			if (animalIndex > 0) {
				animalIndex--;
				UpdateAnimal ();
			} 
		} 
		else if (distance.x < minSwipeDist) {
			if (animalIndex < dim - 1) {
				animalIndex++;
				UpdateAnimal ();
			}
		}			

	}

	void UpdateAnimal()
	{
		Animal a = SaveManager.Instance.GetListFromIndex (animalIndex);

		txt.text = a.nickName;
		foodInfo.GetComponent<Text>().text = a.food + "/100"; 

		img.sprite = AnimalType.GetImgFromIndex (a.type);
		img.color = a.color;

		slider.GetComponent<Slider>().value = a.getPercentage ();

		Balloon.instance.Greeting ();
	}


	public void BackClick(){

		SceneManager.LoadScene (1);
	}


	public static void feedAnimation(Color c){
		
		var mainModule = particle.GetComponent<ParticleSystem> ().main;
		mainModule.startColor = c;

		particle.SetActive (false);
		particle.SetActive (true);

		Animal a = SaveManager.Instance.GetListFromIndex (animalIndex);
		slider.GetComponent<Slider>().value = a.getPercentage ();
		foodInfo.GetComponent<Text>().text = a.food + "/100"; 

	}
}
