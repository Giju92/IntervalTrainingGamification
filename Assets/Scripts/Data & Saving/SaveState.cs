using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveState : System.Object
{
	//default date
	public string LastDate = "";

	public int level = 0;
	public int animal = 0;
	public int tickets = 0;
	public int distance		= Helper.MAX_DISTANCE;
	public int runningTime 	= Helper.DURATION_TIME;
	public int walkingTime 	= Helper.DURATION_TIME_WALKING;

	public List<Animal> AnimalOwnedList = new List<Animal>();
	public List<Mission> MissionList = new List<Mission>();
	public int[] FruitList =  new int[12];
}




