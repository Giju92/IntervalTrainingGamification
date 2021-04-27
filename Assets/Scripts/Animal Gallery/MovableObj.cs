using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovableObj : TouchSprite {

	public static MovableObj instance;
	int type;
	bool enter = false;
	public Vector3 SpawnPos = new Vector3(2000,0,0);

	void Start(){
		instance = this;
	}

	void FixedUpdate () {
		
		TouchInput (GetComponent<Collider2D> ());

	}

	public void SetObj(Vector3 pos, int type )
	{
		SpawnPos = pos;
		this.type = type;
		GetComponent<Image> ().sprite = Fruit.GetImageFromIndex (type);
		transform.position = pos;
		TouchSprite.dragFruit = true;
		StartCoroutine (MoveCoroutine ());
	}

	IEnumerator MoveCoroutine()
	{		
		bool finished = false;
		while(!finished){
			if (Input.touchCount > 0) {
				
				switch (Input.GetTouch (0).phase) 
				{
					case TouchPhase.Ended:
						
						FruitsList.instance.Enable ();
						TouchSprite.dragFruit = false;
						finished = true;
						//work here
						if (enter) {
							
							//explosion?
						} else {
							float time = 0.3f;
							float elapsedTime = 0;
							Vector3 startPosition = transform.position;

							while (elapsedTime < time) {
								transform.position = Vector3.Lerp (startPosition, SpawnPos, elapsedTime / time);
								transform.localScale = Vector3.Lerp (new Vector3 (1, 1, 1), new Vector3 (3 / 5,3 / 5, 3 / 5), elapsedTime / time);

								elapsedTime += Time.deltaTime;
								yield return new WaitForEndOfFrame ();
							}
						}


						transform.position = new Vector3 (2000, 0, 0);
						transform.localScale = new Vector3 (1, 1, 1);
						FruitsList.instance.BroadCastEventToChild (type, enter);

						break;

					default:
						transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position).x, Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position).y, 0);
						yield return new WaitForEndOfFrame ();
							break;
				}	
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.CompareTag("Animal")) 
		{
			//Debug.Log ("enter");
			enter = true;
		}	
	}

	void OnTriggerExit2D(Collider2D other){

		if (other.CompareTag("Animal")) 
		{
			//Debug.Log ("exit");
			enter = false;
		}	
	}



}