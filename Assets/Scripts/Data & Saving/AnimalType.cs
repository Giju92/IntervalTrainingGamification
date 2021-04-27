using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class AnimalType
{
	static  Object [] sprites = Resources.LoadAll ("Images/Animals");
	enum Type {pig,donkey,monkey,dog,sheep,mouse,lion,panda,giraffe,cat,cow,cock,bear,tiger,ferret,chicken,horse,rabbit,zebra,goat};


	public static int GetRandomAnimalIndex()
	{	
		return Random.Range(0,Helper.ANIMAL_COUNT);
	}

	public static Sprite GetImgFromIndex(int index)
	{	
		return (Sprite)sprites [index+1];
	}

	public static string GetNameFromIndex(int index)
	{	
		Type t = (Type)index;
		return t.ToString ();
	}

	public static Sprite GetDefaultImg()
	{	
		return Resources.Load<Sprite>("Images/unknown");
	}

	public static string CheckTollerance(int animal, int fruit){
	
	
		switch (animal) {

			case (int) Type.bear:
				
				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.cat:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.chicken:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();
			case (int) Type.cock:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();
			case (int) Type.cow:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.dog:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.donkey:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.ferret:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.giraffe:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.goat:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.horse:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.lion:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();
			case (int) Type.monkey:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.mouse:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();
			case (int) Type.panda:
				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();
			case (int) Type.pig:
				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.rabbit:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();
			
			case (int) Type.sheep:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.tiger:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

			case (int) Type.zebra:

				if (fruit == (int)Fruit.Type.tomato)
					return Fruit.FoodInfo.good.ToString();
				else if (fruit == (int)Fruit.Type.lemon)
					return Fruit.FoodInfo.bad.ToString();
				else
					return Fruit.FoodInfo.neutral.ToString();

				default:
					return Fruit.FoodInfo.neutral.ToString();
			}
	
	}
}

[System.Serializable]
public class Animal 
{
	public int type = -1;
	public Color color = new Color();
	public int maxFood = 100;
	public int food = 0;
	public string nickName = "default";


	public void setAnimal()
	{
		//create an animal randomly
		type = AnimalType.GetRandomAnimalIndex ();
		color = RandomColor.GetRandomColor ();
		food = Random.Range (1, 100);
	}

	public void SetNickName(string s)
	{
		nickName = s;
	}

	public int DecreseFood(int value)
	{
		food -= value;
		return food;
	}

	public string AddFood(int fruit)
	{
		string s = AnimalType.CheckTollerance (type, fruit);


		if (s.Equals (Fruit.FoodInfo.bad.ToString())) {
			
			if (food > 0) 
			{
				food -= 15;

				if (food < 0)
					food = 0;
			}
			
				
		} 
		else if (s.Equals (Fruit.FoodInfo.good.ToString())) {
			
			if (food < 0)
				food = 0;
			
			food += 25;
			if (food > maxFood)
				food = maxFood;
			
		}
		else if (s.Equals (Fruit.FoodInfo.neutral.ToString())){

			if (food < 0)
				food = 0;
			
			food += 10;
			if (food > maxFood)
				food = maxFood;
		}

		return s;
	}


	public float getPercentage()
	{
		return (float) food / maxFood;
	}
}

public class Fruit
{
	public int type { get; set;}
	public enum Type {banana,cherry,lemon,orange,apple,ananas,pear,blackberry,watermelon,strawbery,tomato,chestnut};
	public static  Object [] sprites = Resources.LoadAll ("Images/Fruits");
	public Sprite img {get{ return (Sprite)sprites[type+1]; }}
	public enum FoodInfo {good, neutral,bad};

	public Fruit()
	{
		this.type = Random.Range(0,12);
	}

	public Fruit(int type)
	{
		this.type = type;
	}

	public static Sprite GetImageFromIndex(int i)
	{
		return (Sprite)sprites[i+1];
	}
}



static class RandomColor
{
	public static Color GetRandomColor(){

		int i;

		i = Random.Range (0, 5);		

		switch(i){
			case 0:
				return Color.blue;
			case 1:
				return Color.green;
			case 2:
				return Color.red;
			case 3:
				return Color.yellow;
			case 4:
				return Color.white;
			default:
				return Color.white;
		}
	}

	public static string GetNameColor(Color c){

		if(c.Equals(Color.blue))
			return "blue";
		if(c.Equals(Color.green))
			return "green";
		if(c.Equals(Color.red))
			return "red";
		if(c.Equals(Color.yellow))
			return "yellow";
		if(c.Equals(Color.white))
			return "white";	
		
		return "unknown";
	}
}