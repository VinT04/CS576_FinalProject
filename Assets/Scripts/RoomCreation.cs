using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreation : MonoBehaviour
{
    public int width = 9;
    public int length = 9;
    public GameObject player;
    public GameObject wallPrefab; // ColumnSquare_Small1 Variant
    public GameObject doorPrefab; // Desert_Temple_SegmentSingle is what this should be set to
    public GameObject pedestalPrefab;
    internal CellType[,] map;
    internal Bounds bounds;
    internal float wallHeight;
    private int roomIndex;

    void Start()
    {
        // Automatically create a room when the game starts
        //CreateRoom(1); // Default room index
        wallHeight = 10;
        bounds = GetComponent<Collider>().bounds;
        map = new CellType[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
                if(i==0 || j==0 || i == width-1 || j == width-1){
                    map[i, j] = CellType.WALL;
                }
                else{
                    map[i, j] = CellType.FLOOR;
                }
        }
        map[length/2,0] = CellType.DOOR;
        map[length/2,width-1] = CellType.DOOR;
        map[4,7] = CellType.DOORMAT;
        map[length/2,width/2] = CellType.PEDESTAL;

        drawMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void drawMap()
    {
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
                else if (map[w, l] == CellType.DOORMAT)
                {
                    GameObject obj = new GameObject("DOORMAT"); // Empty object to handle entering rooms
                    
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);
                    obj.transform.parent = transform;

                    BoxCollider collider = obj.AddComponent<BoxCollider>();
                    
                    collider.isTrigger = true; 
                    collider.size = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length);

                    obj.AddComponent<RoomExit>(); // Attach custom script
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
                else if (map[w, l] == CellType.PEDESTAL)
                {
                    GameObject obj = Instantiate(pedestalPrefab);
                    obj.name = "PEDESTAL";
                    obj.transform.localScale = new Vector3(bounds.size[0] / (float)width, wallHeight / 7, bounds.size[2] / (float)length);
                    obj.transform.position = new Vector3(x + 0.5f, y, z + 0.5f);
                    obj.transform.rotation = Quaternion.Euler(0, 90, 0);

                    // Add the first BoxCollider for triggering
                    BoxCollider triggerCollider = obj.GetComponent<BoxCollider>();
                    if (!triggerCollider)
                    {
                        triggerCollider = obj.AddComponent<BoxCollider>();
                    }
                    triggerCollider.isTrigger = true; // Set as a trigger
                    triggerCollider.size = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length * 3/5); // Adjust size as needed

                    // Create a child GameObject for the second collider
                    GameObject physicalCollider = new GameObject("PhysicalCollider");
                    physicalCollider.transform.parent = obj.transform;
                    physicalCollider.transform.localPosition = Vector3.zero; // Align with parent
                    physicalCollider.transform.localScale = Vector3.one; // Reset scale

                    // Add the second BoxCollider for physical collisions
                    BoxCollider collisionCollider = physicalCollider.AddComponent<BoxCollider>();
                    collisionCollider.isTrigger = false; // Enable physical collisions
                    collisionCollider.size = new Vector3(bounds.size[0] / (float)width, wallHeight / 2, bounds.size[2] / (float)length * 3/5); // Adjust size as needed

                    // Attach the custom script
                    obj.AddComponent<Pedestal>();
                }
            }
        }
    }
}
