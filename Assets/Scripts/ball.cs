using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {
	
	// Use this for initialization
	
	Vector3 velocity;
	
	public bool moving;
	
	void Start () {
		
		moving=false;
		transform.localScale=new Vector3(constants.ballSize/1f,constants.ballSize/1f,constants.ballSize/1f);
		transform.position=new Vector3(transform.position.x,transform.position.y,-constants.ballSize/1f);

	}
	
	public void shoot(Vector3 force){
	
		moving=true;	
		velocity=force;
	}
	
	void Update () {
		
		// All ball movement here
		// transform.position=velocity;
		if (moving){
			transform.Translate(new Vector3(velocity.x,velocity.y,0));
		}
		
	} 
	
	void Spawn () {
		
		
		
	}
}
