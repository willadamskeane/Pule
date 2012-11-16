using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	
	public level currentLevel;
	public Camera cam;
	public GameObject breakingAnimation;
	char type;
	bool scoring=false;
	float scoringTarget;
	float breakAt;
	bool activated=false;
	bool activeColoring=false;
	
	public AudioClip arrow;
	public AudioClip low;
	public AudioClip med;
	public AudioClip high;
	public AudioClip breaking;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(breakAt>0 && Time.time>breakAt){
			breakAt=0;
			int randInt=Random.Range (0,20);
			char newType='0';
			if (randInt>10)
			{
				newType='3';
			}else if(randInt>15){
				newType='4';	
			}else if(randInt>18){
				newType='5';
			}
			setType(newType);
						
		}
		
		if (type=='i'){
			if (!activated){
				renderer.material.color =  Color.Lerp (Color.red, Color.green,  Mathf.PingPong (Time.time, constants.activateColorTime) / constants.activateColorTime);
			}
		}
		
	}
	
	public void Activate(GameObject collidingBallObject){
		
		Vector3 pushForce = new Vector3(0,0,0);
		
		if (type=='2'){
			breakAt=Time.time+constants.breakDelay;
			audio.PlayOneShot(breaking);
			GameObject bricks=(GameObject)Instantiate(breakingAnimation,transform.position,new Quaternion(0,0,0,0));
			Destroy(bricks, 1);
		}
		
		if ((type=='3' || type=='4' || type=='5' || type=='h') && !activated){
			
			activated=true;
			renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type+"_active",typeof(Texture2D));
			scoring=true;
			scoringTarget=Time.time+1;
			switch (type)
			{
				case '3':
					audio.PlayOneShot(low);
					currentLevel.currentScore+=10;
				break;
				case '4':
					audio.PlayOneShot(med);
					currentLevel.currentScore+=50;
				break;
				case '5':
					audio.PlayOneShot(high);
					currentLevel.currentScore+=100;
				break;
				case 'h':
					audio.PlayOneShot(high);
					currentLevel.currentScore+=200;
				break;
			}
		}
		
		if(type == '9'){
			pushForce = (new Vector3(0, 1, 0)).normalized;
		}
		
		if(type == 'a'){
			pushForce = (new Vector3(1, 1, 0)).normalized;
		}
		if(type == 'b'){
			pushForce = (new Vector3(1, 0, 0)).normalized;
		}
		if(type == 'c'){
			pushForce = (new Vector3(1, -1, 0)).normalized;
		}
		if(type == 'd'){
			pushForce = (new Vector3(0, -1, 0)).normalized;
		}
		if(type == 'e'){
			pushForce = (new Vector3(-1, -1, 0)).normalized;
		}
		if(type == 'f'){
			pushForce = (new Vector3(-1, 0, 0)).normalized;
		}
		if(type == 'g'){
			pushForce = (new Vector3(-1, 1, 0)).normalized;
		}
		
		if (pushForce.magnitude>0)
		{
			// activeColoring=true;
			audio.PlayOneShot(arrow);
			collidingBallObject.rigidbody.AddRelativeForce (constants.arrowForceMultiplier*pushForce,ForceMode.Impulse);
		}
		
		if (type=='i'){
			currentLevel.livesLeft++;
			activated=true;
			// Instantiate (collidingBallObject,new Vector3(collidingBallObject.transform.position.x-2,collidingBallObject.transform.position.y+2,collidingBallObject.transform.position.z),new Quaternion(0,0,0,0));
			setType('0');
			// collidingBallObject.transform.localScale=new Vector3(constants.ballSize*1.5f,constants.ballSize*1.5f,constants.ballSize*1.5f);
		}
		
		
	}
	
	public void setType(char type)
	{
		renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type,typeof(Texture2D));
		if (type == '1' || type == '2'){ 
			collider.isTrigger=false;	
		}else{
			collider.isTrigger=true;
		}
		this.type=type;
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag=="Player"){
			Activate (other.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.tag=="Player"){
			Activate (other.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision other){
		if (other.collider.tag=="Player"){
			Activate (other.gameObject);
		}	
	}
	
	void OnGUI(){
		/*
		if (scoring){
			Vector3 pos = cam.WorldToScreenPoint (transform.position);
			GUI.Label(new Rect(pos.x-3,Screen.height-pos.y+10+(scoringTarget/Time.time)*200,20,20),new GUIContent("5"));	
		}
		*/
	}
	
}
