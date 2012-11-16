using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {
	
	// Use this for initialization
	
	// Vector3 velocity;
	
	public bool moving;
	public bool colliding;
	public bool decelerating=false;
	float lastVelocity;
	
	void Start () {
		
		moving=false;
		transform.localScale=new Vector3(constants.ballSize/1f,constants.ballSize/1f,constants.ballSize/1f);
		transform.position=new Vector3(transform.position.x,transform.position.y,-constants.ballSize/1.5f);

	}
	
	public void shoot(Vector3 force){
	
		moving=true;	
		// velocity=force;
		rigidbody.AddForce(force*constants.forceMultiplier,ForceMode.Impulse);
		
	}
	
	void Update () {
		
		// All ball movement here
		if (moving){
			// transform.Translate(new Vector3(velocity.x,velocity.y,0));
		}
		
		if (rigidbody.velocity.magnitude-lastVelocity<0){
			decelerating=true;
		}
		lastVelocity=rigidbody.velocity.magnitude;
		
	} 
	
}
