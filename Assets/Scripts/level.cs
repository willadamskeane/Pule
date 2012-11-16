using UnityEngine;
using System.Collections;
using System.IO;

public class level : MonoBehaviour {
	
	public TextAsset fileName;
	string levelText;
	string levelName;
	int height;
	int width;
	int highScore;
	string highScoreName;
	int numLives;
	public Vector3 ballStart;
	GameObject[][] tiles;
	public GameObject tile;
	public Camera cam;
	
	void Start(){
		// LoadLevel (fileName.text);	
	}
	
	public void LoadLevel(string text)
	{
		levelText=text;
		char[] chars = text.ToCharArray();
		string w; string h;
		width=ToInt(chars[0])*10+ToInt(chars[1]);
		height=ToInt(chars[2])*10+ToInt(chars[3]);
		numLives=ToInt(chars[4]);
		Init(width,height);
		for(int j = height-1; j>=0; j--) {
			for(int i = 0; i<width; i++) {
				char type = chars[(i*height)+j+5];
				tiles[i][j].GetComponent<tile>().setType(type);
				if(type == '8'){
					ballStart = new Vector3(i, j, 0);
				}
				if (type == '1' || type == '2'){
					tiles[i][j].collider.isTrigger=false;	
				}
			}		
		}
		float oSize=width/3;
		if (height>width){oSize=height/2;}
		cam.orthographicSize=oSize;
		cam.transform.position=new Vector3(oSize*1.5f,oSize/1.5f,-10);
	}


	public void Init(int w, int h){
		// initialize tiles array to blank tiles
		tiles = new GameObject[w][];
		for(int i = 0; i<w; i++){
			tiles[i] = new GameObject[h];
			for(int j = 0; j<h; j++){
				tiles[i][j] = Instantiate(tile, new Vector3(i, j, 0), Quaternion.Euler(0,180,0)) as GameObject;
				tiles[i][j].GetComponent<tile>().level = gameObject;
				tiles[i][j].GetComponent<tile>().cam = cam;
			}
		}
	}

	public int ToInt(char c){
		return ((int) c) - ((int) '0');
	}
	
	/*
	public int GetTileType(Vector3 position){
		
		return tiles[(int) position.x][(int) position.y].GetComponent<tile>().type;
		
	}
	*/
}
