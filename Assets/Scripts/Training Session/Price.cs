using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Price : MonoBehaviour {

	int state = (int)SessionManager.State.price;

	public Image animal;



	public void StartState () {
		this.gameObject.GetComponent<Animator> ().Play ("SpinEggSimple");
	}

	public void ChangeSprite()
	{
		MaterialManager.instance.UpdateImage ();
	}

	public void CheckState()
	{
		StartCoroutine(check());
	}


	IEnumerator check()
	{
		yield return new WaitForSeconds (1);
		if (SessionManager.GetInstance ().IsFinished ()) {
			this.gameObject.GetComponent<Animator> ().Play ("OpenEgg");
		} else {
			EndState ();
		}
	}
	

	public void EndState()
	{
		SessionManager.GetInstance().EndState (state);
	}
}
