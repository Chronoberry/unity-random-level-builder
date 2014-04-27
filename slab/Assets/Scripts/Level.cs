using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
    public int levelWidth = 3;
    public int levelHeight = 3;
    public int levelFill = 1;

    public int tileWidth = 128;
    public int tileHeight = 128;

    public GameObject tileSprite;
	public GameObject transparentSprite;
    public GameObject boat; 
    public GameObject treasureChest; 
    public int maxChests = 4;

    private int currentChests = 0;
    private int[,] levelTiles;
    private GameObject[,] tiles;
    private const int EMPTY_TILE = 0;
    private const int FILLED_TILE = 1;
	private const int TRANSPARENT_TILE = 2;


    void Start() {
        //Setup boat and player spawn points
        Vector3 boatStartPosition = new Vector3( levelWidth * 0.5f, levelHeight - 1, 0f);
        boat = (GameObject)Instantiate( boat, boatStartPosition, Quaternion.identity);
        
        //Create the level
        levelTiles = new int[levelWidth, levelHeight];
        levelTiles = TemplateLevel();
        tiles = new GameObject[levelWidth, levelHeight];
        for (int col = 0; col < levelHeight; col++) {
            for(int row = 0; row < levelWidth; row++){
				if(levelTiles[row, col] != FILLED_TILE && levelTiles[row, col] != TRANSPARENT_TILE) {
                    levelTiles[row, col] = randomFillTile ();
                }
				if(levelTiles[row, col] == FILLED_TILE) {
                    tiles[row, col] = (GameObject)Instantiate(tileSprite, new Vector3(row, col, 1), Quaternion.identity);
                }
				else if(levelTiles[row, col] == TRANSPARENT_TILE) {
					tiles[row, col] = (GameObject)Instantiate(transparentSprite, new Vector3(row, col, 1), Quaternion.identity);
				}
				if(randomFillChest() == 1 && currentChests < maxChests && col !=0 && levelTiles[row, col+1] != FILLED_TILE) {
					treasureChest = (GameObject)Instantiate(treasureChest, new Vector3(row, col+1, 1), Quaternion.identity);
					currentChests += 1;
				}
            }
        }
    }

    int[,] TemplateLevel() {
        int[,] levelTemplate = new int[levelWidth, levelHeight];
		for (int col = 0; col < levelHeight; col++) {
			levelTemplate [0, col] = FILLED_TILE;					// left wall
			levelTemplate [levelWidth - 1, col] = FILLED_TILE;		// right wall
		}
		for (int row = 0; row < levelWidth; row++) {
			levelTemplate[row, 0] = FILLED_TILE;					// floor
			levelTemplate[row, levelHeight-1] = TRANSPARENT_TILE;	// ceiling
        }
        return levelTemplate;
    }

    int randomFillTile() {
        int fill = 0;
        int random = Random.Range(0, 13);
        if (random < levelFill) {
                fill = 1;
        }
        return fill;
    }

	int randomFillChest() {
		int fill = 0;
		int random = Random.Range(0, 20);
		if (random < 1) {
			fill = 1;
		}
		return fill;
	}

    public int getLevelHeight() {
        return this.levelHeight;
    }

    public int getLevelWidth(){
        return this.levelWidth;

    }

    // Update is called once per frame
    void Update() {
    }

    void OnGUI() {
    }
}
