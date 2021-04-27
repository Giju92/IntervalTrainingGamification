using System;	
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnimalImage: MonoBehaviour {


	public void UpdateImage()
	{
		Image img = this.gameObject.GetComponent<Image>();
		img.sprite = AnimalType.GetImgFromIndex (SessionManager.GetInstance ().animal.type);
		img.color = SessionManager.GetInstance ().animal.color;
	}


}
