using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitsList : MonoBehaviour {

	public static FruitsList instance;
	public GameObject prefab;
	Transform grid;


	// Use this for initialization
	void Start () {
		instance = this;

		grid = gameObject.transform.GetChild (0);

		for (int i = 0; i < Helper.FRUIT_COUNT; i++) 
		{
			GameObject elem = Instantiate (prefab);
			elem.GetComponent<InventoryElem> ().SetValue (i);

			elem.transform.SetParent (grid);
		}	

	}

	public void Enable()
	{
		GetComponent<ScrollRect> ().horizontal = true;
	}

	public void Disable()
	{
		GetComponent<ScrollRect> ().horizontal = false;
	}

	public void BroadCastEventToChild(int i, bool result)
	{
		Transform elem = grid.GetChild (i);
		elem.GetComponent<InventoryElem> ().EndDrop (result);
	}
}
