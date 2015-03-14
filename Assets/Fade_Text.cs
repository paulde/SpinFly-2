using UnityEngine;
using System.Collections;

public class Fade_Text : MonoBehaviour {
	public GUIText text;
	float guiTime = 3.0f;
	public bool show;

	private GameObject saver;
	private Save_script saveScript;

	public Color myColor;
	public float ratio;

	void Start() {
		saver = GameObject.Find("Save Sphere");
		saveScript = saver.GetComponent <Save_script> ();

		myColor = guiText.color;
	}

	// Update is called once per frame
	void Update () {
		//show = saveScript.levelText;
		ratio = Time.time / guiTime;
		/*
		if (show) {
			//ratio = Time.time / guiTime;
			myColor.a = Mathf.Lerp (0, 1, ratio);
			guiText.color = myColor;
		} else {*/
			myColor.a = Mathf.Lerp (1, 0, ratio);
			guiText.color = myColor;
		//}
	}
}
