using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour {

	public static Balloon instance;
	Coroutine c;

	List<string> greeting = new List<string>();

	List<string> thank_B = new List<string>();
	List<string> thank_N = new List<string>();
	List<string> thank_G = new List<string>();

	List<string> starving = new List<string>();
	List<string> stable = new List<string>();
	List<string> good = new List<string>();

	public Text txt;
	public Slider slider;


	void Awake(){
		instance = this;

		greeting.Add ("hi!!");
		greeting.Add ("hello!!");
		greeting.Add ("bye bye");
		greeting.Add ("cheers");
		greeting.Add ("greetings");
		greeting.Add ("welcome");

		thank_N.Add ("thank you");
		thank_N.Add ("thankful");
		thank_N.Add ("nice meal");

		thank_G.Add ("love it");
		thank_G.Add ("the best");
		thank_G.Add ("tasteful");

		thank_B.Add ("grrrr");
		thank_B.Add ("bwach");
		thank_B.Add ("burp");

		starving.Add ("i'm hungry");
		starving.Add ("feed me");
		starving.Add ("food!!");
		starving.Add ("help me");

		stable.Add ("nice day");
		stable.Add ("well");
		stable.Add ("good");

		good.Add ("i'm great");
		good.Add ("super");
		good.Add ("powerful");
	}

	public void Greeting () {
		if(c != null)
			StopCoroutine (c);
		
		txt.text = greeting [Random.Range (0, greeting.Count)];
		c = StartCoroutine (ShowState ());
	}

	public void Thanks (int type) {
		if(c != null)
			StopCoroutine (c);

		switch (type) 
		{
			case (int) Fruit.FoodInfo.bad:
				txt.text = thank_B [Random.Range (0, thank_B.Count)];
				break;
			case (int) Fruit.FoodInfo.neutral:
				txt.text = thank_N [Random.Range (0, thank_N.Count)];
				break;
			case (int) Fruit.FoodInfo.good:
				txt.text = thank_G [Random.Range (0, thank_G.Count)];
				break;
		}

		c = StartCoroutine (ShowState ());
	}

	IEnumerator ShowState()
	{
		yield return new WaitForSeconds (2);

		if (slider.value < (float) 5 / 10) {
			txt.text = starving [Random.Range (0, starving.Count)];
		} else if (slider.value > (float) 9 / 10) {
			txt.text = good [Random.Range (0, good.Count)];
		} else {
			txt.text = stable [Random.Range (0, stable.Count)];
		}
	}
}
