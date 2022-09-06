using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Line4", order = 1)]
public class ScriptableLine4 : ScriptableObject
{
    
    public float[,] cellPosition = new float[,] { { -1.5f, 0 }, { -0.5f, 0 }, { 0.5f, 0 }, { 1.5f, 0 } };
    public int rotation = 0;
    
}
