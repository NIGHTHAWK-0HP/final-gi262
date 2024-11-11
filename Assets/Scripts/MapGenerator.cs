using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;    // Tilemap ที่ใช้แสดงผล
    public Tile groundTile;    // Tile ที่ใช้แสดงพื้นที่ว่าง
    public Tile wallTile;      // Tile ที่ใช้แสดงกำแพง

    public int width = 20;
    public int height = 20;
    int[,] map;

    void Start()
    {
        GenerateMap();
        DrawMap();
    }

    void GenerateMap()
    {
        map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.Range(0f, 1f) < 0.2f)  // 20% เป็นกำแพง
                {
                    map[x, y] = 1;  // กำแพง
                }
                else
                {
                    map[x, y] = 0;  // พื้นที่ว่าง
                }
            }
        }
    }

    void DrawMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                if (map[x, y] == 1)
                {
                    tilemap.SetTile(position, wallTile);  // วางกำแพง
                }
                else
                {
                    tilemap.SetTile(position, groundTile);  // วางพื้น
                }
            }
        }
    }
        // ฟังก์ชันใน Update สำหรับตรวจสอบการกดปุ่ม R
        void Update()
        {
            // ตรวจสอบว่าผู้เล่นกดปุ่ม R หรือไม่
            if (Input.GetKeyDown(KeyCode.R))
            {
                GenerateMap();  // รีเจนแผนที่ใหม่
            }
        }
}
