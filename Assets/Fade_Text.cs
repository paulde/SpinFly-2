using UnityEngine;
using System.Collections;

public class Fade_Text : MonoBehaviour {
	public GUIText text;
	float guiTime = 3.0f;

	void Start() {
		text.guiText.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > guiTime) {
			text.guiText.enabled = false;
		}

		Color myColor = guiText.color;
		float ratio = Time.time / guiTime;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		guiText.color = myColor;
	}
}
