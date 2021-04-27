using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

	int state = (int)SessionManager.State.result;

	public Slider stars;
	public Slider slider;
	public GameObject ParentFillArea;
	public GameObject Explosion;
	public GameObject Fall;
	public AudioClip Filling;
	public AudioClip FairyDustSound;

	private AudioSource mAudio;

	void Start(){
		mAudio = GetComponent<AudioSource> ();
	}


	// Use this for initialization
	public void StartState () 
	{
		Fall.SetActive (false);
		Explosion.SetActive (false);
		stars.value = 0;

		slider.fillRect = (RectTransform)ParentFillArea.transform.GetChild (Helper.MAX_ATTEMPT - SessionManager.GetInstance().GetAttempt() - 1);

		StartCoroutine (ShowResult ());
	}

	private IEnumerator ShowResult()
	{			
		float elapsedTime = 0;
		float time = 40f;
		float fillTime = 5f;
		float distance = (float) SessionManager.GetInstance().GetTotalDistance()/SaveManager.Instance.GetDistance();

		float amount = (float) SessionManager.GetInstance().getLastRunningDistance() / (SaveManager.Instance.GetDistance()/Helper.MAX_ATTEMPT);
		//fill the stars
		if (amount > 0) {
		
			//sound
			mAudio.clip = Filling;
			mAudio.Play();

			int cnt = 1;
			while (elapsedTime < time && stars.value < 1) {
				stars.value = Mathf.Lerp (0, amount, (elapsedTime / time));

				elapsedTime += Time.deltaTime * cnt * 0.1f;
				yield return new WaitForEndOfFrame ();
				cnt++;
			}
			stars.value = amount;

			mAudio.clip = FairyDustSound;
			mAudio.Play();


			//explosion particle sistem
			Explosion.SetActive (true);
			yield return new WaitForSeconds (1f);
			//move camera
			StartCoroutine (SessionManager.GetInstance ().MoveCameraDownRoutine ());
			yield return new WaitForSeconds (0.6f);

			Fall.SetActive (true);
			yield return new WaitForSeconds (1f);

			//fill the sliderRace
			elapsedTime = 0;
			float startValue = slider.value; 
			while (elapsedTime < fillTime && slider.value < 1 ) 
			{
				slider.value = (float)Mathf.Lerp (startValue, distance, (elapsedTime / fillTime));

				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}
			slider.value = distance;
			//TODO find a way to go in the next screen by tapping
			if(slider.value != 1)
				yield return new WaitForSeconds (3f);			
		}
		else 
		{
			//TODO
			yield return new WaitForSeconds (3f);
		}


		EndState ();
	}

	void EndState()
	{
		SessionManager.GetInstance().EndState (state);
	}
}
