using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	
	public GameObject level;
	public Camera cam;
	char type;
	bool scoring=false;
	float scoringTarget;
	float breakAt;
	
	public AudioClip arrow;
	public AudioClip low;
	public AudioClip med;
	public AudioClip high;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(breakAt>0 && Time.time>breakAt){
			breakAt=0;
			setType((char) (((int) '0') + Random.Range (3,5)));
		}
		
	}
	
	public void Activate(GameObject collidingBallObject){
		
		Vector3 pushForce = new Vector3(0,0,0);
		
		if (type=='2'){
			breakAt=Time.time+.2f;
		}
		
		if (type=='3' || type=='4' || type=='5'){
			renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type+"_active",typeof(Texture2D));
			scoring=true;
			scoringTarget=Time.time+1;
			switch (type)
			{
				case '3':
					audio.PlayOneShot(low);
				break;
				case '4':
					audio.PlayOneShot(med);
				break;
				case '5':
					audio.PlayOneShot(high);
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
			audio.PlayOneShot(arrow);
			collidingBallObject.rigidbody.AddRelativeForce (constants.arrowForceMultiplier*pushForce,ForceMode.Impulse);
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
