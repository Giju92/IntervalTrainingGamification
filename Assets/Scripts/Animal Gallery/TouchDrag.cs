using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventoryElem))]
public class TouchDrag : TouchSprite {
	
	int cnt = 0;

	// Update is called once per frame
	void FixedUpdate () {
		if (!TouchSprite.dragFruit) {
			TouchInput (GetComponent<Collider2D> ());
		}
	}


	void OnTouchStayed(){
		cnt++;
		if (cnt == 10) {	

			if (GetComponent<InventoryElem> ().CheckDrag ()) {	
				Vibration.Vibrate (50);
				FruitsList.instance.Disable ();
				cnt = 0;
			}
							
		}
	}


	void OnTouchEnded(){

		FruitsList.instance.Enable ();
		cnt = 0;
	}

}
