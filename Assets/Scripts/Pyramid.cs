using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    PEDESTAL = 11
}
public class Pyramid:MonoBehaviour
{
    // Start is called before the first frame update

    public int width = 25;
    public int length = 25;
    public GameObject player;
    public GameObject wallPrefab; // ColumnSquare_Small1 Variant
    public GameObject doorPrefab; // Desert_Temple_SegmentSingle is what this should be set to
    public GameObject entrancePrefab; // Desert_Temple_Entrance
    public GameObject healthBar;

    // Add later buttons for intro/try again as needed
    internal CellType[,] map;
    internal Bounds bounds;
    internal float wallHeight;


    void Start()
    {
        wallHeight = transform.localScale.z;
        bounds = GetComponent<Collider>().bounds;
        map = new CellType[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
                map[i, j] = CellType.WALL;
        }
        // Make according to map
        // Puzzle rooms are 1 x 1
        // 5 Puzzle rooms, each with their own challenge - can generate later
        // Ankh room is 2 x 2
        // Walls and hallway are like normal
        // Import prefabs for walls, floors, rooms, etc.
        initHallway(map);
        initRooms(map);
        drawMap();

        if (!wallPrefab.GetComponent<Collider>())
        {
            wallPrefab.AddComponent<BoxCollider>();
        }

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

        grid[3, 7] = CellType.DOOR;
        grid[4, 10] = CellType.DOOR;

        grid[4, 21] = CellType.R2;
        grid[4, 22] = CellType.R2;
        grid[5, 21] = CellType.R2;
        grid[5, 22] = CellType.R2;

        grid[4, 20] = CellType.DOOR;
        grid[5, 23] = CellType.DOOR;

        grid[13, 19] = CellType.R3;
        grid[13, 20] = CellType.R3;
        grid[14, 19] = CellType.R3;
        grid[14, 20] = CellType.R3;

        grid[12, 19] = CellType.DOOR;
        grid[15, 20] = CellType.DOOR;

        grid[16, 13] = CellType.R4;
        grid[16, 14] = CellType.R4;
        grid[17, 13] = CellType.R4;
        grid[17, 14] = CellType.R4;

        grid[16, 15] = CellType.DOOR;
        grid[18, 13] = CellType.DOOR;

        grid[18, 1] = CellType.R5;
        grid[18, 2] = CellType.R5;
        grid[19, 1] = CellType.R5;
        grid[19, 2] = CellType.R5;

        grid[18, 3] = CellType.DOOR;
        grid[20, 2] = CellType.DOOR;

        grid[12,0] = CellType.ENTRANCE;

        for (int i = 11; i < 14; i++)
        {
            for (int j = 11; j < 14; j++)
            {
                grid[i, j] = CellType.TREASURE;
            }
        }

    }

    void drawMap()
    {
        Debug.Log(1e-6f);
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
                    collider.isTrigger = true; // Make it a trigger collider

                    obj.AddComponent<DoorInteraction>(); // Attach custom script
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
                    GameObject obj = Instantiate(entrancePrefab);
                    obj.name = "ENTRANCE";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);

                    BoxCollider collider = obj.GetComponent<BoxCollider>();
                    if (!collider)
                    {
                        collider = obj.AddComponent<BoxCollider>();
                    }
                    collider.isTrigger = true; // Make it a trigger collider
                    obj.AddComponent<EntranceTrigger>(); // Attach the custom script
                }
            }
        }
    }
}
