using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Student
{
    public class OOPWall : Identity
    {
        // Initialize any properties or perform setup here
        private void Start()
        {
            // No need to check for the OOPWall component here because it's already attached
            // You can initialize specific properties related to this wall here if needed
            Debug.Log("OOPWall initialized at position: " + transform.position);
        }
    }
}
