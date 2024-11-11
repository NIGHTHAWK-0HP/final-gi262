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
        public GameObject[] tilePrefab;
        public GameObject[] itemsPrefab;

        [Header("Set Transform")]
        public Transform floorParent;
        public Transform wallParent;
        public Transform itemPotionParent;
        public Transform tileParent;

        [Header("Set object Count")]
        public int obsatcleCount;
        public int itemPotionCount;

        public int[,] mapdata;

        public OOPWall[,] walls;
        public OOPItemPotion[,] potions;

        // block types ...
        public int empty = 0;
        public int tile = 1;
        public int potion = 2;

        // Start is called before the first frame update
        void Start()
        {
            mapdata = new int[X, Y];
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

            walls = new OOPWall[X, Y];
            int count = 0;
            while (count < obsatcleCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == 0)
                {
                    PlaceTile(x, y);
                    count++;
                }
            }

            potions = new OOPItemPotion[X, Y];
            count = 0;
            while (count < itemPotionCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                if (mapdata[x, y] == empty)
                {
                    PlaceItem(x, y);
                    count++;
                }
            }
        }

        public int GetMapData(float x, float y)
        {
            if (x >= X || x < 0 || y >= Y || y < 0) return -1;
            return mapdata[(int)x, (int)y];
        }

        public void PlaceItem(int x, int y)
        {
            int r = Random.Range(0, itemsPrefab.Length);
            GameObject obj = Instantiate(itemsPrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = itemPotionParent;
            mapdata[x, y] = potion;
            potions[x, y] = obj.GetComponent<OOPItemPotion>();
            potions[x, y].positionX = x;
            potions[x, y].positionY = y;
            potions[x, y].mapGenerator = this;
            obj.name = $"Item_{potions[x, y].Name} {x}, {y}";
        }

        public void PlaceTile(int x, int y)
        {
            int r = Random.Range(0, tilePrefab.Length);
            GameObject obj = Instantiate(tilePrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = tileParent;
            mapdata[x, y] = tile;
            walls[x, y] = obj.GetComponent<OOPWall>();
            walls[x, y].positionX = x;
            walls[x, y].positionY = y;
            walls[x, y].mapGenerator = this;
            obj.name = $"Box_{walls[x, y].Name} {x}, {y}";
        }
    }
}