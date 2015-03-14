using UnityEngine;
using System.Collections;

public class ballscript : MonoBehaviour {
	public Vector3 test;
	public GUIText scoreText;
	public int score;
	private int old_score;
	bool hasJump;
	public float time;
	public bool goalMet;
	public int goal;
	public GameObject player;
	private ballscript otherScript;
	
	private int controlType;
	public bool slow_isOn;
	public bool net_isOn;
	public float slow_timer;
	public float net_timer;
	public float POWERUP_DURATION = 10;
	public AudioSource source;
	public AudioClip death;
	public AudioClip step;
	public AudioClip step2;
	public AudioClip powerups;
	public AudioClip win;

	private float vel_vol = 0.3f;

	private Save_script saveScript;
	private GameObject saver;
	private int level;

	private bool win_sound = true;
	public GameObject block;
	private BlockTop_script top;

	public GUIText levelText;
	public int totalScore;

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad(transform.gameObject);
		saver = GameObject.Find("Save Sphere");
		saveScript = saver.GetComponent <Save_script> ();
		level = saveScript.level;
		time = saveScript.timeLeft;
		totalScore = (int) saveScript.highScore;

		hasJump = true;
		score = 0;
		old_score = 0;
		displayScore ();
		displayLevel ();

		Physics.gravity = new Vector3 (0, -20, 0);
		goalMet = false;

		if (level == 1)
		{
			goal = 1; //3
		}
		else if (level == 2)
		{
			goal = 1; //7
		}
		else
		{
			goal = 1; //15
		}
		

		GameObject.Find ("Fader").GetComponent<Fade> ().FadeIn ();

		player = GameObject.Find("Player");
		otherScript = player.GetComponent<ballscript> ();

		controlType = 3;

		slow_isOn = false;
		net_isOn = false;
		slow_timer = POWERUP_DURATION;
		net_timer = POWERUP_DURATION;

		transform.position = new Vector3 (0, 30, 0);

		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//generate sound on point collect
		if (old_score != score)
		{
			source.pitch = 1f;
			source.PlayOneShot(powerups, 1f);
			old_score = score;
		}
		if (Input.GetKey (KeyCode.Alpha1)) {
			controlType = 1;
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			controlType = 2;
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			controlType = 3;
		}


		if (controlType == 1) { // Original controls
			float forceFactor = 15 * rigidbody.mass;
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				rigidbody.AddForce (0, 0, forceFactor);
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				rigidbody.AddForce (0, 0, -forceFactor);
			}
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				rigidbody.AddForce (-forceFactor, 0, 0);
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				rigidbody.AddForce (forceFactor, 0, 0);
			}

			if (Input.GetKey(KeyCode.Space) && hasJump) {
			hasJump = false;
			rigidbody.velocity = new Vector3( rigidbody.velocity.x, 7, rigidbody.velocity.z );
		
				}
		} else if(controlType == 2) { // Version 2 Control
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector3.forward * 20f * Time.fixedDeltaTime, transform.parent);
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector3.back * 20f * Time.fixedDeltaTime, transform.parent);
			}
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector3.left * 20f * Time.fixedDeltaTime, transform.parent);
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector3.right * 20f * Time.fixedDeltaTime, transform.parent);
				//transform.Rotate (0, 0, 5);
			}

			if (Input.GetKey(KeyCode.Space) && hasJump) {
			hasJump = false;
			rigidbody.velocity = new Vector3( rigidbody.velocity.x, 7, rigidbody.velocity.z );
			
				}
		}
		else if(controlType == 3) { // Version 2 Control
			float forceFactor = 25 * rigidbody.mass;
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				rigidbody.AddForce (0, 0, forceFactor);
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				rigidbody.AddForce (0, 0, -forceFactor);
			}
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				rigidbody.AddForce (-forceFactor, 0, 0);
			}
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				rigidbody.AddForce (forceFactor, 0, 0);
			}

//<<<<<<< Updated upstream
			float max_speed = 12;
//=======
			if (Input.GetKey(KeyCode.Space) && hasJump) {
			hasJump = false;
			rigidbody.velocity = new Vector3( rigidbody.velocity.x, 20, rigidbody.velocity.z );
			}


//>>>>>>> Stashed changes
			if(rigidbody.velocity.magnitude > max_speed)
         	{
                rigidbody.velocity = rigidbody.velocity.normalized * max_speed;
         	}
		}
		

		displayScore ();
		displayLevel ();

		float f = (transform.position.y) / 25;

		if (goalMet != true) {
			time -= Time.fixedDeltaTime;
		}
		//reset level when time runs out
		if (time <= 0) {
			float fadeTime = GameObject.Find ("Player").GetComponent<Fade>().BeginFade(1);
			resetVars();
			Application.LoadLevel (Application.loadedLevel);
		}
		//reset game when ball falls out of bounds.
		Vector3 pos = transform.position;
		float field_limit = 22f;
		if (pos.z > field_limit || pos.z < -field_limit || pos.x > field_limit || pos.x < -field_limit)
		{
			print ("reset");
			resetVars();
			float fadeTime = GameObject.Find ("Player").GetComponent<Fade>().BeginFade(1);
			Application.LoadLevel (Application.loadedLevel);
		}
		if (slow_isOn == true) {
			slow_timer -= Time.fixedDeltaTime;

			if(slow_timer <= 0){
				slow_isOn = false;
			}
		}
		if (net_isOn == true) {
			net_timer -= Time.fixedDeltaTime;
			
			if(net_timer <= 0){
				net_isOn = false;
				Destroy(GameObject.Find("Net"));
			}
		} 
		if (score >= goal || Input.GetKey(KeyCode.H)) {
			goalMet = true;
			if (win_sound)
			{
				source.PlayOneShot(win, 1f);
				win_sound = false;
			}
		
		}

		if (transform.position.y >= 44) {
			saveScript.highScore = time;
			GameObject.Find ("Fader").GetComponent<Fade> ().FadeOut ();
			if(level != 1) {
				saveScript.level++;
				saveScript.timeLeft = time + 60f;
				Application.LoadLevel (Application.loadedLevel);
			}
			else {
				resetVars();
				Application.LoadLevel (0);
			}
		}

	}

	IEnumerator OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Block") {//&& collisionInfo.contacts [0].normal.y > .99) {
				hasJump = true;
				float hit_vol = collisionInfo.relativeVelocity.magnitude * vel_vol;
				float decider = Random.Range(0f, 1f);
				if (decider > 0.5f)
				{
					source.PlayOneShot(step, vel_vol);
				}
				else 
				{
					source.PlayOneShot(step2, vel_vol);
				}


				}
				else if (collisionInfo.collider.tag == "Ground") {
					source.PlayOneShot(death, 1f);
					saveScript.level = 1;
					saveScript.timeLeft = 60;
					float fadeTime = GameObject.Find ("Player").GetComponent<Fade>().BeginFade(1);
					yield return new WaitForSeconds(fadeTime);
					Application.LoadLevel (Application.loadedLevel);
					print ("Resetting");
				}

				//Allows players recovery
				if (collisionInfo.collider.tag == "PowerUp") {
					rigidbody.velocity = new Vector3( rigidbody.velocity.x, 50, rigidbody.velocity.z );
					slow_isOn = true;
					slow_timer = POWERUP_DURATION;
					source.PlayOneShot(powerups, 1f);
				}

				if (collisionInfo.collider.tag == "Net") {
					rigidbody.velocity = new Vector3( rigidbody.velocity.x, 50, rigidbody.velocity.z );
				}
				if (collisionInfo.collider.tag == "Net_Power") {
					rigidbody.velocity = new Vector3( rigidbody.velocity.x, 50, rigidbody.velocity.z );
					net_isOn = true;
					net_timer = POWERUP_DURATION;
					source.PlayOneShot(powerups, 1f);
					Instantiate (Resources.Load ("Net"), new Vector3 (-0.7f, 3.4f, -0.4f), Quaternion.identity);
				}


			if (collisionInfo.collider.tag == "Ground")
			{
				
				score = 0;
			}

	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Net") {
			rigidbody.velocity = new Vector3( rigidbody.velocity.x, 50, rigidbody.velocity.z );
		}
	}

	void OnTriggerStay (Collider collisionInfo)
	{
		//enabled only for control mode 2
		if (collisionInfo.tag == "BlockTop" && controlType == 2 &&
		!(Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) ||
		  Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) ||
		  Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.A) ||
		  Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.D) ||
		  Input.GetKey (KeyCode.Space))) {
			
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
			rigidbody.angularVelocity = Vector3.zero;
			Vector3 vel = new Vector3();
			float speed = 0;
			if(goalMet != true){
				vel =  collisionInfo.gameObject.GetComponent<BlockTop_script> ().vel;
				//vel.y = rigidbody.velocity.y;
				vel.y = 0;
				speed = collisionInfo.gameObject.GetComponent<BlockTop_script> ().speed;
			}
			transform.Translate (vel / 60 * speed );
		}
		if (collisionInfo.tag == "Block" ||
		    collisionInfo.tag == "BlockTop"){
			hasJump = true;
			
		}
	}
	void OnCollisionStay(Collision collisionInfo)
	{
		Debug.Log (collisionInfo.collider.tag);
		if (collisionInfo.collider.tag == "Block" ||
		    collisionInfo.collider.tag == "BlockTop"){
			hasJump = true;
			
		}
	}

	void displayScore()
	{
		scoreText.text = "Time: " + time.ToString("0") + "\nGoal: " + score.ToString () + "/" + goal + "\n"
					   + "Score: " + totalScore.ToString();
	}

	void displayLevel() 
	{
		levelText.text = "Level " + level;
	}

	void resetVars() {
		saveScript.level = 1;
		saveScript.timeLeft = 60;
		saveScript.highScore = 0;
	}
}
