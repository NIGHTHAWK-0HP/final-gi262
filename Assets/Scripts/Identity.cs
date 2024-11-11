using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Identity class เป็น base class (คลาสแม่) ของทุก object ที่ถูกสร้างขึ้นในเกม
public class Identity : MonoBehaviour
{
    public string Name;
    public int positionX;
    public int positionY;
    public OOPMapGenerator mapGenerator;

    public virtual void Hit()
    {
        Debug.Log("hit!");
    }
}