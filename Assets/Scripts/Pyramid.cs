using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


enum CellType
{
    WALL = 0,
    FLOOR = 1,
    TREASURE = 2,
    DOOR = 3,
    R1 = 4,
    R2 = 5,
    R3 = 6,
    R4 = 7,
    R5 = 8,
    EXIT = 9,
    ENTRANCE = 10,
    PEDESTAL = 11,
    DOORMAT = 12,
    ANKH_DOORMAT = 13,
    ANKH_DOOR = 14,
}

public class Pyramid : MonoBehaviour
{
    // Start is called before the first frame update

    public int width = 25;
    public int length = 25;
    public GameObject player;
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject ankhdoorPrefab;

    // Add later buttons for intro/try again as needed
    internal CellType[,] map;
    internal Bounds bounds;
    internal float wallHeight;
    internal (float, float)[,] centers;
    public RawImage minimap_image;
    private Dictionary<int, Vector3> roomTileLocations;



    void Start()
    {
        wallHeight = transform.localScale.z;
        bounds = GetComponent<Collider>().bounds;
        map = new CellType[width, length];
        centers = new (float, float)[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
                map[i, j] = CellType.WALL;
        }
        initHallway(map);
        initRooms(map);
        drawMap();

        callMove(); // move the player

        // setup minimap
        minimap_image = GameObject.Find("Minimap Image").GetComponent<RawImage>();
        if (minimap_image != null) Debug.Log("found canvas!");
        minimap_image.texture = Resources.FindObjectsOfTypeAll<RenderTexture>()
            .FirstOrDefault(rt => rt.name == "Indoor Texture");

        if (GameObject.Find("Canvas") != null)
        {
            GameObject.Find("Canvas-Temp").SetActive(false);
        }

        // Get the room index
        int roomIndex = PlayerPrefs.GetInt("RoomIndex", 1);
    }

        // function for teleporting player
    void moveToTile(int roomNumber)
    {
        // Define a dictionary to map room numbers to their corresponding tile grid coordinates
        Dictionary<int, (int x, int z)> tileCoords = new Dictionary<int, (int x, int z)>
        {
            {0 , (12, 1)},
            { 1, (4, 11) }, // Room 1
            { 2, (4, 20) }, // Room 2
            { 4, (12, 19) }, // Room 4 -- these got switched somehow when creating rooms, doesn't matter though
            { 3, (19, 13) }, // Room 3
            { 5, (20, 2) }, // Room 5
            { 6, (9, 12) }  // Room 6
        };

        // Check if the dictionary contains the given room number
        if (tileCoords.TryGetValue(roomNumber, out (int x, int z) tile))
        {
            // Calculate the center of the tile
            Vector3 tileCenter = GetTileCenter(tile.x, tile.z);
            // Find the player GameObject
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Move the player to the tile center
                player.transform.position = tileCenter;
                Debug.Log($"Player moved to tile center at {tileCenter} for room {roomNumber}");
                while (Vector3.Distance(player.transform.position, tileCenter) > 0.1f)
                {
                        player.transform.position = tileCenter;
                }
            }
            else
            {
                Debug.LogError("Player GameObject not found! Ensure the player is tagged 'Player'.");
            }
        }
        else
        {
            Debug.LogError($"No tile coordinates found for room number {roomNumber}");
        }
        Debug.Log($"moved player to {player.transform.position}");
        PlayerPrefs.SetInt("RoomIndex", 0);
    }

    // helper for returning center of tile
    Vector3 GetTileCenter(int w, int l)
    {
        // Ensure the tile indices are within the map bounds
        if (w < 0 || w >= width || l < 0 || l >= length)
        {
            Debug.LogError($"Tile indices out of bounds: w={w}, l={l}");
            return Vector3.zero; // Return a default value
        }

        // Tile dimensions
        float tileWidth = bounds.size[0] / (float)width;
        float tileLength = bounds.size[2] / (float)length;
        tileWidth = 4.8f; // hardcode -- questionable
        tileLength = 4.8f;

        // Calculate the center of the tile
        float x = bounds.min[0] + (tileWidth * w) + (tileWidth / 2);
        float z = bounds.min[2] + (tileLength * l) + (tileLength / 2);
        float y = bounds.max[1]; // Assuming all tiles are at the same height

        return new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {

    }

        void initHallway(CellType[,] grid)
    {
        for (int i = 2; i < 15; i++)
            grid[1, i] = CellType.FLOOR;
        for (int i = 18; i < 21; i++) // changed from 23 to 21
            grid[1, i] = CellType.FLOOR;
        grid[2, 2] = CellType.FLOOR;
        grid[2, 11] = CellType.FLOOR;
        for (int i = 14; i < 19; i++)
            grid[2, i] = CellType.FLOOR;
        grid[2, 20] = CellType.FLOOR; // changed from 22 to 20

        grid[3, 2] = CellType.FLOOR;
        grid[3, 6] = CellType.FLOOR;
        grid[3, 11] = CellType.FLOOR;
        grid[3, 15] = CellType.FLOOR;
        grid[3, 18] = CellType.FLOOR;
        grid[3,20] = CellType.FLOOR;
        //for (int i = 20; i < 23; i++)
            //grid[3, i] = CellType.FLOOR;

        for (int i = 1; i < 4; i++)
            grid[4, i] = CellType.FLOOR;
        grid[4, 6] = CellType.FLOOR;
        grid[4, 10] = CellType.FLOOR;
        grid[4, 11] = CellType.FLOOR;
        grid[4, 15] = CellType.FLOOR;
        grid[4, 18] = CellType.FLOOR;
        grid[4, 19] = CellType.FLOOR;

        grid[5, 1] = CellType.FLOOR;
        for (int i = 3; i < 7; i++)
            grid[5, i] = CellType.FLOOR;
        for (int i = 11; i < 19; i++)
            grid[5, i] = CellType.FLOOR;

        grid[6, 1] = CellType.FLOOR;
        grid[6, 14] = CellType.FLOOR;
        grid[6, 18] = CellType.FLOOR;
        grid[6, 23] = CellType.FLOOR;

        grid[7, 1] = CellType.FLOOR;
        for (int i = 3; i < 9; i++)
            grid[7, i] = CellType.FLOOR;
        grid[7, 14] = CellType.FLOOR;
        grid[7, 15] = CellType.FLOOR;
        grid[7, 18] = CellType.FLOOR;
        for (int i = 20; i < 24; i++)
            grid[7, i] = CellType.FLOOR;

        grid[8, 1] = CellType.FLOOR;
        grid[8, 3] = CellType.FLOOR;
        for (int i = 8; i < 13; i++)
            grid[8, i] = CellType.FLOOR;
        for (int i = 15; i < 19; i++)
            grid[8, i] = CellType.FLOOR;
        grid[8, 20] = CellType.FLOOR;

        for (int i = 1; i < 6; i++)
            grid[9, i] = CellType.FLOOR;
        grid[9, 8] = CellType.FLOOR;
        grid[9, 12] = CellType.FLOOR;
        grid[9, 15] = CellType.FLOOR;
        grid[9, 20] = CellType.FLOOR;

        grid[10, 5] = CellType.FLOOR;
        grid[10, 8] = CellType.FLOOR;
        grid[10, 9] = CellType.FLOOR;
        for (int i = 15; i < 23; i++)
            grid[10, i] = CellType.FLOOR;

        for (int i = 4; i < 7; i++)
            grid[11, i] = CellType.FLOOR;
        grid[11, 9] = CellType.FLOOR;
        grid[11, 17] = CellType.FLOOR;
        grid[11, 19] = CellType.FLOOR;
        grid[11, 22] = CellType.FLOOR;

        for (int i = 1; i < 10; i++)
            grid[12, i] = CellType.FLOOR;
        for (int i = 15; i < 19; i++)
            grid[12, i] = CellType.FLOOR;
        grid[12, 22] = CellType.FLOOR;

        for (int i = 4; i < 7; i++)
            grid[13, i] = CellType.FLOOR;
        grid[13, 9] = CellType.FLOOR;
        grid[13, 15] = CellType.FLOOR;
        grid[13, 22] = CellType.FLOOR;
        grid[13, 23] = CellType.FLOOR;

        grid[14, 5] = CellType.FLOOR;
        grid[14, 9] = CellType.FLOOR;
        grid[14, 15] = CellType.FLOOR;
        grid[14, 23] = CellType.FLOOR;

        for (int i = 4; i < 8; i++)
            grid[15, i] = CellType.FLOOR;
        grid[15, 9] = CellType.FLOOR;
        grid[15, 10] = CellType.FLOOR;
        grid[15, 15] = CellType.FLOOR;
        grid[15, 23] = CellType.FLOOR;

        grid[16, 4] = CellType.FLOOR;
        grid[16, 7] = CellType.FLOOR;
        grid[16, 10] = CellType.FLOOR;
        for (int i = 18; i < 21; i++)
            grid[16, i] = CellType.FLOOR;
        grid[16, 23] = CellType.FLOOR;

        grid[17, 4] = CellType.FLOOR;
        for (int i = 7; i < 11; i++)
            grid[17, i] = CellType.FLOOR;
        grid[17, 18] = CellType.FLOOR;
        grid[17, 23] = CellType.FLOOR;

        grid[18, 4] = CellType.FLOOR;
        grid[18, 18] = CellType.FLOOR;
        grid[18, 23] = CellType.FLOOR;

        for (int i = 8; i < 16; i++)
            grid[19, i] = CellType.FLOOR;
        grid[19, 17] = CellType.FLOOR;
        grid[19, 18] = CellType.FLOOR;
        grid[19, 23] = CellType.FLOOR;

        for (int i = 3; i < 6; i++)
            grid[20, i] = CellType.FLOOR;
        grid[20, 8] = CellType.FLOOR;
        grid[20, 15] = CellType.FLOOR;
        grid[20, 17] = CellType.FLOOR;
        grid[20, 23] = CellType.FLOOR;

        grid[21, 1] = CellType.FLOOR;
        grid[21, 2] = CellType.FLOOR;
        for (int i = 5; i < 9; i++)
            grid[21, i] = CellType.FLOOR;
        for (int i = 15; i < 21; i++)
            grid[21, i] = CellType.FLOOR;
        grid[21, 23] = CellType.FLOOR;

        grid[22, 1] = CellType.FLOOR;
        grid[22, 20] = CellType.FLOOR;
        grid[22, 22] = CellType.FLOOR;
        grid[22, 23] = CellType.FLOOR;

        for (int i = 1; i < 23; i++)
            grid[23, i] = CellType.FLOOR;

        for (int i = 11; i < 14; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                grid[i, j] = CellType.FLOOR;
            }
        }
    }

    void initRooms(CellType[,] grid)
    {
        grid[3, 8] = CellType.R1;
        grid[3, 9] = CellType.R1;
        grid[4, 8] = CellType.R1;
        grid[4, 9] = CellType.R1;

        grid[3, 6] = CellType.DOORMAT; // R1 doormat
        grid[3, 7] = CellType.DOOR;
        grid[4, 10] = CellType.EXIT;

        grid[4, 21] = CellType.R2;
        grid[4, 22] = CellType.R2;
        grid[5, 21] = CellType.R2;
        grid[5, 22] = CellType.R2;

        grid[4, 21] = CellType.EXIT; // Moved to 4,21 from 4,20 to open hall space
        grid[4, 20] = CellType.FLOOR;
        grid[5, 23] = CellType.DOOR;
        grid[6, 23] = CellType.DOORMAT; // R2 doormat

        grid[13, 19] = CellType.R3;
        grid[13, 20] = CellType.R3;
        grid[14, 19] = CellType.R3;
        grid[14, 20] = CellType.R3;

        grid[13, 19] = CellType.EXIT; // Moved to 13,19 from 12,19 to open hall space
        grid[12, 19] = CellType.FLOOR;
        grid[15, 20] = CellType.DOOR;
        grid[16, 20] = CellType.DOORMAT; // R3 doormat

        grid[16, 13] = CellType.R4;
        grid[16, 14] = CellType.R4;
        grid[17, 13] = CellType.R4;
        grid[17, 14] = CellType.R4;

        grid[15, 15] = CellType.DOORMAT; // R4 doormat
        grid[16, 15] = CellType.DOOR;
        grid[18, 13] = CellType.EXIT;

        grid[18, 1] = CellType.R5;
        grid[18, 2] = CellType.R5;
        grid[19, 1] = CellType.R5;
        grid[19, 2] = CellType.R5;

        grid[18, 4] = CellType.DOORMAT; // R5 doormat
        grid[18, 3] = CellType.DOOR;
        grid[19, 2] = CellType.EXIT; // Moved to 19,2 from 20,2 to open hall space
        grid[20,2] = CellType.FLOOR;

        grid[12,0] = CellType.ENTRANCE;

        for (int i = 11; i < 14; i++)
        {
            for (int j = 11; j < 14; j++)
            {
                grid[i, j] = CellType.TREASURE;
            }
        }

        // Ankh-room:
        grid[10, 12] = CellType.EXIT;
        grid[12, 14] = CellType.ANKH_DOOR;
        grid[12, 15] = CellType.ANKH_DOORMAT; // AR doormat

    }

    void drawMap()
    {
        Debug.Log(1e-6f);
        int roomID = 1;
        int w = 0;
        for (float x = bounds.min[0]; x < bounds.max[0]; x += bounds.size[0] / (float)width, w++)
        {
            int l = 0;
            for (float z = bounds.min[2]; z < bounds.max[2]; z += bounds.size[2] / (float)length, l++)
            {
                if ((w >= width) || (l >= width))
                    continue;

                float y = bounds.min[1];

                if (map[w, l] == CellType.WALL)
                {
                    GameObject obj = Instantiate(wallPrefab);
                    obj.name = "WALL";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);

                    BoxCollider collider = obj.GetComponent<BoxCollider>();
                    if (!collider)
                    {
                        collider = obj.AddComponent<BoxCollider>();
                    }
                }
                else if (map[w, l] == CellType.DOOR)
                {
                    GameObject obj = Instantiate(doorPrefab);
                    obj.name = "DOOR";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);

                    BoxCollider collider = obj.GetComponent<BoxCollider>();
                    if (!collider)
                    {
                        collider = obj.AddComponent<BoxCollider>();
                    }
                }
                else if (map[w, l] == CellType.EXIT)
                {
                    GameObject obj = Instantiate(doorPrefab);
                    obj.name = "EXIT";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);
                    BoxCollider collider = obj.GetComponent<BoxCollider>();
                    if (!collider)
                    {
                        collider = obj.AddComponent<BoxCollider>();
                    }
                }
                else if (map[w, l] == CellType.ENTRANCE)
                {
                    /*
                    GameObject obj = Instantiate(entrancePrefab);
                    obj.name = "ENTRANCE";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);

                    BoxCollider collider = obj.GetComponent<BoxCollider>();
                    if (!collider)
                    {
                        collider = obj.AddComponent<BoxCollider>();
                    }
                    collider.isTrigger = true;
                    obj.AddComponent<EntranceTrigger>(); // THIS SHOULD BE CHANGED
                    */
                }
                else if (map[w, l] == CellType.DOORMAT)
                {
                    GameObject obj = new GameObject("DOORMAT"); // Empty object to handle entering rooms

                    obj.name = "DOORMAT";
                    
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);
                    obj.transform.parent = transform;

                    BoxCollider collider = obj.AddComponent<BoxCollider>();
                    
                    collider.isTrigger = true; 
                    collider.size = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);

                    obj.AddComponent<DoorInteraction>(); // For door handling stuff
                    DoorInteraction doorInteraction = obj.AddComponent<DoorInteraction>();
                    doorInteraction.roomIndex = roomID++; // Assign the specific room index dynamically
                }
                else if (map[w, l] == CellType.ANKH_DOORMAT)
                {
                    GameObject obj = new GameObject("ANKH_DOORMAT"); // Empty object to handle entering rooms

                    obj.name = "ANKH_DOORMAT";
                    
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);
                    obj.transform.parent = transform;

                    BoxCollider collider = obj.AddComponent<BoxCollider>();
                    
                    collider.isTrigger = true; 
                    collider.size = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);

                    //obj.AddComponent<DoorInteraction>(); // For door handling stuff
                    DoorInteraction doorInteraction = obj.AddComponent<DoorInteraction>();
                    doorInteraction.roomIndex = 6; // Assign the specific room index dynamically
                }
                else if (map[w, l] == CellType.ANKH_DOOR)
                {
                    GameObject obj = Instantiate(ankhdoorPrefab);
                    obj.name = "ANKH_DOOR";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);

                    BoxCollider collider = obj.GetComponent<BoxCollider>();
                    if (!collider)
                    {
                        collider = obj.AddComponent<BoxCollider>();
                    }
                }
            }
        }
        //callMove();
    }

    void callMove()
    {
        Debug.Log("move called");
        StartCoroutine(FadeAndMove());
    }

    private IEnumerator FadeAndMove()
    {
        // Fade in the black screen using InteractionTextManager
        InteractionTextManager.Instance.FadeInLoadingScreen(1.0f, "Loading ...");

        // Wait for the fade-in to complete
        yield return new WaitForSeconds(1.0f);

        // Perform the teleportation
        int roomIndex = PlayerPrefs.GetInt("RoomIndex", 1);
        moveToTile(roomIndex);
        //PlayerPrefs.SetInt("RoomIndex", 0); // Reset room index for the next scene

        // Wait for a short moment after teleporting (optional)
        yield return new WaitForSeconds(1.0f);

        // Fade out the black screen using InteractionTextManager
        InteractionTextManager.Instance.FadeOutLoadingScreen(1.0f);
        PlayerPrefs.SetInt("RoomIndex", 0);
    }
}
