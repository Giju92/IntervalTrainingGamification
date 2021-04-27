using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFruit : MonoBehaviour {

	// Use this for initialization
	public int Inizialize (int? type)
	{
		transform.localPosition = new Vector2 (Random.Range (-400, 400), 1000);
		GetComponent<ConstantForce2D> ().torque = Random.Range (0, 2000);
		StartCoroutine (LifeTime ());

		Fruit f;
		if (type == null)
			f = new Fruit ();
		else
			f = new Fruit ((int)type);
		
		GetComponent<SpriteRenderer> ().sprite = f.img;
		SaveManager.Instance.AddFruit (f.type);
		Vibration.Vibrate (100);
		return f.type;
	}

	IEnumerator LifeTime()
	{
		yield return new WaitForSeconds (Random.Range(4,8));
		Destroy (this.gameObject);
	}
}
