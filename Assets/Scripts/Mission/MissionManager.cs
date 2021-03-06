using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

	//public Image animalImage;
	public Transform AnimalItem;
	public Transform AnimalList;

	public Transform MissionItem;
	public Transform MissionList;

	public Button sendButton;

	List<int> SelectedAnimal = new List<int> ();

	private int currentMission = -1;
	private int requiredAnimal = -1;

	void Start(){
	
		InitAnimalCollection ();
		InitMissionCollection ();
	}

	void Update(){
	
		if (Input.GetKeyDown (KeyCode.Escape))
			SceneManager.LoadScene ("Main");			
	
	}

	public void OnBackButtonCLick()
	{
		SceneManager.LoadScene ("Main");
	}


	public void InitMissionCollection()
	{
		int i = 0;
		foreach (Mission m in SaveManager.Instance.GetMissionList()) 
		{
			int cnt = i;
			Transform obj = Instantiate (MissionItem);
			obj.SetParent (MissionList);
			obj.GetComponent<Button>().onClick.AddListener (() => OnMissionSelect (cnt,m.GetRequirementsValue()));

			obj.GetChild (0).GetComponent<Image> ().sprite = Resources.Load<Sprite>(m.imgPath);
			obj.GetChild (1).GetComponent<Text> ().text = m.city;
			obj.GetChild (2).GetComponent<Text> ().text = m.GetDescription ();
			obj.GetChild (3).GetComponent<Text> ().text = "0/" + m.GetRequirementsValue ();

			i++;
		}
	}

	public void InitAnimalCollection()
	{
		int i = 0;
		foreach (Animal a in SaveManager.Instance.GetOwnedAnimalList()) 
		{
			int cnt = i;
			Transform obj = Instantiate (AnimalItem);
			obj.SetParent (AnimalList);
			obj.GetComponent<Button>().onClick.AddListener (() => OnAnimalSelect (cnt));
			obj.GetComponent<Button>().interactable = true;

			obj.GetChild (0).GetComponent<Image> ().sprite = AnimalType.GetImgFromIndex(a.type);
			obj.GetChild (0).GetComponent<Image> ().color = a.color;

			obj.GetChild (1).GetComponent<Text> ().text = a.nickName;

			i++;
		}
	}


	private void OnAnimalSelect(int currentIndex)
	{		
		int index = SelectedAnimal.IndexOf (currentIndex);

		if (index >= 0) {
			//already in the list
			SelectedAnimal.Remove (currentIndex);
			//light blue
			AnimalList.GetChild (currentIndex).GetComponent<Image> ().color = new Color32 (0, 200, 255, 100);
		} 
		else if (index < 0 && SelectedAnimal.Count < requiredAnimal)
		{
			//add in the list
			SelectedAnimal.Add (currentIndex);
			//yellow
			AnimalList.GetChild (currentIndex).GetComponent<Image> ().color = new Color32 (255, 255, 0, 100);
		}

		CheckState ();
	}


	private void OnMissionSelect(int currentIndex, int animalsRequired)
	{
		if (currentMission == currentIndex)
		{
			return;
		}
		SelectedAnimal.Clear ();
		//clear the other mission button
		if (currentMission != -1) {
			MissionList.GetChild (currentMission).GetComponent<Image> ().color = new Color32 (0, 0, 0, 0);
			MissionList.GetChild (currentMission).GetChild (3).GetComponent<Text> ().text = "0/" + requiredAnimal;
		} 
				
		currentMission = currentIndex;
		requiredAnimal = animalsRequired;
		//color the button
		MissionList.GetChild (currentMission).GetComponent<Image> ().color = new Color32 (80, 20, 255, 255);

		Mission m = SaveManager.Instance.GetMissionFromIndex (currentIndex);

		List<Animal> list = SaveManager.Instance.GetOwnedAnimalList ();

		int i = 0;
		foreach (Animal a in list) {
		
			//clear the animalList & disable

			AnimalList.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 200, 255, 100);
			if (m.CheckAnimal (a)) {
				AnimalList.GetChild (i).GetComponent<Button> ().interactable = true;
				AnimalList.GetChild (i).GetComponent<Image> ().color = new Color32 (0, 200, 255, 100);

				Color32 c = AnimalList.GetChild (i).GetChild (0).GetComponent<Image> ().color;
				c.a = 255;
				AnimalList.GetChild (i).GetChild (0).GetComponent<Image> ().color = c;

				AnimalList.GetChild (i).GetChild (1).GetComponent<Text> ().color = new Color32 (0, 0, 0, 255);

			}
			else
			{
				AnimalList.GetChild (i).GetComponent<Button> ().interactable = false;

				Color32 c = AnimalList.GetChild (i).GetChild (0).GetComponent<Image> ().color;
				c.a = 100;
				AnimalList.GetChild (i).GetChild (0).GetComponent<Image> ().color = c;

				AnimalList.GetChild (i).GetChild (1).GetComponent<Text> ().color = new Color32 (0, 0, 0, 100);
			}

			i++;
		}
		CheckState ();

	}

	bool CheckState()
	{

		if (requiredAnimal == SelectedAnimal.Count) 
		{
			sendButton.interactable = true;
			MissionList.GetChild (currentMission).GetChild (3).GetComponent<Text> ().text = SelectedAnimal.Count + "/" + requiredAnimal;

			return false;
		}
			
		else
		{
			sendButton.interactable = false;
			MissionList.GetChild (currentMission).GetChild (3).GetComponent<Text> ().text = SelectedAnimal.Count + "/" + requiredAnimal;
			return true;
		}


	}

	public void SendAnimal()
	{
		SaveManager.Instance.RemoveAnimal (SelectedAnimal);
		SaveManager.Instance.RemoveMission (currentMission);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		SaveManager.Instance.IncreaseLevel ();
	}

}
