using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]

public class GameSetting : ScriptableObject
{

    public Vector3 touchOffset;
    public int scorePerBlock;
    public float borderDistance;
    public float clickedScale;

    public float timer;

}
