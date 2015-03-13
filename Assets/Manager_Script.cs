using UnityEngine;
using System.Collections;

public class Manager_Script : MonoBehaviour {

	GameObject start;
	GameObject option;
	GameObject control1;
	GameObject control2;
	GameObject control3;

	// Use this for initialization
	void Start () {
		start = GameObject.Find ("Start");
		start.SetActive (true);
		option = GameObject.Find ("Option");
		option.SetActive (true);

		control1 = GameObject.Find ("Control1");
		control1.SetActive (false);
		control2 = GameObject.Find ("Control2");
		control2.SetActive (false);
		control3 = GameObject.Find ("Control3");
		control3.SetActive (false);
	}

	public void startGame() {
		Application.LoadLevel ("level1");
		
	}
	public void options() {
		start.SetActive (false);
		option.SetActive (false);
		control1.SetActive (true);
		control2.SetActive (true);
		control3.SetActive (true);
		
	}
	public void Control1() {
		PlayerPrefs.SetInt ("Control", 1);


		start.SetActive (true);
		option.SetActive (true);
		control1.SetActive (false);
		control2.SetActive (false);
		control3.SetActive (false);

	}
	public void Control2() {
		PlayerPrefs.SetInt ("Control", 2);
		
		
		start.SetActive (true);
		option.SetActive (true);
		control1.SetActive (false);
		control2.SetActive (false);
		control3.SetActive (false);
		
	}
	public void Control3() {
		PlayerPrefs.SetInt ("Control", 3);
		
		
		start.SetActive (true);
		option.SetActive (true);
		control1.SetActive (false);
		control2.SetActive (false);
		control3.SetActive (false);
		
	}
}
