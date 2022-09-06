using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/Line3", order = 1)]
public class ScriptableLine3 : ScriptableObject
{
    
    public float[,] cellPosition = new float[,] { { -1f, 0 }, { 0, 0 }, { 1f, 0 } };
    public int rotation = 0;
    
}
