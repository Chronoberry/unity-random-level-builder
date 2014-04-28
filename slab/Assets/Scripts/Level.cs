using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
    public int levelNumber = 0;
    public int levelWidth = 5;
    public int levelHeight = 5;
    public int levelFill = 1;

    public int tileWidth = 128;
    public int tileHeight = 128;

    public GameObject tileSprite;
    public GameObject transparentSprite;
    public GameObject boat; 
    public GameObject player;
    public GameObject rubberDuck; 
    public GameObject background;
    public GameObject octopus;
    public int maxDucks = 4;
    public int maxOctopus = 1;

    private int currentDucks = 0;
    private int currentOctopus = 0;
    private int[,] levelTiles;
    private GameObject[,] tiles;
    private const int EMPTY_TILE = 0;
    private const int FILLED_TILE = 1;
    private const int TRANSPARENT_TILE = 2;


    void Start() {
        transform.position = new Vector3(-(levelWidth/2.0f), -(float)2*levelHeight, 0f);
        // Setup background, level, boat and player spawn points
            SpawnBackground();
            SpawnLevel();
            SpawnBoat();	
            SpawnPlayer ();

            // Setup event listener to level up
            Messenger.AddListener("level up", LevelUp);
            Messenger.AddListener("respawn player", MovePlayer);
    }
	
	void LevelUp() {
		levelNumber++;
		levelWidth += Random.Range (1, 4);
		levelHeight += Random.Range (1, 2);
		DestroyAll();
		MoveBackground();
		SpawnLevel();
                MoveBoat();
		MovePlayer();
	}

	void SpawnBackground() {
		Vector3 backgroundPosition = new Vector3(levelWidth * 0.5f, levelHeight-1.5f, 0f);
		background = (GameObject)Instantiate(background, backgroundPosition, Quaternion.identity);
	}

	void MoveBackground() {
		background.transform.position = new Vector3(levelWidth * 0.5f, levelHeight-1.5f, 0f);
	}

	void SpawnBoat() {
		Vector3 boatStartPosition = new Vector3(levelWidth * 0.5f, levelHeight-1, 0f);
		boat = (GameObject)Instantiate(boat, boatStartPosition, Quaternion.identity);
	}

        void MoveBoat() {  
		boat.transform.position = new Vector3(levelWidth * 0.5f, levelHeight-1, 0f);
  	}

	void SpawnPlayer() {
		Vector3 playerStartPosition = new Vector3(levelWidth * 0.5f, levelHeight-3, 0f);
		player = (GameObject)Instantiate(player, playerStartPosition, Quaternion.identity);
	}

	void MovePlayer() {
		player.transform.position = new Vector3(levelWidth * 0.5f, levelHeight-3, 0f);
	}

	void SpawnLevel() {
		//Create the level
		currentDucks = 0;
		currentOctopus = 0;
		maxDucks = (levelNumber + 1) * Random.Range(2, 6);
		maxOctopus = levelNumber / 2;
		levelFill = Mathf.CeilToInt (levelNumber / 10f);
		Debug.Log (levelFill);
		levelTiles = new int[levelWidth, levelHeight];
		levelTiles = TemplateLevel();
		tiles = new GameObject[levelWidth, levelHeight];
		for (int col = 0; col < levelHeight; col++) {
			for(int row = 0; row < levelWidth; row++) {
				// Randomly mark tile as filled
				if(levelTiles[row, col] != FILLED_TILE && levelTiles[row, col] != TRANSPARENT_TILE && col != levelHeight-2) {
					levelTiles[row, col] = randomFillTile ();
				}
				// Create game objects for each filled tile
				if(levelTiles[row, col] == FILLED_TILE) {
					tiles[row, col] = (GameObject)Instantiate(tileSprite, new Vector3(row, col, 1), Quaternion.identity);
					tiles[row, col].transform.Rotate(0, 0, (Random.Range(0, 13)*37), Space.World);
				}
				else if(levelTiles[row, col] == TRANSPARENT_TILE) {
					tiles[row, col] = (GameObject)Instantiate(transparentSprite, new Vector3(row, col, 1), Quaternion.identity);
				}
				// Randomly spawn rubber ducks
				if(randomSpawnDucks() &&
				   currentDucks < maxDucks &&
				   levelTiles[row, col] != FILLED_TILE &&
				   levelTiles[row, col] != TRANSPARENT_TILE)
				{
					Instantiate(rubberDuck, new Vector3(row, col, 1), Quaternion.identity);
					currentDucks += 1;
				}
				// Randomly spawn octopus
				if(randomSpawnOctopus() &&
				   currentOctopus < maxOctopus &&
				   levelTiles[row, col] != FILLED_TILE &&
				   levelTiles[row, col] != TRANSPARENT_TILE)
				{
					Instantiate(octopus, new Vector3(row, col, 1), Quaternion.identity);
					currentOctopus += 1;
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

    bool randomSpawnDucks() {
        bool spawn = false;
        int random = Random.Range(0, levelNumber+1);
        if (random < 1) {
                spawn = true;
        }
        return spawn;
    }
	
	bool randomSpawnOctopus() {
		bool spawn = false;
		int random = Random.Range(0, levelNumber+5);
		if (random < 1) {
			spawn = true;
		}
		return spawn;
	}

	void DestroyAll() {
		object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) ;
		foreach (object thisObject in allObjects) {
			if (((GameObject)thisObject).activeInHierarchy) {
				if (((GameObject)thisObject).tag != "LevelBuilder" && 
				    ((GameObject)thisObject).tag != "MainCamera" &&
				    ((GameObject)thisObject).tag != "Player" && 
				    ((GameObject)thisObject).tag != "Background" && 
				    ((GameObject)thisObject).tag != "SoundManager" && 
				    ((GameObject)thisObject).tag != "Boat")
				{
					Debug.Log(((GameObject)thisObject).ToString());
					try {
						Debug.Log(((GameObject)thisObject).GetComponent("MessengerHelper").ToString());
					}
					catch (System.Exception e) {
						Destroy((GameObject)thisObject);
					}
				}
			}
		}
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

    public int getCurrentDucks(){
        return currentDucks;
    }
}
