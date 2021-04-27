using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BouncingObject : MonoBehaviour {

	Fruit fruit; 
	public GameObject particle;
	Coroutine c;

	void Start()
	{
		fruit = new Fruit ();
		this.transform.GetChild(1).GetComponent<Image>().sprite = fruit.img;
		Vector2 externalForce = new Vector2 (Random.Range (-100, 100), 0);
		//this.GetComponent<ConstantForce2D> ().force = externalForce;
		GetComponent<ConstantForce2D> ().force = externalForce;

		c = StartCoroutine(ExpirationRoutine(Random.Range(6,10)));
	}

	IEnumerator ExpirationRoutine(int time )
	{
		float elapsedTime = 0;
		float currentTime = (float)time/3;

		for (int i = 0; i < 20; i++) 
		{
			while (elapsedTime < currentTime / 2) 
			{
				float value = Mathf.Lerp(1, 1.2f, (elapsedTime / (float)(currentTime/2)));	
				this.transform.localScale = new Vector2(value, value);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			elapsedTime = 0;
			while (elapsedTime < currentTime / 2) 
			{
				float value = Mathf.Lerp(1.2f, 1, (elapsedTime / (float)(currentTime/2)));	
				this.transform.localScale = new Vector2(value, value);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			currentTime = currentTime / 2;
			elapsedTime = 0;
		}

		while (elapsedTime < currentTime / 2) 
		{
			float value = Mathf.Lerp(1, 1.2f, (elapsedTime / (float)(currentTime/2)));	
			this.transform.localScale = new Vector2(value, value);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}


		/*while (elapsedTime < time)
		{		
			float value = Mathf.Lerp(1, 2, (elapsedTime / time));	
			this.transform.localScale = new Vector2(value, value);

			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}*/

		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Border") 
		{
			if (this.gameObject.GetComponent<ConstantForce2D> ().force.x > 0) {
				this.gameObject.GetComponent<ConstantForce2D> ().force = new Vector2 (this.gameObject.GetComponent<ConstantForce2D> ().force.x * -1 + 1, this.gameObject.GetComponent<ConstantForce2D> ().force.y); 
			} else {
				this.gameObject.GetComponent<ConstantForce2D> ().force = new Vector2(this.gameObject.GetComponent<ConstantForce2D> ().force.x*-1-1,this.gameObject.GetComponent<ConstantForce2D> ().force.y); 
			}

			this.gameObject.GetComponent<ConstantForce2D> ().torque = Random.Range (500, 1500);
		}

	}

	void OnMouseDown()
	{
		StopCoroutine (c);
		Vector2 center = this.transform.localPosition;
		GameObject p = Instantiate (particle);
		p.transform.position = this.transform.position;
		p.transform.parent = FindObjectOfType<Canvas> ().transform;

		Vibration.Vibrate(100);
		Spawner.instance.CatchObject (fruit.type);
		Destroy (gameObject);
	}
}
