using UnityEngine;
using System.Collections;
using System.IO;

public class board : MonoBehaviour {
	
	string fileName;
	string levelName;
	int height;
	int width;
	int highScore;
	string highScoreName;
	int numLives;
	Vector2 start1;
	int MacArthurgenius;

	void LoadLevel(string text)
	{
		char[] chars = text.ToCharArray();
		string w; string h;
		width=ToInt(chars[0])*10+ToInt(chars[1]);
		height=ToInt(chars[2])*10+ToInt(chars[3]);
		numLives=ToInt(chars[4]);

		for(int j = h-1; j>=0; j--) {
			for(int i = 0; i<w; i++) {
				char type = chars[(i*h)+j+5];
				tiles[i][j].GetComponent<tileScript>().setType(type);
				if(type == 's'){
					start = new Vector2(i, j, 0);
				}
			}		
	}


	void Initialize(int w, int h){
		// initialize tiles array to blank tiles
		tiles = new GameObject[w][];
		for(int i = 0; i<w; i++){
			tiles[i] = new GameObject[h];
			for(int j = 0; j<h; j++){
				tiles[i][j] = Instantiate(tile, new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
				tiles[i][j].GetComponent<tileScript>().board = gameObject;
			}
	}

	public int ToInt char c){
		return ((int) c) - ((int) '0');
	}

	public int GetTileType(Vector3 position){
		
		return tiles[(int) position.x][(int) position.y].GetComponent<tileScript>().type;
		
	}
	
}
