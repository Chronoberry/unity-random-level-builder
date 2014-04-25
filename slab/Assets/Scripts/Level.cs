using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public int levelWidth = 3;
	public int levelHeight = 3;
	public int levelFill = 1;

	public int tileWidth = 128;
	public int tileHeight = 128;

	public GameObject tileSprite;
	
	private int[,] levelTiles;
	private GameObject[,] tiles;
	private const int EMPTY_TILE = 0;
	private const int FILLED_TILE = 1;

	// Use this for initialization
	void Start() {
		levelTiles = new int[levelWidth, levelHeight];
		levelTiles = TemplateLevel();
		tiles = new GameObject[levelWidth, levelHeight];
		for (int row = 0; row < levelWidth; row++) {
			for(int col = 0; col < levelHeight; col++){
				if(levelTiles[row, col] != FILLED_TILE) {
					levelTiles[row, col] = randomFillTile ();
				}
				if(levelTiles[row, col] == 1) {
					tiles[row, col] = (GameObject)Instantiate(tileSprite, new Vector3(row, col, 1), Quaternion.identity);
				}
				//Debug.Log(levelTiles[row, col]);
			}
		}
	}

	int[,] TemplateLevel() {
		int[,] levelTemplate = new int[levelWidth, levelHeight];
		for (int row = 0; row < levelWidth; row++) {
			levelTemplate[row, 0] = FILLED_TILE;				// left wall
			levelTemplate[row, levelHeight-1] = FILLED_TILE;	// right wall
		}
		for (int col = 0; col < levelHeight; col++) {
			levelTemplate [0, col] = FILLED_TILE;				// ceiling
			levelTemplate [levelWidth - 1, col] = FILLED_TILE;	// floor
		}
		return levelTemplate;
	}

	int randomFillTile() {
		int fill = 0;
		int random = Random.Range (0, 10);
		if (random < levelFill) {
			fill = 1;
		}
		return fill;
	}

	// Update is called once per frame
	void Update() {
	}

	void OnGUI() {
	}
}
