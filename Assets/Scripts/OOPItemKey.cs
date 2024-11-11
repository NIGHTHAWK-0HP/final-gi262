using System.Collections;
using System.Collections.Generic;
using Student;
using UnityEngine;

public class OOPItemKey : Identity
{
    public override void Hit()
    {
        mapGenerator.player.inventory.AddItem("Red Key");
        mapGenerator.player.inventory.ShowInventory();
        mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
        Destroy(gameObject);
    }
}