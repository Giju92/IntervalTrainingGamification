using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour {

	Slider slider;
	public Image img;

	void Start(){
	
		slider = transform.GetComponent<Slider>();	
	}

	// Update is called once per frame
	void Update () {

		img.color = Color.Lerp( Color.red, Color.green, (slider.value / 1));
	}
}
