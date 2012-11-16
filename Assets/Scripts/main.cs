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
	bool postLevel=false;
	
	Vector3 targetCamPos;
	
	public int score;
	public GUIStyle scoreStyle;
	
	// Use this for initialization
	void Start () {
		
		currentLevel = cam.GetComponent<level>();
		currentLevel.LoadLevel(level.text);
		Physics.bounceThreshold=0.0f;
		Time.fixedDeltaTime = 0.005f;
		Spawn();

	}
	
	void Spawn(){
		if (currentLevel.livesLeft<=0){
			postLevel=true;
			currentLevel.DestroyLevel();
			Destroy (currentBallObject);
		}else{
			if (currentBallObject){
				Destroy(currentBallObject);
				aiming=false;
				shooting=false;
			}
			currentBallObject=(GameObject)Instantiate(ball,currentLevel.ballStart,new Quaternion(0,0,0,0));
			currentBall=currentBallObject.GetComponent<ball>();
			currentBallObject.rigidbody.isKinematic=true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!postLevel){
		    if (aiming){
	            if (!line.renderer.enabled) line.renderer.enabled=true;
			    if (Input.GetMouseButtonUp(0)){
			      	shoot ();
			    }
	        }else{
	            if (line.renderer.enabled) line.renderer.enabled=false;    
	        }
			
			if(Input.GetMouseButton (0)){
	            if (!aiming){
					Spawn ();	
				}
				aim ();
			}
			
			if (!aiming && (currentLevel.ballStart-currentBallObject.transform.position).magnitude<1){
				currentBallObject.collider.enabled=true;	
			}
			
			if (shooting){
				if (currentBall.rigidbody.velocity.magnitude<.1 && currentBall.decelerating){
					currentLevel.Explode(currentBallObject.transform.position);
					Spawn();
				}
			}
		}
		
		/*
		if (shooting){
			targetCamPos=new Vector3(currentBallObject.transform.position.x,currentBallObject.transform.position.y,-10);	
			cam.transform.Translate ((targetCamPos.x-cam.transform.position.x)*Time.deltaTime,(targetCamPos.y-cam.transform.position.y)*Time.deltaTime,0);
			Camera.main.orthographicSize=Camera.main.orthographicSize+(6-Camera.main.orthographicSize)*Time.deltaTime;
		}
		*/

	}
	
	void OnGUI(){
		if (!postLevel){
			string lifeTerm="shots left";
			if (currentLevel.livesLeft==1) lifeTerm="shot left";
			GUI.Label(new Rect(0,0,Screen.width,50),new GUIContent(currentLevel.currentScore.ToString()+" points, "+currentLevel.livesLeft+" "+lifeTerm),scoreStyle);
			// GUI.Label(new Rect(10,10,200,50),);
		}else{
			GUI.Label(new Rect(0,0,Screen.width,Screen.height),new GUIContent("You scored "+currentLevel.currentScore.ToString()+" points!"),scoreStyle);				
			if(GUI.Button(new Rect(0,0,Screen.width,Screen.height+60),new GUIContent("Restart"),scoreStyle)){
				postLevel=false;
				currentLevel = cam.GetComponent<level>();
				currentLevel.LoadLevel(level.text);
				Spawn();
			}
		}
		
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
		
		currentLevel.livesLeft--;
		
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
