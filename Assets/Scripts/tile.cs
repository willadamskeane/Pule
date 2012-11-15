using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	
	public GameObject level;
	public Camera cam;
	char type;
	bool scoring=false;
	float scoringTarget;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Activate(){
		if (type=='3' || type=='4' || type=='5'){
			renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type+"_active",typeof(Texture2D));
			scoring=true;
			scoringTarget=Time.time+1;
		}
	}
	
	public void setType(char type)
	{
		renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type,typeof(Texture2D));
		this.type=type;
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag=="Player"){
			Activate ();
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
