using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour {

	void Start()
	{		
		this.GetComponent<ConstantForce2D> ().force = new Vector2 (Random.Range (-100, 100), 0);
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

}
