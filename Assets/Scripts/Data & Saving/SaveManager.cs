using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour {

	public static bool created = false;
	public static SaveManager Instance { set; get; }
	public SaveState state;
	public static int LastAnimalUnlocked = -1;

	private void Awake()
	{
		if (!created) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
			Load ();
			created = true;

			//ResetSave ();
			//add animals
			/*if (state.AnimalOwnedList.Count == 0) {
				for (int i = 0; i < 30; i++) {
					Animal a = new Animal ();
					a.setAnimal ();
					a.SetNickName ("animal " + i);
					AddAnimal (a);
					UnlockAnimal (a.type);
				}
			}

			//add mission
			if (state.MissionList.Count == 0) {

				Mission m = new Mission ();

				List<Requirement> l = new List<Requirement> ();
				Requirement r = new Requirement ();
				r.setRequirement (12, Color.yellow);
				l.Add (r);

				m.setMission ("ROMA", "Images/zoo2", l);
				state.MissionList.Add (m);

				m = new Mission ();

				l = new List<Requirement> ();
				r = new Requirement ();
				r.setRequirement (null, Color.red);
				l.Add (r);
				l.Add (r);
				l.Add (r);

				m.setMission ("PARIS", "Images/zoo1", l);
				state.MissionList.Add (m);


				m = new Mission ();

				l = new List<Requirement> ();
				r = new Requirement ();
				r.setRequirement (10, Color.green);
				l.Add (r);
				l.Add (r);
				l.Add (r);


				m.setMission ("MADRID", "Images/zoo3", l);
				state.MissionList.Add (m);

				m = new Mission ();

				l = new List<Requirement> ();
				r = new Requirement ();
				r.setRequirement (null, null);
				l.Add (r);
				l.Add (r);
				l.Add (r);
				l.Add (r);
				l.Add (r);
				l.Add (r);


				m.setMission ("LONDON", "Images/zoo1", l);
				state.MissionList.Add (m);

				m = new Mission ();

				l = new List<Requirement> ();
				r = new Requirement ();
				r.setRequirement (3, null);
				l.Add (r);
				l.Add (r);
				l.Add (r);
				l.Add (r);
				l.Add (r);
				l.Add (r);


				m.setMission ("RIGA", "Images/zoo2", l);
				state.MissionList.Add (m);

			}



			if (state.FruitList [0] == 0) {
				int[] v = new int[] { 20, 42, 17, 30, 13, 15, 18, 13, 2, 3, 5, 8, 13 };
				SaveManager.Instance.AddFruits (v);
			}

			Save ();*/
		}
		else
		{
			Destroy (this.gameObject);
		}		

	}

	private void Save () 
	{
		PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
	}	

	public void Load()
	{
		if(PlayerPrefs.HasKey("save"))
		{			
			state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
		}
		else
		{
			state = new SaveState ();
			Save ();
		}
	}

	public void AddAnimal(Animal a)
	{
		state.AnimalOwnedList.Add (a);
		Save();
	}

	public bool IsAnimalOwned(int index)
	{
		return (state.animal & (1 << index )) != 0;
	}

	public void RemoveAnimal(List<int> l)
	{
		List<Animal> selectedAnimal = new List<Animal> ();

		foreach (int i in l) 
		{
			selectedAnimal.Add (state.AnimalOwnedList[i]);
		}

		foreach (Animal a in selectedAnimal) 
		{
			state.AnimalOwnedList.Remove (a);
		}

		Save ();
	}

	public void RemoveMission(int index)
	{
		state.MissionList.RemoveAt(index);
		Save ();
	}

	public void UnlockAnimal(int index)
	{
		LastAnimalUnlocked = index;
		state.animal |= 1 << index;
		Save ();
	}

	public List<Animal> GetOwnedAnimalList()
	{
		return state.AnimalOwnedList;
	}

	public string giveFoodToAnimal(int animal, int fruit)
	{
		string s = state.AnimalOwnedList [animal].AddFood(fruit);
		Save ();
		return s;
	}

	public List<Mission> GetMissionList()
	{
		return state.MissionList;
	}


	public int GetListDimension()
	{
		return state.AnimalOwnedList.Count;
	}

	public int GetFruitFromIndex (int i)
	{
		return state.FruitList [i];
	}

	public string GetFruitsList()
	{
		string s = "FRUITS:\n";
		for (int i = 0; i < Helper.FRUIT_COUNT; i++) {

			Fruit.Type t = (Fruit.Type)i;
			s += t.ToString () + " " + state.FruitList [i] + "\n" ;
		}

		return s;
	}

	public void AddFruits(int[] vector)
	{
		for (int i = 0; i < Helper.FRUIT_COUNT; i++) {
		
			state.FruitList [i] += vector[i] ;
		}

		Save ();
	}

	public void SubFruit(int index)
	{
		state.FruitList [index] = state.FruitList [index] - 1 ;

		Save ();
	}

	public void AddFruit(int index)
	{
		state.FruitList [index] = state.FruitList [index] + 1 ;

		Save ();
	}

	public Animal GetListFromIndex(int index)
	{

		return state.AnimalOwnedList[index];
	}

	public Mission GetMissionFromIndex(int index)
	{

		return state.MissionList[index];
	}

	public int GetDistance()
	{
		return state.distance;
	}

	public void SetDistance(int dist)
	{
		state.distance = dist;
		Save ();
	}

	public string GetLastDate()
	{		
		return state.LastDate;
	}

	public void SetLastDate(string date)
	{
		state.LastDate = date;
		Save ();
	}

	public void UpdateAnimals(int value)
	{
		List<Message> l = new List<Message>();

		for(int i = state.AnimalOwnedList.Count - 1; i >= 0; i--)
		{
			int foodValue = state.AnimalOwnedList[i].DecreseFood (value);

			if ( foodValue < -20)			
			{
				Message m = new Message(state.AnimalOwnedList[i],(int) Message.MsgType.left);
				l.Add (m);
				state.AnimalOwnedList.RemoveAt(i);
			}
			else if (foodValue > 90)			
			{
				Message m = new Message(state.AnimalOwnedList[i],(int) Message.MsgType.happy);
				l.Add (m);
			}
			else if (foodValue < 10 && foodValue > -10)			
			{
				Message m = new Message(state.AnimalOwnedList[i],(int) Message.MsgType.angry);
				l.Add (m);
			}
		}

		MessageMenager.instance.SetList(l);
		Save ();
	}


	public void SetRunningTime(int time)
	{
		state.runningTime = time;
		Save ();
	}

	public int GetRunningTime()
	{
		return state.runningTime;
	}


	public int GetWalkingTime()
	{
		return state.walkingTime;
	}

	public void SetWalkingTime(int time)
	{
		state.walkingTime = time;
		Save ();
	}

	public int GetTicketsCount()
	{
		return state.tickets;
	}

	public void SumTicket(int value)
	{
		state.tickets += value;
		Save ();
	}

	public void IncreaseLevel ()
	{
		state.level++;
		Save ();
	}

	public int GetLevel ()
	{
		return state.level;
	}


	public void ResetSave()
	{
		PlayerPrefs.DeleteKey ("save");
	}
}
