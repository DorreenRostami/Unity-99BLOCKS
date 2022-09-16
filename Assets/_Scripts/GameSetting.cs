using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]

public class GameSetting : ScriptableObject
{

    public Vector3 touchOffset;
    public float borderDistance;
    public float clickedScale;
    public Vector3 startedScale;
    public float timeInSeconds;

    public int scorePerShape; //which is 5
    public int mainScore = 0;
    public List<GridBlock> popBlocks;

    private void Awake()
    {
        mainScore = 0;
        scorePerShape = 5;
    }

}
