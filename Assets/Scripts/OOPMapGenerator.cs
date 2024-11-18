using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public Transform tileParent;
        public Transform itemPotionParent;

        [Header("Set object Count")]
        public int obsatcleCount;
        public float itemDensity = 0.05f; // Percentage of grid to be filled with items (5% by default)

        public int itemPotionCount;

        public int[,] mapdata;

        public OOPWall[,] walls;
        public OOPItemPotion[,] potions;

        // block types ...
        public int empty = 0;
        public int tile = 1;
        public int potion = 2;
        public int exit = 4;

        // Start is called before the first frame update
        void Start()
        {
            mapdata = new int[X, Y];

            // Generate walls and floors
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

            // Calculate item count based on map size and density ratio
            itemPotionCount = Mathf.RoundToInt(X * Y * itemDensity);

            potions = new OOPItemPotion[X, Y];
            count = 0;
            while (count < itemPotionCount)
            {
                int x = Random.Range(0, X);
                int y = Random.Range(0, Y);
                // Ensure the tile is empty and not already occupied by an obstacle or item
                if (mapdata[x, y] == empty)
                {
                    PlaceItem(x, y);
                    count++;
                }
            }

            // Place the exit
            mapdata[X - 1, Y - 1] = exit;
            Exit.transform.position = new Vector3(X - 1, Y - 1, 0);
        }

        public int GetMapData(float x, float y)
        {
            if (x >= X || x < 0 || y >= Y || y < 0) return -1;
            return mapdata[(int)x, (int)y];
        }

        public void PlaceItem(int x, int y)
        {
            // Ensure the spot is empty before placing the item
            if (mapdata[x, y] != empty)
            {
                Debug.LogWarning($"Cannot place item at ({x}, {y}) because the tile is already occupied.");
                return;
            }

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
            // Ensure the spot is empty before placing the tile (obstacle)
            if (mapdata[x, y] != empty)
            {
                Debug.LogWarning($"Cannot place tile at ({x}, {y}) because the tile is already occupied.");
                return;
            }

            if (tilePrefab == null || tilePrefab.Length == 0)
            {
                Debug.LogError("tilePrefab array is not assigned or is empty!");
                return;
            }

            int r = Random.Range(0, tilePrefab.Length);
            if (tilePrefab[r] == null)
            {
                Debug.LogError($"tilePrefab[{r}] is null!");
                return;
            }

            // Instantiate the tile prefab (obstacle)
            GameObject obj = Instantiate(tilePrefab[r], new Vector3(x, y, 0), Quaternion.identity);
            obj.transform.parent = tileParent;

            // Log which prefab is being used
            Debug.Log($"Placing tile at ({x}, {y}) using prefab: {tilePrefab[r].name}");

            // Check if the prefab is supposed to have the OOPWall component
            OOPWall wallComponent = obj.GetComponent<OOPWall>();

            if (wallComponent == null)
            {
                // Log a warning if the prefab doesn't have the OOPWall component
                Debug.LogWarning($"OOPWall component missing on prefab: {tilePrefab[r].name}. Skipping tile placement at ({x}, {y}).");

                // Optionally, you could destroy the object if it's not valid
                Destroy(obj);  // Destroy the instantiated object since it's not valid for placement
                return;
            }

            // If the component is found, assign it to the walls array
            walls[x, y] = wallComponent;

            // Initialize wall properties
            walls[x, y].positionX = x;
            walls[x, y].positionY = y;
            walls[x, y].mapGenerator = this;
            obj.name = $"Tile_{walls[x, y].Name} {x}, {y}";

            // Mark the spot as occupied by an obstacle
            mapdata[x, y] = tile;
        }
    }
}
