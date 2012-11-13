using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	
	public GameObject level;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setType(char type)
	{
		renderer.material.mainTexture = (Texture2D)Resources.Load("Tiles/"+type,typeof(Texture2D));
		
	}
	
}
