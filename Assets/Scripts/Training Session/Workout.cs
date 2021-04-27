using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Workout : MonoBehaviour {

	int state = (int) SessionManager.State.workout;
	public Slider slider;
	public Image img;
	public GameObject background;
	public GameObject whistle;
	Coroutine c;
	AudioSource audio;


	public void StartState () 
	{
		audio = this.GetComponent<AudioSource> ();
		c = StartCoroutine(WorkoutCoroutine());
	}

	public IEnumerator WorkoutCoroutine()
	{		
		float elapsedTime = 0;

		float time = SaveManager.Instance.GetRunningTime();
		float startValue = slider.value;

		SessionManager.GetInstance().StartCapture ();

		whistle.SetActive(false);
		//run audio, 
		audio.time = 30 - time;
		audio.Play ();

		Animator animator = background.gameObject.GetComponent<Animator>();
		while (elapsedTime < time)
		{
			animator.speed = Mathf.Lerp(1, 5, (elapsedTime / time));
			slider.value = Mathf.Lerp(0, 1, (elapsedTime / time));
			img.color = Color.Lerp(Color.green, Color.red, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}		 
		whistle.SetActive(true);
		SessionManager.GetInstance().EndCapture ();

		audio.Stop ();
		EndState ();
	}

	public void AbortWorkout()
	{
		StopCoroutine (c);
		audio.Stop ();
	}

	void EndState()
	{		
		//TODO reset all the variable and the object the the initial value
		SessionManager.GetInstance().EndState (state);
	}

}
