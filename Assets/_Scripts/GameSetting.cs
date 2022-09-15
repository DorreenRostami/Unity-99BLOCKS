using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]

public class GameSetting : ScriptableObject
{

    public Vector3 touchOffset;
    //public int scorePerBlock;
    public float borderDistance;
    public float clickedScale;
    public Vector3 startedScale;
    public float timeInSeconds;

    public int scorePerCell; //which is 5
    public List<GridBlock> popBlocks;

}
