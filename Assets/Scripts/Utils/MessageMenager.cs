using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageMenager : MonoBehaviour {

	public Transform MessageItem;
	public Transform MessageGrid;

	public static List<Message> MessagesList = new List<Message> ();
	public static MessageMenager instance;

	void Awake()
	{
		instance = this;
	}

	void Start(){
		
		ManagerUpdate.instance.ForceStart ();
	}

	void RefreshMessageCollection()
	{
		int i;
		if (MessageGrid != null) 
		{
			for (i = MessageGrid.childCount - 1; i >= 0; i--) {
				GameObject.Destroy (MessageGrid.GetChild (i).gameObject);
			}
		}
		i = 0;
		foreach (Message m in MessagesList) 
		{
			int cnt = i;
			Transform obj = Instantiate (MessageItem);
			obj.SetParent (MessageGrid);
			obj.GetComponent<Image> ().color = m.GetColor ();
			//obj.GetComponent<Button>().onClick.AddListener (() => OnMissionSelect (cnt,m.GetRequirementsValue()));

			obj.GetChild (0).GetComponent<Image> ().sprite = AnimalType.GetImgFromIndex(m.a.type);
			obj.GetChild (0).GetComponent<Image> ().color = m.a.color;
			obj.GetChild (1).GetComponent<Text> ().text = m.a.nickName;
			obj.GetChild (2).GetComponent<Text> ().text = m.GetMessageText ();

			i++;
		}
	}

	public void SetList(List<Message> l)
	{
		MessagesList.Clear ();
		MessagesList = l;
		RefreshMessageCollection ();
	}
}

public class Message
{
	public enum MsgType {left,happy,angry};
	public Animal a;
	MsgType type;

	public Message(Animal a, int type)
	{
		this.a = a;
		this.type = (MsgType) type;
	}

	public Color32 GetColor()
	{
		switch(type){
			case MsgType.angry:
				return new Color32 (255, 255, 0, 100);
			case MsgType.happy:
				return new Color32 (0, 255, 0, 100);				
			case MsgType.left:
				return new Color32 (255, 0, 0, 100);
			default:
				return new Color32 (0, 0, 0, 100);
		}
	}

	public string GetMessageText ()
	{
		switch(type){
			case MsgType.angry:
				return "can you feed me please?";
			case MsgType.happy:
				return "thank you for the food";
			case MsgType.left:
				return "i am gone";
			default:
				return "errore";
		}
	}

}
