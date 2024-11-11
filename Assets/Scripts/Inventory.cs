using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Student
{
    public class Inventory : MonoBehaviour
    {
        // 1. สร้าง Dictionary เพื่อเก็บข้อมูลของไอเทมในคลังของผู้เล่น
        public Dictionary<string, int> inventory = new Dictionary<string, int> ();

        // 2. ฟังก์ชันสำหรับเพิ่มไอเทม
        public void AddItem(string itemName)
        {
            if (inventory.ContainsKey (itemName))
            {
                //int amount = inventory[itemName];
                //amount = amount + 1;
                //inventory[itemName] = amount;
                inventory[itemName] += 1;
            }
            else
            {
                //inventory (itemName) = 1;
                inventory.Add (itemName, 1);
            }
          
        }

        // 3. ฟังก์ชันสำหรับลบไอเทม
        public void UseItem(string itemName)
        {
            if (inventory.ContainsKey (itemName))
            {
                inventory[itemName] -= 1;
                if (inventory[itemName] == 0)
                {
                    inventory.Remove (itemName);
                }
            }
            else
            {
                Debug.LogWarning("No item");
            }
        }

        // 4. ฟังก์ชันสำหรับตรวจสอบจำนวนไอเทมในคลัง
        public int numberOfItem(string itemName)
        {
            if (!inventory.ContainsKey (itemName))
            {
                return inventory[itemName];
            }
            else
            {
                return 0;
            }
        }

        // 5. ฟังก์ชันสำหรับแสดงจำนวนไอเทมในคลัง
        public void ShowInventory()
        {
            foreach (var item in inventory)
            {
                Debug.Log($"Item: {item.Key} : {item.Value}");
            }
        }
    }
}