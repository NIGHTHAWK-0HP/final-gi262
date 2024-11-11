using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Student
{

    public class OOPMapGenerator : MonoBehaviour
{
    [Header("Set MapGenerator")]
    public int X;
    public int Y;

    [Header("Set Player")]
    public OOPPlayer player;
    public Vector2Int playerStartPos;

    [Header("Set Exit")]
    public OOPExit Exit;

    [Header("Set Prefab")]
    public GameObject[] floorsPrefab;
    public GameObject[] wallsPrefab;
    public GameObject[] itemsPrefab;
    public GameObject[] keysPrefab;

    [Header("Set Transform")]
    public Transform floorParent;
    public Transform wallParent;
    public Transform itemPotionParent;

    [Header("Set object Count")]
    public int obsatcleCount;
    public int itemPotionCount;
    public int itemKeyCount;

    public int[,] mapdata;

    public OOPWall[,] walls;
    public OOPItemPotion[,] potions;

    // block types ...
    public int empty = 0;
    public int potion = 2;
    public int bonuesPotion = 3;
    public int exit = 4;
    public int key = 5;

    // Start is called before the first frame update
    void Start()
    {
        mapdata = new int[X, Y];

        // Create the map using floor and walls
        for (int x = -1; x < X + 1; x++)
        {
            for (int y = -1; y < Y + 1; y++)
            {
                if (x == -1 || x == X || y == -1 || y == Y)
                {
                    int r = Random.Range(0, wallsPrefab.Length);
                    GameObject obj = Instantiate(wallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
                    obj.transform.parent = wallParent;
                    obj.name = "Wall_" + x + ", " + y;
                }
                else
                {
                    int r = Random.Range(0, floorsPrefab.Length);
                    GameObject obj = Instantiate(floorsPrefab[r], new Vector3(x, y, 1), Quaternion.identity);
                    obj.transform.parent = floorParent;
                    obj.name = "floor_" + x + ", " + y;
                }
            }
        }

        // Initialize walls array (if needed for other purposes, or just for debugging)
        walls = new OOPWall[X, Y];

        // Place obstacles (walls) randomly
        int count = 0;
        while (count < obsatcleCount)
        {
            int x = Random.Range(0, X);
            int y = Random.Range(0, Y);
            if (mapdata[x, y] == empty)
            {
                PlaceWall(x, y);
                count++;
            }
        }

        // Set exit
        mapdata[X - 1, Y - 1] = exit;
        Exit.transform.position = new Vector3(X - 1, Y - 1, 0);
    }

    public int GetMapData(float x, float y)
    {
        if (x >= X || x < 0 || y >= Y || y < 0) return -1;
        return mapdata[(int)x, (int)y];
    }
    public void PlaceWall(int x, int y)
    {
        int r = Random.Range(0, wallsPrefab.Length);
        GameObject obj = Instantiate(wallsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
        obj.transform.parent = wallParent;
        mapdata[x, y] = 1;  // Mark as wall in map data
        walls[x, y] = obj.GetComponent<OOPWall>();
        walls[x, y].positionX = x;
        walls[x, y].positionY = y;
        walls[x, y].mapGenerator = this;
        obj.name = $"Wall_{x}, {y}";
    }
}
}