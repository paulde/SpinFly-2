using UnityEngine;
using System.Collections;

public class Save_script : MonoBehaviour {

	public int level;

	private static Save_script instanceRef;

	// Use this for initialization
	void Start () {

		print("reset");
		if(instanceRef == null)
        {
			instanceRef = this;
			DontDestroyOnLoad(gameObject);
			level = 1;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	
	}
	
}
