using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private float x_pos;
	private float z_pos;

	private Save_script saveScript;
	private GameObject saver;
	private int level;
	GameObject playerObj;

	// Use this for initialization
	void Start () 
	{

		saver = GameObject.Find("Save Sphere");
		saveScript = saver.GetComponent <Save_script> ();
		level = saveScript.level;


		if (level == 1)
		{
			camera.backgroundColor = new Color (0.5f, 0.3f, 0);
		}
		else if (level == 2)
		{
			camera.backgroundColor = new Color (0.0f, 0.75f, 0.82f);
		}
		else 
		{
			camera.backgroundColor = new Color (0.0f, 0.0f, 0.0f);
		}


		playerObj = GameObject.Find ("Player");

		//Initiate starting position
		x_pos = playerObj.transform.position.x;
		z_pos = playerObj.transform.position.z - 6;
		transform.position = new Vector3 (x_pos, playerObj.transform.position.y + 18, z_pos);
		transform.LookAt (playerObj.transform.position);

	
	}
	
	// Update is called once per frame
	void Update () 
	{

		var y = playerObj.transform.position.y + 13;

		if (Input.GetKey (KeyCode.Q)) {
			transform.RotateAround(playerObj.transform.position,Vector3.up, 40* Time.deltaTime);
			x_pos = transform.position.x - playerObj.transform.position.x ;
			z_pos = transform.position.z - playerObj.transform.position.z ;
		}
		if (Input.GetKey (KeyCode.E)) {
			transform.RotateAround(playerObj.transform.position,Vector3.up, -40* Time.deltaTime);
			x_pos = transform.position.x - playerObj.transform.position.x;
			z_pos = transform.position.z - playerObj.transform.position.z;
		}

		
		Vector3 v;

		if(Input.GetButton("Fire1")) 
		{
			v = new Vector3 (0,80,0);
			transform.position = v;
			Vector3 origin = new Vector3(0,0,0);
		       	transform.LookAt(origin);
		}
		else
		{
			v = new Vector3 (x_pos + playerObj.transform.position.x, 36 + ((y-20)/3), z_pos + playerObj.transform.position.z);
			transform.position = v;
			transform.LookAt (playerObj.transform.position);
		}

	}

	private bool revertFogState = true;
	void OnPreRender() {
		if (Input.GetButton ("Fire1")) {
			revertFogState = RenderSettings.fog;
			RenderSettings.fog = false;
		}
	}
	void OnPostRender() {
		RenderSettings.fog = revertFogState;
	}
}
