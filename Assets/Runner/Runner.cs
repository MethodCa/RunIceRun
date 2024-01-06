using UnityEngine;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;
	private static int boosts;
   public AudioClip MiClips;


	public float acceleration;
	public Vector3 boostVelocity, jumpVelocity;
	public float gameOverY;

	private bool touchingPlatform;
	private Vector3 startPosition;
	
	void Start () {
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
 
	}
	
	
	
	void Update () {
		
		if(Input.GetButtonDown("Jump")){
			
			if(touchingPlatform){
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
				
					audio.PlayOneShot (MiClips);
            audio.Play();	
			}
			
		}
		distanceTraveled = transform.localPosition.x;
		 
		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
	}


	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}

	private void GameStart () {
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
	 
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
		
	}
	
	public static void AddBoost(){
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
	/*
	public void cambiartexto()
	{
		GameObject.Find("Distance Text").GetComponent<GUIText>().text="FDGDGFD";
	}
		
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,200,20),"Boton"))
		{
			Application.LoadLevel(0);
		}	
	}*/

}