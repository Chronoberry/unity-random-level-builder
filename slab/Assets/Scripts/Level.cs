using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int levelWidth = 3;
	public int levelHeight = 3;

	public int tileWidth = 128;
	public int tileHeight = 128;

	public GameObject tileSprite;
	
	private int[,] levelTiles;
	private GameObject[,] tiles;

	private static int ROW = 0;
	private static int COL = 1;

	// Use this for initialization
	void Start () {

		levelTiles = new int[levelWidth, levelHeight];
		tiles = new GameObject[levelWidth, levelHeight];
		for (int row = 0; row < levelTiles.GetLength(ROW); row++) {
			for(int col = 0; col < levelTiles.GetLength(COL); col++){



				levelTiles[row, col] = Random.Range(0, 2);
				if( levelTiles[row, col] == 1){
					tiles[row, col] = (GameObject)Instantiate(tileSprite, new Vector3(row, col, 1), Quaternion.identity);
				}
				Debug.Log(levelTiles[row, col]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnGUI(){


	}
}
