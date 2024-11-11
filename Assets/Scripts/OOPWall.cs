using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// OOPWall aka "Demon Wall"
public class OOPWall : Identity
{
    public void Hit()
    {
        Debug.Log("wall hit!");
        mapGenerator.mapdata[positionX, positionY] = mapGenerator.empty;
        Destroy(gameObject);
        mapGenerator.player.TakeDamage(10);
    }
}