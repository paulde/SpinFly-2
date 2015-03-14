using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public Vector3 vel;
	public float speed;
	public float original_speed;
	public Material level_1_mat_1;
	public Material level_1_mat_2;
	public Material level_2_mat_1;
	public Material level_2_mat_2;
	public Material level_3_mat_1;
	public Material level_3_mat_2;
	public Material Level_1_floor_mat;
	public Material Level_2_floor_mat;
	public Material Level_3_floor_mat;

	private int old_level = 0;

	private Renderer rend;

	public GameObject player;
	private ballscript otherScript;

	private Save_script saveScript;
	private GameObject saver;
	private int level;

	// Use this for initialization
	void Start () {


		saver = GameObject.Find("Save Sphere");
		saveScript = saver.GetComponent <Save_script> ();
		level = saveScript.level;

		speed = Random.Range( 2.0f, 5.0f );
		original_speed = speed;

		rend = GetComponent<Renderer>();
		//level_1_mat = Resources.Load("Materials/lambert1", typeof(Material)) as Material;

		player = GameObject.Find("Player");
		otherScript = player.GetComponent<ballscript> ();

		GameObject floor = GameObject.Find("pPlane1");
		Renderer floor_rend = floor.GetComponent<Renderer>();

		GameObject earth = GameObject.Find("earth");
		Renderer earth_rend = earth.GetComponent<Renderer>();

		GameObject lava_sparks = GameObject.Find("Lava Sparks");
		ParticleSystem sparks_rend = lava_sparks.GetComponent<ParticleSystem>();


		Material[] mats = null;
		Material floor_mat;

		if (level == 1)
		{
			mats = new Material[]{level_1_mat_1, level_1_mat_1, level_1_mat_2};
			floor_mat = Level_1_floor_mat;
			earth_rend.enabled = false;

		}
		else if (level == 2)
		{
			mats = new Material[]{level_2_mat_1, level_2_mat_1, level_2_mat_2};
			floor_mat = Level_2_floor_mat;
			sparks_rend.Stop();
			earth_rend.enabled = false;
		}
		else
		{
			mats = new Material[]{level_3_mat_1, level_3_mat_1, level_3_mat_2};
			floor_mat = Level_3_floor_mat;
			sparks_rend.Stop();
			floor_rend.enabled = false;
			earth_rend.enabled = true;
		}

		floor_rend.material = floor_mat;
		rend.materials = mats;
		
	}

	public void SetVelocity( Vector3 v )
	{
		vel = v;

		vel.Normalize ();
	}

	//public void Create( Vector3 vel, 


	
	void OnCollisionStay(Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Player" && collisionInfo.contacts[0].normal.y < -.9 ) {
			if( transform.position.y <= 4 )
			{
				var v = new Vector3( 0, (float)1.0 / 60, 0 );
				transform.Translate( v );
			}


			
		}

	}


	
	// Update is called once per frame
	void FixedUpdate () {


		//gameObject.GetComponent<BlockTop_script> ().vel;


		//int curr_level = GameObject.Find ("Player").GetComponent<"Block Script">.level;

		float f = (transform.position.y) / 4;
		if (otherScript.goalMet == true) 
		{
			speed = 0;
		}else if (otherScript.slow_isOn == true) {
			speed = 1;
		} else {
			speed = original_speed;
		}

		transform.Translate (vel / 60 * speed );


	}
}
