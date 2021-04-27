using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryElem : MonoBehaviour {

	int fruitType;
	int FruitValue;
	Text txt;
	Image img;
	Color full = new Color (1, 1, 1, 1);
	Color empty = new Color (1, 1, 1, 0.3f);


	public void SetValue(int type)
	{
		fruitType = type;

		img = transform.GetChild (0).GetComponent<Image> ();
		img.sprite = Fruit.GetImageFromIndex (type);

		FruitValue = SaveManager.Instance.GetFruitFromIndex (type);
		CheckColor ();


		txt = transform.GetChild (1).GetComponent<Text> ();
		txt.text = "" + FruitValue;
	}

	public bool CheckDrag(){
		
		if (FruitValue > 0) {
			MovableObj.instance.SetObj (transform.position, fruitType);
			FruitValue--;
			txt.text = "" + (FruitValue);
			CheckColor();
			return true;
		}	
		return false;
	}

	void CheckColor(){
		
		if (FruitValue == 0)
			img.color = empty;
		else
			img.color = full;
	}


	public void EndDrop (bool given) {

		if (given) 
		{
			SaveManager.Instance.SubFruit (fruitType);

			string s = SaveManager.Instance.giveFoodToAnimal (GalleryManager.animalIndex, fruitType);

			if (s.Equals (Fruit.FoodInfo.bad.ToString())) {
				GalleryManager.feedAnimation (Color.red);
				Balloon.instance.Thanks ((int) Fruit.FoodInfo.bad);
			} else if (s.Equals (Fruit.FoodInfo.good.ToString())) {
				GalleryManager.feedAnimation (Color.green);
				Balloon.instance.Thanks ((int) Fruit.FoodInfo.good);
			} else if (s.Equals (Fruit.FoodInfo.neutral.ToString())){
				GalleryManager.feedAnimation (Color.white);
				Balloon.instance.Thanks ((int) Fruit.FoodInfo.neutral);
			}

		} 
		else
		{
			FruitValue++;
			txt.text = "" + FruitValue;
			CheckColor();
		}
		
	}
}
