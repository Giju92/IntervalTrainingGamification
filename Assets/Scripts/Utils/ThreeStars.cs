using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeStars : MonoBehaviour {

	//set but to be loaded
	public float MAX_DISTANCE = 100;
	public float DISTANCE = 60;

	public Slider slider;
	public Image img;
	public float target;
	public bool finished = false;
	public float TIME1 = 10;
	public float TIME2 = 10;
	public float TIME3 = 10;
	public float TIME4 = 1;
	public float TIME5 = 1;
	public float TIME6 = 1;
	public float TIME7 = 1;
	public float TIME8 = 1;
	public float TIME9 = 1;
	public float TIME10 = 1;

	// Use this for initialization
	void Start () {
		target = DISTANCE / MAX_DISTANCE;
		StartCoroutine (StartAnimation());
		StartCoroutine (changeColor());
	}

	private IEnumerator StartAnimation()
	{	
		target = DISTANCE / MAX_DISTANCE;

		float elapsedTime = 0;

		//FIRST ITERATION
		float fractTime = TIME1/3;

		float startValue = slider.value;
		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, 1, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		startValue = slider.value;
		elapsedTime = 0;
		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, 0, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		startValue = slider.value;
		elapsedTime = 0;
		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, target, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}


		StartCoroutine(anStep(0.3f,TIME2));
		yield return new WaitForSeconds (TIME2);

		

		StartCoroutine(anStep(0.2f,TIME3));
		yield return new WaitForSeconds (TIME3);
		

		StartCoroutine(anStep(0.1f,TIME4));
		yield return new WaitForSeconds (TIME4);

		startValue = slider.value;
		elapsedTime = 0;
		while (elapsedTime < TIME5)
		{
			slider.value = Mathf.Lerp(startValue, target, (elapsedTime / TIME5));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		slider.value = target;
	}


	public IEnumerator anStep(float percentage, float duration)
	{

		float elapsedTime = 0;

		float fractTime = duration/4;
		float startValue = slider.value;
		float random = Random.Range (-0.2f, 0.2f);

		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, target + percentage + random, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		startValue = slider.value;
		elapsedTime = 0;
		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, target + random, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		startValue = slider.value;
		elapsedTime = 0;
		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, target - percentage + random, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		startValue = slider.value;
		elapsedTime = 0;
		while (elapsedTime < fractTime)
		{
			slider.value = Mathf.Lerp(startValue, target + random, (elapsedTime / fractTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}


	}

	public IEnumerator changeColor()
	{

		float elapsedTime = 0;

		float time = TIME1 + TIME2 + TIME3 + TIME4 + TIME5;
		float startValue = slider.value;


		while (elapsedTime < time)
		{
			
			img.color = Color.Lerp(Color.red, Color.yellow, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}


	}






	

}
