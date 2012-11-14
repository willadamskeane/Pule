using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	
	public GameObject level;
	public Camera cam;
	char type;
	bool scoring=false;
	
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
		}
	}
	
	public void setType(char type)
	{
		renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type,typeof(Texture2D));
		this.type=type;
	}
	
	void OnTriggerEnter(Collider other){
		Activate ();
		Debug.Log (type);
	}
	
	void OnGUI(){
		if (scoring){
			Vector3 pos = cam.WorldToScreenPoint (transform.position);
			GUI.Label(new Rect(pos.x,10,Screen.height-pos.y,90),new GUIContent("5"));	
		}
	}
	
}
