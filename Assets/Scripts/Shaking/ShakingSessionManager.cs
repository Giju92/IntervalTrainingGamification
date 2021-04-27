using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShakingSessionManager : MonoBehaviour {

	public enum State {menu,countdown,shake,fruit};
	public static ShakingSessionManager instance;
	public static int shakingTime = 0;

	public RectTransform Container;
	public RectTransform Menu;
	public RectTransform CountDown;
	public RectTransform Shake;
	public RectTransform Fruit;


	public State currentState;


	void Start()
	{
		//start recap scene
		instance = this;
		Menu.GetComponent<Menu>().StartState();
		currentState = State.menu;
	}

	public void ShackingButtonClick(int time)
	{
		shakingTime = time;
		EndState ((int)State.menu);
	}

	public void OnBackButtonClick(int time)
	{
		SceneManager.LoadScene ("Main");
	}


	public void EndState(int state)
	{
		switch (state) {
			case (int)State.menu:
				Container.anchoredPosition3D = Vector3.left * 1080 * 2;
				currentState = State.countdown;
				CountDown.GetComponent<CountDown> ().StartState ();				
				break;
			case (int)State.countdown:
				Container.anchoredPosition3D = Vector3.left * 1080 * 4;
				currentState = State.shake;
				Shake.GetComponent<Shake> ().StartState ();
				break;
			case (int)State.shake:
				Container.anchoredPosition3D = Vector3.left * 1080 * 6;
				currentState = State.fruit;
				Fruit.GetComponent<ShakingFruitResult> ().StartState ();
				break;
			case (int)State.fruit:
				Container.anchoredPosition3D = Vector3.zero;
				currentState = State.menu;	
				Menu.GetComponent<Menu> ().StartState ();
				break;
		}		

	}
}
