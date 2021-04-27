using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour {

	public Image animalImage;
	public Transform AnimalButton;
	public Transform animalList;
	private int currentAnimalIndex = -1;


	public static Collection Instance { set; get; }

	private void Awake()
	{		
		Instance = this;
	}
	
	public void InitCollection()
	{
		animalList.DetachChildren ();	
		for(int i = 0; i < Helper.ANIMAL_COUNT; i++)
		{

			int cnt = i;
			Transform b = Instantiate (AnimalButton);
			b.SetParent (animalList);
			b.GetComponent<Button>().onClick.AddListener (() => OnAnimalSelect (cnt));


			if(SaveManager.Instance.IsAnimalOwned(i))
				b.GetChild (0).GetComponent<Text> ().text = AnimalType.GetNameFromIndex(i);
			else
				b.GetChild (0).GetComponent<Text> ().text = "??";

		}
	}

	private void OnAnimalSelect(int currentIndex)
	{
		if (currentAnimalIndex == currentIndex)
			return;		

		currentAnimalIndex = currentIndex;

		if (SaveManager.Instance.IsAnimalOwned (currentIndex)) 
		{
			animalImage.sprite = (Sprite) AnimalType.GetImgFromIndex(currentIndex);
		}
		else
		{
			animalImage.sprite = AnimalType.GetDefaultImg();
		}
	}

}
