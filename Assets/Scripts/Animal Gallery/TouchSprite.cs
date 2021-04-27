using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSprite : MonoBehaviour {
	
	public static bool guiTouch = false;
	public static bool dragFruit = false;

	public void TouchInput(Collider2D collider)
	{
		if (!dragFruit) {
			if (Input.touchCount > 0) {
			
				if (collider == Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position))) {
			
					switch (Input.GetTouch (0).phase) {
					case TouchPhase.Began:
						if (dragFruit)
							SendMessage ("OnFirstTouchBegan", SendMessageOptions.DontRequireReceiver);					
						break;

					case TouchPhase.Stationary:
					
						SendMessage ("OnTouchStayed", SendMessageOptions.DontRequireReceiver);

						guiTouch = true;
						break;

					case TouchPhase.Moved:
						SendMessage ("OnTouchMoved", SendMessageOptions.DontRequireReceiver);

						break;
					case TouchPhase.Ended:
						SendMessage ("OnTouchEnded", SendMessageOptions.DontRequireReceiver);
						guiTouch = true;
						break;
					}			
				}
			}
		}
	}
}
