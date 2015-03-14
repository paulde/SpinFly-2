using UnityEngine;
using System.Collections;

public class Save_script : MonoBehaviour {

	public int level;
	public float timeLeft;
	public float highScore;
	public bool levelText;

	private static Save_script instanceRef;

	// Use this for initialization
	void Start () {

		print("reset");
		if(instanceRef == null)
        {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
			level = 1;
			timeLeft = 60f;
			highScore = 0;
			levelText = true;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	
	}
	
}
