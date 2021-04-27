using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mission : ISerializationCallbackReceiver
{
	public string city;

	public List<Requirement> requirements = new List<Requirement> ();
	public string imgPath;
	public bool done = false;

	public List<int?> types = new List<int?>();
	public List<Color?> colors = new List<Color?>();

	public void OnBeforeSerialize()
	{
		types.Clear();
		colors.Clear();

		foreach (Requirement r in requirements)
		{
			types.Add(r.Type);
			colors.Add(r.Color);
		}
	}

	public void OnAfterDeserialize()
	{
		requirements = new List<Requirement> ();

		for (int i = 0; i < types.Count; i++) 
		{
			Requirement r = new Requirement ();
			r.setRequirement (types [i], colors [i]);
			requirements.Add (r);
		}
			
	}




	public void setMission(string str,string path,List<Requirement> l)
	{		
		city = str;
		imgPath = path;
		requirements = l;
	}

	public void EndMission()
	{
		done = true;
	}

	public int GetRequirementsValue()
	{
		return requirements.Count;
	}


	public string GetDescription()
	{
		
		Requirement r = requirements [0];

		if (r.Type == null && r.Color == null) 
		{
			return "" + requirements.Count + " animal/s";

		} 
		else if (r.Type == null && r.Color != null) 
		{		
			return "" + requirements.Count + " " + RandomColor.GetNameColor((Color)r.Color) +  " animal/s";
		}
		else if (r.Type != null && r.Color == null) 
		{
			return "" + requirements.Count + " " + AnimalType.GetNameFromIndex((int)r.Type);
		}
		else if (r.Type != null && r.Color != null) 
		{

			return "" + requirements.Count + " " + RandomColor.GetNameColor((Color)r.Color) + " " + AnimalType.GetNameFromIndex((int)r.Type);
		}

		return "error";
	}


	public bool CheckAnimal(Animal a){

		foreach (Requirement r in requirements) 
		{
			if (r.Check (a))
				return true;
		}
	
		return false;
	}

}

[System.Serializable]
public class Requirement 
{
	public int? Type;
	public Color? Color;

	public void setRequirement(int? t, Color? c)
	{
		Type = t;
		Color = c;
	}


	public bool Check(Animal a)
	{

		if (Type == null && Color == null) 
		{
			return true;
		} 
		else if (Type == null && Color != null) 
		{
			if (a.color.Equals(Color))
				return true;
		}
		else if (Type != null && Color == null) 
		{
			if (a.type == Type)
				return true;
		}
		else if (Type != null && Color != null) {

			if (a.type == Type && a.color.Equals(Color))
				return true;
		}

		return false;
	}
}

