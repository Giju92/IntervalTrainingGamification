using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour {

	public float maxTime;
	public float minSwipeDist;

	float startTime;
	float endTime;

	float swipeDistance;
	float swipeTime;

	Vector3 startPos;
	Vector3 endPos;

	public GameObject manager;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) 
		{
			//detect just the first finger
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

		if (distance.x > minSwipeDist)
			manager.GetComponent<MainManager>().Swipe("right");
		else if(distance.x < minSwipeDist)
			manager.GetComponent<MainManager>().Swipe("left");

	}
}
