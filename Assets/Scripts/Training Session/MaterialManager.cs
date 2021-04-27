using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour {

	string path = "Images/";
	public Material EggMaterial;
	static public MaterialManager instance;

	void Start()
	{
		instance = this;
		EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg1");
	}

	public void UpdateImage()
	{
		float percentage = SessionManager.GetInstance ().GetPercentage ();
		if (percentage < 0.1) {
			EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg1");
		} else if (percentage >= 0.1 && percentage < 0.2) {
			EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg2");
		} else if (percentage >= 0.2 && percentage < 0.3) {
			EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg3");
		} else if (percentage >= 0.3 && percentage < 0.4) {
			EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg4");
		} else if (percentage >= 0.4 && percentage < 0.5) {
			EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg5");
		} else if (percentage >= 0.5 && percentage < 0.6) {
			EggMaterial.mainTexture = Resources.Load<Texture> (path + "Egg6");
		} else if (percentage >= 0.6) {
			EggMaterial.mainTexture = Resources.Load<Texture>  (path + "Egg7");
} 
		}

}
