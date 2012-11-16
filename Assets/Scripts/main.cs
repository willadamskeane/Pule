using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {
		
	public GameObject ball;
	public GameObject currentBallObject;
	public ball currentBall;
	public GameObject cam;
	level currentLevel;
	public TextAsset level;
	public GameObject line;
	
	public AudioClip shot;
	
	bool aiming=false;
	bool shooting=false;
	
	Vector3 targetCamPos;
	
	public GUIStyle scoreStyle;
	
	// Use this for initialization
	void Start () {
		
		currentLevel = cam.GetComponent<level>();
		currentLevel.LoadLevel(level.text);
		currentBallObject=(GameObject)Instantiate(ball,currentLevel.ballStart,new Quaternion(0,0,0,0));
		currentBall=currentBallObject.GetComponent<ball>();
		currentBallObject.rigidbody.isKinematic=true;
		Physics.bounceThreshold=0.0f;
		Time.fixedDeltaTime = 0.005f;

	}
	
	// Update is called once per frame
	void Update () {
		
	    if (aiming){
            if (!line.renderer.enabled) line.renderer.enabled=true;
		    if (Input.GetMouseButtonUp(0)){
		      	shoot ();
		    }
        }else{
            if (line.renderer.enabled) line.renderer.enabled=false;    
        }
		
		if(Input.GetMouseButton (0)){
            aim ();
		}
		
		if (!aiming && (currentLevel.ballStart-currentBallObject.transform.position).magnitude<1){
			currentBallObject.collider.enabled=true;	
		}
		
		/*
		if (shooting){
			targetCamPos=new Vector3(currentBallObject.transform.position.x,currentBallObject.transform.position.y,-10);	
			cam.transform.Translate ((targetCamPos.x-cam.transform.position.x)*Time.deltaTime,(targetCamPos.y-cam.transform.position.y)*Time.deltaTime,0);
			Camera.main.orthographicSize=Camera.main.orthographicSize+(6-Camera.main.orthographicSize)*Time.deltaTime;
		}*/

	}
	
	void OnGUI(){
		GUI.Label(new Rect(10,10,Screen.width,70),new GUIContent("Score "),scoreStyle);
		// GUI.Label(new Rect(10,10,200,50),);
		
	}
	
    void aim(){
        
		aiming=true;
		shooting=false;
		
		currentBallObject.rigidbody.isKinematic = true;
		currentBallObject.collider.enabled=false;
		currentBall.moving=false;
		
        Vector3 clickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector3 startPos = currentLevel.ballStart;
       
        clickPos.z=-5;
        startPos.z=-5;

        if((clickPos-startPos).magnitude > constants.maxAimRadius){
        	clickPos=startPos+(clickPos - startPos).normalized*constants.maxAimRadius;
        }
       
        currentBallObject.transform.position = new Vector3(clickPos.x,clickPos.y,currentBall.transform.position.z);
        float length=(clickPos-startPos).magnitude*11.5f;
       
        line.GetComponent<LineRenderer>().SetColors(Color.red, Color.yellow);
        line.GetComponent<LineRenderer>().SetWidth(.2f,.5f);
        line.GetComponent<LineRenderer>().SetPosition(0,clickPos);
        line.GetComponent<LineRenderer>().SetPosition(1,startPos+(clickPos - startPos).normalized*-length);
       
        // currentBall.collider.enabled=false;
           
    }
	
	void shoot(){

		currentBallObject.rigidbody.isKinematic = false;
		
        Vector3 clickPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector3 startPos = currentLevel.ballStart;
		
		
        clickPos.z=-5;
        startPos.z=-5;

        if((clickPos-startPos).magnitude > constants.maxAimRadius){
                clickPos=startPos+(clickPos - startPos).normalized * constants.maxAimRadius;
        }
       
		// audio.pitch = (clickPos-startPos).magnitude / (constants.maxAimRadius / 2);
		audio.PlayOneShot(shot);
		
        float xspd=startPos.x-clickPos.x;
        float yspd=startPos.y-clickPos.y;
		
		aiming=false;
		shooting=true;
		
		// We should call a shoot method in ball here, which will apply appropriate force
		Vector3 force = new Vector3(xspd,yspd,0);
		currentBall.shoot(force);
		
		
	}
	
}
